using UnityEngine;
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

       // GameObject myplayer= (GameObject)  PhotonNetwork.Instantiate("Acontroller", mysp.transform.position, mysp.transform.rotation, 0); //group 0 does nothing on cloud
        GameObject myplayer = (GameObject)PhotonNetwork.Instantiate("RangerPrefab", mysp.transform.position, mysp.transform.rotation, 0); //group 0 does nothing on cloud
        standbyCam.enabled=false;


       // **** turning scripts on
        ((MonoBehaviour)myplayer.GetComponent("RangerMovement")).enabled = true;
        ((MonoBehaviour)myplayer.GetComponent("specialMouseMove")).enabled = true;
        ((MonoBehaviour)myplayer.GetComponent("MyNetworkChar")).enabled = true; 
       
        //****Finding the camera
      Transform amIcam=  myplayer.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0);
      Debug.Log("am i cam? " + amIcam.name);
      amIcam.gameObject.SetActive(true);
      //  myplayer.GetComponent<MyNetworkChar>().enabled = true;
    }


}