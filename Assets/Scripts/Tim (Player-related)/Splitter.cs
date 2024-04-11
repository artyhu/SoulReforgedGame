using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
{
    public Transform spawnPoint;

    private Aiming mouse;

    [SerializeField]
    private float spawnRange = 5f;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Clone clone;

    private PlayerHealth hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<PlayerHealth>();
        mouse = GetComponent<Aiming>();
    }

    private void OnSplitter()
    {
                        if (!PauseMenuController.isPaused)
        {
        if (clone.isActiveAndEnabled)
        {
            hp.hpLossPerSec = 0.05f;
            //reset to original position of player
            spawnPoint.position = gameObject.transform.position;
            clone.Inactive();
        }
        else
        {

            // check spawnpoint within radius
            Vector3 center = spawnPoint.position;

            float rSquared = spawnRange * spawnRange;
            float distance = (mouse.mousePos - center).sqrMagnitude;

            if (distance < rSquared)
            {
                spawnPoint.position = mouse.mousePos;
            }
            else
            {
                spawnPoint.position = center + ((mouse.mousePos - center).normalized * spawnRange);
            }

            hp.hpLossPerSec = 0.1f;
            clone.Active(spawnPoint);

            
        }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnPoint == null)
            return;

        Gizmos.DrawWireSphere(spawnPoint.position, spawnRange);
    }
}
