using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class GlobalReferences 
{
    public static Camera mainCamera;
    public static GameObject thePlayer;
    public static bool onPause = false;
    public static int score = 0;
    public static bool thePlayerIsInvincible;
    //Scene Referencer seab selle
    public static GameObject[] playerBulletPrefabs, enemyBulletPrefabs;
    
    public static int hp = 15;
    public static int maxHP = 15;

    public static int bulletCount = 0;
    public static int maxBulletCount = 20;

    public static GameObject ammoDropPrefab;
    public static GameObject[] listOfEnemyPrefabs;
    public static class Tags
    {
        public const string PlayerBullet = "Bullet";
    }
    public static class Settings
    {
        public static float sensitivity = 3500;
        public static float volume = 0.5f;
    }
    public static bool AddAmmo(bool eatLead){
        if(bulletCount < maxBulletCount){
            bulletCount += 1;
            return true;
        }
        else if(eatLead){
            bulletCount += 1;
            //mängu disaini küsimus: kas magazine'i suurust ka eatlead'iga suurendada?
            maxBulletCount += 1;
            return true;
        }
        else{
            return false;
        }
    }
}