using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyData enemyData;
        private EnemyView enemyView;
        private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();
        public EnemyPool(EnemyView enemyView, EnemyData enemyData)
        {
            this.enemyData = enemyData;
            this.enemyView = enemyView;
        }

        public EnemyController GetEnemy()
        {
            if (pooledEnemies.Count > 0)
            {
                PooledEnemy pooledEnemy = pooledEnemies.Find(item => item.isUsed != true);
                if (pooledEnemy != null)
                {
                    pooledEnemy.isUsed = true;
                    return pooledEnemy.Enemy;
                }
            }
            return CreateNewPooledEnemy();
        }
        public void ReturnToEnemyPool(EnemyController returnedEnemy)
        {
            PooledEnemy pooledEnemy = pooledEnemies.Find(item => item.Enemy.Equals(returnedEnemy));
            pooledEnemy.isUsed = false;
        }
        private EnemyController CreateNewPooledEnemy()
        {
            PooledEnemy pooledEnemy = new PooledEnemy();
            pooledEnemy.Enemy = new EnemyController(enemyView,enemyData);
            pooledEnemy.isUsed = true;
            pooledEnemies.Add(pooledEnemy);
            return pooledEnemy.Enemy;
        }
        public class PooledEnemy
        {
            public EnemyController Enemy;
            public bool isUsed;
        }
    }
}
