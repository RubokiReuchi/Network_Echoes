using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnlineManager : MonoBehaviour
{
    static public OnlineManager onlineManager;

    [SerializeField] PlayerController boyController;
    [SerializeField] PlayerController girlController;
    private void Awake()
    {
        onlineManager = this; // revisar
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Server")) girlController.enabled = false;
        else boyController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
