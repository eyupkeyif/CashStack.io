using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyStats enemyStats;
   public class EnemyStats
    {
        public float maxCapacity;
        public float movementSpeed;
        public float collectMultiplier;
    }
}
