using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnlineManager : MonoBehaviour
{
    static public OnlineManager onlineManager;

    [SerializeField] PlayerInput boyInput;
    [SerializeField] PlayerInput girlInput;
    private void Awake()
    {
        onlineManager = this; // revisar
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Server")) girlInput.enabled = false;
        else boyInput.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
