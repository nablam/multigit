﻿using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    SpawnSpot[] SpawSpots;
    public Camera standbyCam;

    public int x;

    // Use this for initialization
    void Start(){
        SpawSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        //SpawSpots = GameObject.FindObjectsOfType(typeof(SpawnSpot)) as SpawnSpot[];

       Connect();
      
    }

    void Connect() {
        PhotonNetwork.ConnectUsingSettings("v0.0.1");
       // PhotonNetwork.offlineMode = true;
    }


    void OnGUI(){
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby() {
        Debug.Log("ONLanLobby joined!!");

        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed() {
        Debug.Log("failed random LANLOBBY");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom() {
        Debug.Log("on INA ROOM!!");
        SpawnPlayer();

    }
    void SpawnPlayer() {
        if (SpawSpots == null) {
            Debug.Log("freakout no spawnspots");
        }
        SpawnSpot mysp = SpawSpots[Random.Range(0, SpawSpots.Length)];

        GameObject myplayer= (GameObject)  PhotonNetwork.Instantiate("Acontroller", mysp.transform.position, mysp.transform.rotation, 0); //group 0 does nothing on cloud
        standbyCam.enabled=false;

       // myplayer.GetComponent<MouseLook>().enabled = true;
       // ((MonoBehaviour)myplayer.GetComponent("MouseLook")).enabled = true;
        myplayer.GetComponent<FPSInputController>().enabled= true;
        
        myplayer.GetComponent<CharacterMotor>().enabled = true;
        myplayer.transform.FindChild("Main Camera").gameObject.SetActive(true);
        myplayer.transform.FindChild("Main Camera").GetComponent<MouseLook>().enabled = true;

        //myplayer.transform.FindChild("Main Camera").gameObject.GetComponent<Camera>().enabled = true; // SetActive(true);   

        myplayer.GetComponent<MyNetworkChar>().enabled = false;
    }


}