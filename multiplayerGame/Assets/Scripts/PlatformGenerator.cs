using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
		


	}

    float timer2 = 1f;
	// Update is called once per frame
	void Update () {

        timer2 -= Time.deltaTime;
        if(timer2 <= 0f){
            GameObject myplayer = (GameObject)PhotonNetwork.Instantiate("singlePlatformPrefab", transform.position, transform.rotation, 0); //group 0 does nothing on cloud
        timer2=12f;
        }

	//	GameObject myplayer = (GameObject)PhotonNetwork.Instantiate("MagicCubeprefab", transform.position, transform.rotation, 0); //group 0 does nothing on cloud

	}
}
