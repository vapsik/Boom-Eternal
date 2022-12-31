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

    public static int killsSinceHealthDrop = 0;

    //Scene Referencer seab selle
    public static GameObject[] playerBulletPrefabs, enemyBulletPrefabs;
    
    public static int hp;
    public static int maxHP;

    public static int bulletCount;
    public static int maxBulletCount;
    public static GameObject currentSceneLight;
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
<<<<<<< Updated upstream
=======
            audioManager.playSound("pickUpBullet");
            return true;
        }
        else{
            return false;
        }
    }
    public static bool AddHP(int hpAdded = 2, bool maxCanIncrease = false){
        if(hp < maxHP){
            hp += hpAdded;
            if (hp > maxHP)
            {
                hp = maxHP;
            }
            audioManager.playSound("pickUpBullet");
            return true;
        }
        else if (maxCanIncrease){
            hp += hpAdded;
            maxHP += hpAdded;
            audioManager.playSound("pickUpBullet");
            return true;
        }
        else{
            return false;
        }
    }
    public static bool CheckTile(Vector2 pos){
        if(floorWallCeilingTiles[0].HasTile(floorWallCeilingTiles[0].WorldToCell(pos))
        && !floorWallCeilingTiles[1].HasTile(floorWallCeilingTiles[1].WorldToCell(pos))
        && !floorWallCeilingTiles[2].HasTile(floorWallCeilingTiles[2].WorldToCell(pos))){
>>>>>>> Stashed changes
            return true;
        }
        else{
            return false;
        }
    }
}