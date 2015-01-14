using UnityEngine;
using System.Collections;

public class MyNetworkChar : Photon.MonoBehaviour {
	private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this

	private Vector3 correctPlayerT2BONEPos = Vector3.zero; // We lerp towards this
	private Quaternion correctPlayerT2BONERot = Quaternion.identity; // We lerp towards this

    private Vector3 pos=Vector3.zero;
    private Vector3 post2=Vector3.zero;

    private Quaternion rot = Quaternion.identity;
    private Quaternion rott2 = Quaternion.identity;

    Transform t2bone;
	public int y=90;
    public AnimationClip runClip;
	// Use this for initialization
	void Start () {
        animation.AddClip(runClip, "running");
		PhotonNetwork.sendRate = 20;
		PhotonNetwork.sendRateOnSerialize = 10;

		t2bone = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
		//correctPlayerT2BONEPos = t2bone.position;
		//correctPlayerT2BONERot = t2bone.rotation;
	}
 //   void FixedUpdate() { animation.CrossFade("running"); }
	// Update is called once per frame
	void Update () {
      //  photonView.RPC("doruns", PhotonTargets.All); 
        
		/*
		if (photonView.isMine)
		{
			//do nothing, ou controller if handelling the motion
		}

		else
		{
			//lerp from ewhere we thik they are to whwre they are 
			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 0.1f);

			t2bone.position = Vector3.Lerp(t2bone.position, this.correctPlayerT2BONEPos, Time.deltaTime * 0.1f);
			t2bone.rotation = Quaternion.Lerp(t2bone.rotation, this.correctPlayerT2BONERot, Time.deltaTime * 0.1f);
		}
	   // GetComponent<PhotonView>().isMine
	*/
		if (photonView.isMine)
		{
		//	pos = transform.position;
		//	rot = transform.rotation;
		//	post2 = t2bone.position;
		//	rott2 = t2bone.rotation;

		}
		else
		{
             
            photonView.RPC("doruns", PhotonTargets.All );
            animation.Play("running");
			transform.position = Vector3.Lerp(transform.position, this.pos, Time.deltaTime *  5f);
			transform.rotation = rot;
			t2bone.position = Vector3.Lerp(t2bone.position, this.post2, Time.deltaTime *  0.5f);
			t2bone.rotation = rott2;
		}


	}

    [RPC]
    public void doruns() { animation.Play("running"); }



  
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{

	  

	   
	   // Debug.Log("clearly running ");
		if (stream.isWriting)
		{ 
			//this is OUR player , we need to send our poeition
            pos = transform.position;
            rot = transform.rotation;
            post2 = t2bone.position;
            rott2 = t2bone.rotation;


			stream.SendNext(pos);
			stream.SendNext(rot);
			stream.SendNext(post2);
			stream.SendNext(rott2);
		
		}
		else
		{ 
			//this is THEIR player ,  Receive their position , and update our version

            pos = (Vector3)stream.ReceiveNext();
            rot = (Quaternion)stream.ReceiveNext();
            post2 = (Vector3)stream.ReceiveNext();
            rott2  = (Quaternion)stream.ReceiveNext();
		}
		
	}
	
}
