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

    Animator Anim;

    void Awake() {

        t2bone = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
        Anim = transform.GetComponent<Animator>();
        RM = transform.GetComponent<RangerMovement>();
    }

	// Use this for initialization
	void Start () {
    
        
		PhotonNetwork.sendRate = 20;
		PhotonNetwork.sendRateOnSerialize = 10;

		//correctPlayerT2BONEPos = t2bone.position;
		//correctPlayerT2BONERot = t2bone.rotation;

   
 
	}
	
	// Update is called once per frame
	void Update () {
	
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
     

            transform.position = Vector3.Lerp(transform.position, this.pos, Time.deltaTime * 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.rot, Time.deltaTime * 0.05f);

            t2bone.position = post2;
            t2bone.rotation = rott2;

           
        }


	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
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

            stream.SendNext(Anim.GetFloat("speed_param"));
            stream.SendNext(Anim.GetBool("jumping_param"));
		
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

            Anim.SetFloat("speed_param", (float)stream.ReceiveNext());
            Anim.SetBool("jumping_param", (bool)stream.ReceiveNext());
		}
		
	}

	
}
