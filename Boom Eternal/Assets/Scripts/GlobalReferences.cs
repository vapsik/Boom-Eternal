using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalReferences 
{
    public static Camera mainCamera;
    public static GameObject thePlayer;
    public static int hp;
    public static int score;


    public static class Tags
    {
        public const string PlayerBullet = "Bullet";
    }

}