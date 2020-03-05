using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSettings
{
    public class UserColor
    {
        public int[] color;
    }

    public class EnemyColor
    {
        public int[] color1;
        public int[] color2;
    }

    public class EnemySpawnSettings
    {
        public int[] count;
        public float[] size;
    }

    public UserColor user;
    public EnemyColor enemy;
    public EnemySpawnSettings enemySpawnSettings;
}
