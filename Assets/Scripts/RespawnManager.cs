using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;  

    [SerializeField] GameObject playerBoy;
    [SerializeField] GameObject playerGirl;

    [NonEditable] public GameObject currentCheckpoint;

    RemoteInputs inputs;
    public GameObject[] checkpoints;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.FindGameObjectWithTag("OnlineManager").GetComponent<RemoteInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs.checkpoint != -1)
        {
            Debug.Log("new checkpoint is " + inputs.checkpoint);
            SetCurrentSpawn(checkpoints[inputs.checkpoint]);
            inputs.checkpoint = -1;
        }
    }

    public void SetCurrentSpawn(GameObject go)
    {
        currentCheckpoint = go;
    }

    void Respawn()
    {
        Transform boyPoint = currentCheckpoint.transform.Find("BoyPoint");
        playerBoy.transform.position = boyPoint.position;
        playerBoy.transform.rotation = boyPoint.rotation;
        PlayerController boyController = playerBoy.GetComponent<PlayerController>();
        if (boyPoint.rotation.eulerAngles.y == 180) boyController.lookingRight = false;
        else boyController.lookingRight = true;
        boyController.rotationAngle = boyPoint.rotation.eulerAngles.y;

        Transform girlPoint = currentCheckpoint.transform.Find("GirlPoint");
        playerGirl.transform.position = girlPoint.position;
        playerGirl.transform.rotation = girlPoint.rotation;
        PlayerController girlController = playerGirl.GetComponent<PlayerController>();
        if (girlPoint.rotation.eulerAngles.y == 180) girlController.lookingRight = false;
        else girlController.lookingRight = true;
        girlController.rotationAngle = girlPoint.rotation.eulerAngles.y;

        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");
        foreach (GameObject key in keys)
        {
            key.GetComponent<Key>().RespawnKey();
        }
    }

    public void FadeInFinish()
    {
        Respawn();
        FadeManager.instance.FadeOut();
    }
}
