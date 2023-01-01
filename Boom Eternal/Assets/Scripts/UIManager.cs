using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI hpText, scoreText, bulletsText, enemiesLeftText, staminaText;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image crosshair;
    [HideInInspector] public static bool onPause = false;
    bool killedAllEnemies;
    private float gameTimeScale = 1;
    bool lastScene;
    // Start is called before the first frame update
    void Awake()
    {
        lastScene = SceneManager.GetActiveScene().name == "SampleScene" ? true : false;  
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "hp: " + GlobalReferences.hp.ToString() + " / " + GlobalReferences.maxHP.ToString();
        bulletsText.text = "bullets: " + GlobalReferences.bulletCount.ToString() + " / " + GlobalReferences.maxBulletCount.ToString();
        scoreText.text = "score: " + GlobalReferences.score.ToString();
        enemiesLeftText.text = !lastScene ?  "# of enemies to kill: " + GlobalReferences.enemiesLeft.ToString()
         : "# of enemies killed: " + GlobalReferences.enemiesLeft + "/20";
        staminaText.text =  "can leap: " + GlobalReferences.leapCount;
        if (GlobalReferences.enemiesLeft == 0){
            killedAllEnemies = true;
            //Debug.Log("killed all enemies");
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            SetOnPause();
        }
    }
    public void SetOnPause(){
        onPause = !onPause;
        if(onPause){
            gameTimeScale = Time.timeScale;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            pauseMenu.SetActive(true);
            GlobalReferences.onPause = true;
        }
        else
        {
            Time.timeScale = gameTimeScale;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            GlobalReferences.onPause = false;
        }
    }
}
