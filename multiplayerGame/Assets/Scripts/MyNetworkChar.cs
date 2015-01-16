using UnityEngine;
using System.Collections;

public class MyNetworkChar : Photon.MonoBehaviour {
    private Vector3 realpos = Vector3.zero; // We lerp towards this
	private Quaternion realrot = Quaternion.identity; // We lerp towards this

    private Vector3 realpost2 = Vector3.zero; // We lerp towards this
    private Quaternion realrott2 = Quaternion.identity; // We lerp towards this

    private Quaternion rot = Quaternion.identity;
    private Quaternion rott2 = Quaternion.identity;

    private Vector3 localMoveDist = Vector3.zero;
    private bool localisjumping = false;
    private float localspeed = 0f;
    Transform t2bone;

    RangerMovement RM;

    Animator Anim;

    bool gotFirstUpdate = false;

    void Awake() {
        t2bone = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
        Anim = transform.GetComponent<Animator>();
        RM = transform.GetComponent<RangerMovement>();
    }

	// Use this for initialization
	void Start () {   
	//	PhotonNetwork.sendRate = 20;
	//	PhotonNetwork.sendRateOnSerialize = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (photonView.isMine)
		{
 
		}
		else
		{
            //transform.position = Vector3.Lerp(transform.position, realpos, 0.1f);
           // transform.rotation = Quaternion.Lerp(transform.rotation, this.rot, Time.deltaTime * 0.05f);      
        }


	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{ 
			stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(t2bone.position);
            stream.SendNext(t2bone.rotation);


            stream.SendNext(RM.moveDirection);
            stream.SendNext(RM.isjumping);
            stream.SendNext(RM.speed);
            stream.SendNext(Anim.GetFloat("speed_param"));
            stream.SendNext(Anim.GetBool("jumping_param"));	
		}
		else
		{ 
			//this is THEIR player ,  Receive their position , and update our version

          

            realpos = (Vector3)stream.ReceiveNext();
            realrot = (Quaternion)stream.ReceiveNext();

            realpost2 = (Vector3)stream.ReceiveNext();
            realrott2 = (Quaternion)stream.ReceiveNext();

            localMoveDist= (Vector3)stream.ReceiveNext();
            localisjumping = (bool)stream.ReceiveNext();
            localspeed = (float)stream.ReceiveNext();

            Anim.SetFloat("speed_param", (float)stream.ReceiveNext());
            Anim.SetBool("jumping_param", (bool)stream.ReceiveNext());


            //teleport oponant to real position when they first join the LAAAN LOBBBYY
            if (gotFirstUpdate == false)
            {
                transform.position = realpos;
                transform.rotation = realrot;
                t2bone.position = realpost2;
                t2bone.rotation = realrott2;
                gotFirstUpdate = true;
            }
         


		}
		
	}

    void FixedUpdate()
    {

        if (photonView.isMine)
        {
            
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realpos, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realrot,  0.1f);
            //t2bone.position = Vector3.Lerp(t2bone.position, realpost2, 0.1f);
            t2bone.rotation = Quaternion.Lerp(t2bone.rotation, realrott2, 0.1f); 

        }


    }
	
}
