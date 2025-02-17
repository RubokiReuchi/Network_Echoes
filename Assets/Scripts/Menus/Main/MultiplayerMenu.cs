using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiplayerMenu : MonoBehaviour
{
    [SerializeField] InitialScreenManager manager;

    public GameObject createButton;
    bool workingOnRoom;

    public GameObject createMenu;
    public GameObject joinMenu;
    public GameObject createPasswordInput;
    public GameObject joinHostInput;

    //public GameObject createMenuTCP;
    //public GameObject joinMenuTCP;
    //public GameObject createPasswordInputTCP;
    //public GameObject joinHostInputTCP;

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
                //createMenuTCP.SetActive(false);
                //joinMenuTCP.SetActive(false);
                EventSystem.current.SetSelectedGameObject(createButton);
            }
        }
    }
    public void CreateRoom()
    {
        createMenu.SetActive(true);
        workingOnRoom = true;
        EventSystem.current.SetSelectedGameObject(createPasswordInput);
    }

    public void JoinRoom()
    {
        joinMenu.SetActive(true);
        workingOnRoom = true;
        EventSystem.current.SetSelectedGameObject(joinHostInput);
    }

    //public void CreateRoomTCP()
    //{
    //    createMenuTCP.SetActive(true);
    //    workingOnRoom = true;
    //    EventSystem.current.SetSelectedGameObject(createPasswordInputTCP);
    //}

    //public void JoinRoomTCP()
    //{
    //    joinMenuTCP.SetActive(true);
    //    workingOnRoom = true;
    //    EventSystem.current.SetSelectedGameObject(joinHostInputTCP);
    //}
}
