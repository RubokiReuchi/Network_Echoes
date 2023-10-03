using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class MultiplayerMenu : MonoBehaviour
{
    [SerializeField] InitialScreenManager manager;

    public GameObject createMenu;
    public GameObject joinMenu;

    bool workingOnRoom;
    [SerializeField] AudioSource accept;


    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        workingOnRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!workingOnRoom)
            {
                accept.Play();
                manager.OpenMenu(ACTIVE_MENU.MAIN);
            }
            else
            {
                createMenu.SetActive(false);
                joinMenu.SetActive(false);
            }
        }
    }

    public void CreateRoom()
    {
        createMenu.SetActive(true);
        workingOnRoom = true;
    }

    public void JoinRoom()
    {
        joinMenu.SetActive(true);
        workingOnRoom = true;
    }
}
