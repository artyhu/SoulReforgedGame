using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerTransition : MonoBehaviour
{

    private string tagTarget = "Player";

    [SerializeField]
    private GameObject mainManager, towerManager, mainTransition;
    [SerializeField]
    private Transform entry;
    [SerializeField]
    private PolygonCollider2D room;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private CinemachineConfiner2D towerCameraConfiner;

    public GameObject[] saves;

    private bool opened = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagTarget)
        {
            saves = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject i in saves)
            {
                i.SetActive(false);
            }

            mainManager.SetActive(false);

            opened = true;
            Debug.Log("yay");
            towerManager.SetActive(true);
            mainTransition.SetActive(true);
            player.position = entry.position;
            towerCameraConfiner.m_BoundingShape2D = room;

            gameObject.SetActive(false);
        }
    }
}
