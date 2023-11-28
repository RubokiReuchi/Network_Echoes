using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnlineManager : MonoBehaviour
{
    static public OnlineManager instance;

    [SerializeField] Serialize serialize;

    [SerializeField] PlayerController boyController;
    [SerializeField] PlayerController girlController;
    [SerializeField] RemotePlayerController boyRemoteController;
    [SerializeField] RemotePlayerController girlRemoteController;

    [SerializeField] DetectorEchoAttack boyEcho; 
    [SerializeField] ProjectileEchoAttack girlEcho;
    [SerializeField] RemoteDetectorEchoAttack boyRemoteEcho;
    [SerializeField] RemoteProjectileEchoAttack girlRemoteEcho;
    private void Awake()
    {
        instance = this; // revisar
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Server"))
        {
            boyController.enabled = true;
            girlRemoteController.enabled = true;
            boyEcho.enabled = true;
            girlRemoteEcho.enabled = true;
            serialize.controlledCharacter = boyController.transform;
        }
        else
        {
            girlController.enabled = true;
            boyRemoteController.enabled = true;
            girlEcho.enabled = true;
            boyRemoteEcho.enabled = true;
            serialize.controlledCharacter = girlController.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
