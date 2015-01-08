﻿using UnityEngine;
using System.Collections;

public class MyNetworkChar : Photon.MonoBehaviour {
    private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {
            //do nothing, ou controller if handelling the motion
        }

        else
        {
            //lerp from ewhere we thik they are to whwre they are 
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 0.1f);
        }
       // GetComponent<PhotonView>().isMine
	
	}


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       // Debug.Log("clearly running ");
        if (stream.isWriting)
        { 
            //this is OUR player , we need to send our poeition
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        
        }
        else
        { 
            //this is THEIR player ,  Receive their position , and update our version

            transform.position= (Vector3) stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }

}
