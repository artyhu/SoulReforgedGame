using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllowExit : MonoBehaviour
{
    private bool exit;
    private string tagTarget = "Player";

    [SerializeField]
    private GameObject door, mainManager, towerManager, towerTransition;

    private AudioSource source;

    [SerializeField]
    private PolygonCollider2D room;

    [SerializeField]
    private Transform entry;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private CinemachineConfiner2D mainCameraConfiner;

    [SerializeField]
    private TowerTransition gets;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        exit = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void Activate()
    {
        exit = true;
        door.SetActive(false);
    }

    public void Deactive()
    {
        exit = false;
        door.SetActive(true);
        source.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (exit && collision.gameObject.tag == tagTarget)
        {

            towerManager.SetActive(false);
            mainManager.SetActive(true);
            towerTransition.SetActive(true);

            foreach (GameObject i in gets.saves)
            {
                i.SetActive(true);
            }

            player.position = entry.position;
            mainCameraConfiner.m_BoundingShape2D = room;
            gameObject.SetActive(false);
        }
    }
}
