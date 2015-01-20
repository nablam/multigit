using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NetworkManager : MonoBehaviour
{

    public Camera standbyCamera;
    SpawnSpot[] spawnSpots;

    public bool offlineMode = false;

    bool connecting = false;

    List<string> chatMessages;
    int maxChatMessages = 5;

    public float respawnTimer = 0f;

    // Use this for initialization
    void Start()
    {
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "lol");
        chatMessages = new List<string>();
    }

    void OnDestroy()
    {
        PlayerPrefs.SetString("Username", PhotonNetwork.player.name);
    }

    public void AddChatMessage(string m)
    {
        GetComponent<PhotonView>().RPC("AddChatMessage_RPC", PhotonTargets.AllBuffered, m);
    }

    [RPC]
    void AddChatMessage_RPC(string m)
    {
        while (chatMessages.Count >= maxChatMessages)
        {
            chatMessages.RemoveAt(0);
        }
        chatMessages.Add(m);
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("MultiFPS v001");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.connected == false && connecting == false)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Username: ");
            PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name);
            GUILayout.EndHorizontal();
            
            
            if (GUILayout.Button("Single Player"))
            {
                connecting = true;
                PhotonNetwork.offlineMode = true;
                OnJoinedLobby();
            }
            
            if (GUILayout.Button("WAAAAN LOBBYYY!!!"))
            {
                connecting = true;
                Connect();

            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        if (PhotonNetwork.connected == true && connecting == false)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            foreach (string msg in chatMessages)
            {
                GUILayout.Label(msg);
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();

        }

    }

    void OnJoinedLobby()
    {
       // Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
     //   Debug.Log("OnJoinedRoom");

        connecting = false;
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {
        Screen.showCursor = false;
        AddChatMessage("Spawning player: " + PhotonNetwork.player.name);


        if (spawnSpots == null)
        {
            Debug.LogError("WTF?!?!?");
            return;
        }

        SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
      //  Debug.Log("how many spawnp" + spawnSpots.Length);

        GameObject myplayer = (GameObject)PhotonNetwork.Instantiate("RangerPrefab", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0); //group 0 does nothing on cloud
        standbyCamera.enabled = false;
        //myplayer.GetComponent<TextMesh>().name = RangerLabel



        // **** turning scripts on
        ((MonoBehaviour)myplayer.GetComponent("RangerMovement")).enabled = true;
        ((MonoBehaviour)myplayer.GetComponent("specialMouseMove")).enabled = true;
        ((MonoBehaviour)myplayer.GetComponent("PlayerShoot")).enabled = true;
      //  ((MonoBehaviour)myplayer.GetComponent("MyNetworkChar")).enabled = true;

        //  ((MonoBehaviour)myplayer.GetComponent("NetworkChar2")).enabled = true;
        //    ((MonoBehaviour)myplayer.GetComponent("myFPScontroller")).enabled = true;  
        //****Finding the camera
        Transform amIcam = myplayer.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0);
        //Debug.Log("am i cam? " + amIcam.name);
        amIcam.gameObject.SetActive(true);

        //naming
        myplayer.transform.FindChild("RangerLabel").GetComponent<TextMesh>().text = PhotonNetwork.player.name;
    
    }

    void Update() {
        if (respawnTimer > 0) { 
            //in health grab this Network manger in the Die() lbl 1337
            respawnTimer -= Time.fixedDeltaTime;
            if (respawnTimer <= 0) {
            //time to respawn playa
                SpawnMyPlayer();

            }
        }
    
    }
}



/*
public class NetworkManager : MonoBehaviour
{
    SpawnSpot[] SpawSpots;
    public Camera standbyCam;

    public int x;

    public bool offlinemode = false;

    // Use this for initialization
    void Start(){
        SpawSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        //SpawSpots = GameObject.FindObjectsOfType(typeof(SpawnSpot)) as SpawnSpot[];

     
      
    }

    void Connect() {
        if (offlinemode) PhotonNetwork.offlineMode = true;
        else
        PhotonNetwork.ConnectUsingSettings("v0.0.1");
    }


    void OnGUI(){
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.connected == false) {
            if (GUILayout.Button("Multiplay Player")) Connect();

        }
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
      //  ((MonoBehaviour)myplayer.GetComponent("NetworkChar2")).enabled = true;
    //    ((MonoBehaviour)myplayer.GetComponent("myFPScontroller")).enabled = true;  
        //****Finding the camera
      Transform amIcam=  myplayer.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0);
      //Debug.Log("am i cam? " + amIcam.name);
      amIcam.gameObject.SetActive(true);
      //  myplayer.GetComponent<MyNetworkChar>().enabled = true;
    }


}

*/