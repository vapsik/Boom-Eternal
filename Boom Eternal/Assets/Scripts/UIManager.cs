using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI hpText, scoreText, bulletsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "hp: " + GlobalReferences.hp.ToString() + " / " + GlobalReferences.maxHP.ToString();
        bulletsText.text = "bullets: " + GlobalReferences.bulletCount.ToString() + " / " + GlobalReferences.maxBulletCount.ToString();
        scoreText.text = GlobalReferences.score.ToString();
    }
}
