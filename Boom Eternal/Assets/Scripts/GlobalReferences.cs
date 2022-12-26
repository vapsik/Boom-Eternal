using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalReferences 
{
    public static Camera mainCamera;
    public static GameObject thePlayer;
    
    public static int score = 0;
    
    public static int hp = 15;
    public static int maxHP = 15;

    public static int bulletCount = 0;
    public static int maxBulletCount = 20;


    public static class Tags
    {
        public const string PlayerBullet = "Bullet";
    }

}