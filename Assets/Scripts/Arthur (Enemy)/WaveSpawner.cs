using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
        public string waveName;
        public int numEnemies;
        public GameObject[] typeOfEnemies;
        public float spawnInterval;
}
public class WaveSpawner : MonoBehaviour
{
        public Wave[] waves;
        public Transform[] spawnPoints;
        public Animator animator;
        public TMP_Text waveName;

        public static Wave currentWave;
        private int currentWaveNum;

        private float nextSpawnTime;

        private bool canSpawn = true;
        private bool canAnimate = false;

        private void Start()
        {
            currentWaveNum = 0;
            currentWave = waves[currentWaveNum];
            waveName.text = currentWave.waveName;
        }

        private void Update()
        {
            currentWave = waves[currentWaveNum];
            spawnWave();
            GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (totalEnemies.Length == 0)
            {
                if (currentWaveNum+1 != waves.Length)
                {
                    if (canAnimate)
                    {
                    waveName.text = waves[currentWaveNum+1].waveName;
                    animator.SetTrigger("WaveComplete");
                    canAnimate = false;
                    }
                }
                else
                {
                    if (currentWave.numEnemies == 0){
                                            PauseMenuController.isGameWon = true;
                    }
                }
            }
            
        }
        void spawnNextWave()
        {
                currentWaveNum++;
                canSpawn = true;
        }
        void spawnWave()
        {

            if (canSpawn && nextSpawnTime < Time.time)
            {
                GameObject randomEnemy = currentWave.typeOfEnemies[Random.RandomRange(0, currentWave.typeOfEnemies.Length)];
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
                currentWave.numEnemies--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;
                if (currentWave.numEnemies == 0)
                {
                    canSpawn = false;
                    canAnimate = true;
                } 
            }

        }
}

