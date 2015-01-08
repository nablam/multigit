using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

    public Camera standbyCam;

    public int x;

    // Use this for initialization
    void Start(){

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
        PhotonNetwork.Instantiate("Acontroller", Vector3.zero, Quaternion.identity, 0); //group 0 does nothing on cloud
    }aa
}