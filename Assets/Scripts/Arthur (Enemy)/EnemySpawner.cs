using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool canSpawn = true;

    [SerializeField]
    private float enemyInterval = 6f;

    [SerializeField]
    private GameObject enemyPrefab;

    //[SerializeField]
    //private GameObject rangedEnemyPrefab;

    //[SerializeField]
    //private float rangedEnemyInterval = 8;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(spawnEnemy(rangedEnemyInterval, rangedEnemyPrefab));
    }

    private void Update()
    {
                        if (!PauseMenuController.isPaused)
        {

        if (canSpawn)
        {
            StartCoroutine(spawnEnemy(enemyInterval, enemyPrefab));
            canSpawn = false;
        }
        }
        
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSecondsRealtime(interval);
        Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-10f, 10f), 0), Quaternion.identity);
        canSpawn = true;
        // infinite enemies spawning, for the update video
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
