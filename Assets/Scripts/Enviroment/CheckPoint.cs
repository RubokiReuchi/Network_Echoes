using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int checkpointID;
    
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RespawnManager.instance.SetCurrentSpawn(gameObject);
            GameObject.FindGameObjectWithTag("OnlineManager").GetComponent<RemoteInputs>().checkpoint = checkpointID;
        }
    }
}
