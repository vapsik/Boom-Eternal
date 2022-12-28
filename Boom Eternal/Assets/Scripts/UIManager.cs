using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI hpText, scoreText, bulletsText;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image crosshair;
    [HideInInspector] public bool onPause = false;
    // Start is called before the first frame update
    /*void Awake()
    {
        GlobalReferences.crosshair = crosshair;
    }*/

    // Update is called once per frame
    void Update()
    {
        hpText.text = "hp: " + GlobalReferences.hp.ToString() + " / " + GlobalReferences.maxHP.ToString();
        bulletsText.text = "bullets: " + GlobalReferences.bulletCount.ToString() + " / " + GlobalReferences.maxBulletCount.ToString();
        scoreText.text = GlobalReferences.score.ToString();
        if(Input.GetKeyDown(KeyCode.Escape)){
            SetOnPause();
        }
        if(onPause){
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            pauseMenu.SetActive(true);
            GlobalReferences.onPause = true;
        }
        else{
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            GlobalReferences.onPause = false;
        }
    }
    public void SetOnPause(){
        onPause = !onPause;
    }
}
