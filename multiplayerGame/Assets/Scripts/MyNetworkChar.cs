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

    private Vector3 localMoveDist = Vector3.zero;
    private bool localisjumping = false;
    private float localspeed = 0f;
    Transform t2bone;

    RangerMovement RM;


 

	// Use this for initialization
	void Start () {
        RM = transform.GetComponent<RangerMovement>();
        
		PhotonNetwork.sendRate = 20;
		PhotonNetwork.sendRateOnSerialize = 10;

		t2bone = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
		//correctPlayerT2BONEPos = t2bone.position;
		//correctPlayerT2BONERot = t2bone.rotation;

        animation.wrapMode = WrapMode.Loop;
        animation.AddClip(idleClip, "idleing");
        animation.AddClip(runClip, "running");
        animation.AddClip(runBackClip, "runningback");

        animation.wrapMode = WrapMode.Once;
        animation.AddClip(jumpClip, "jumping");
        animation.AddClip(dieClip, "deying");
        animation.wrapMode = WrapMode.Clamp;
        animation.AddClip(fallClip, "falling");


        animation["jumping"].layer = 7;
 
	}
	
	// Update is called once per frame
	void Update () {
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
          
			pos = transform.position;
			rot = transform.rotation;
			post2 = t2bone.position;
			rott2 = t2bone.rotation;

         

            //Lerping block 

		}
		else
		{
          /*
          //working block for simple pos and rot 
          transform.position = pos;
          transform.rotation = rot;
          t2bone.position = post2;
          t2bone.rotation = rott2;
          */

            transform.position = Vector3.Lerp(transform.position, this.pos, Time.deltaTime * 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.rot, Time.deltaTime * 0.05f);

            t2bone.position = post2;
            t2bone.rotation = rott2;

            photonView.RPC("DotheWalk", PhotonTargets.Others);
        }


	}

    public AnimationClip idleClip;
    public AnimationClip runClip;
    public AnimationClip runBackClip;
    public AnimationClip jumpClip;
    public AnimationClip fallClip;
    public AnimationClip dieClip;

    [RPC]
    void DotheWalk()
    {
        animation.Play("running");
    }
	


    /*
	public void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {

		if (stream.isWriting)
		{
			//this is OUR player , we need to send our poeition
			pos = transform.position;
			rot = transform.rotation;
			post2 = t2bone.position;
			rott2 = t2bone.rotation;
			stream.Serialize(ref pos);
			stream.Serialize(ref post2);
			stream.Serialize(ref rot);
			stream.Serialize(ref rott2);


		}
		else
		{
			stream.Serialize(ref pos);
			stream.Serialize(ref post2);
			stream.Serialize(ref rot);
			stream.Serialize(ref rott2);
			transform.position=pos ;
			transform.rotation=rot ;
			t2bone.position=post2 ;
			t2bone.rotation=rott2 ;

		}
	}

	*/
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

            stream.SendNext(RM.moveDirection);
            stream.SendNext(RM.isjumping);
            stream.SendNext(RM.speed);
		
		}
		else
		{ 
			//this is THEIR player ,  Receive their position , and update our version

            pos = (Vector3)stream.ReceiveNext();
            rot = (Quaternion)stream.ReceiveNext();
            post2 = (Vector3)stream.ReceiveNext();
            rott2  = (Quaternion)stream.ReceiveNext();

            localMoveDist= (Vector3)stream.ReceiveNext();
            localisjumping = (bool)stream.ReceiveNext();
            localspeed = (float)stream.ReceiveNext();


		}
		
	}
    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //We know there should be instantiation data..get our bools from this PhotonView!
        object[] objs = photonView.instantiationData; //The instantiate data..
    

     

    }
	
}
