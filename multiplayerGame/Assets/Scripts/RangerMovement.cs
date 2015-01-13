using UnityEngine;
using System.Collections;

public class RangerMovement : MonoBehaviour
{

	#region pubs
	public AnimationClip idleClip;
	public AnimationClip runClip;
	public AnimationClip runBackClip;
	public AnimationClip jumpClip;
	public AnimationClip fallClip;
	public AnimationClip dieClip;

	#endregion

	#region privs
	private Transform Rangert2bone;
	//float speed = 10f;
  //  Vector3 moveDirectiondirection = Vector3.zero;
  //  private float jumpSpeed = 8.0F;
  //  private float gravity = 20.0F;
 //   private Vector3 moveDirection = Vector3.zero;

	CharacterController cc;

	#endregion

	// Use this for initialization
	void Start () {

	   cc=  transform.GetComponent<CharacterController>();
		animation.AddClip(idleClip, "idleing");
		animation.AddClip(runClip, "running");
		animation.AddClip(runBackClip, "runningback");
		animation.AddClip(jumpClip, "jimping");
		animation.AddClip(fallClip, "falling");
		animation.AddClip(dieClip, "deying");

	 //   Rangert2bone = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
	  //  animation["running"].AddMixingTransform(Rangert2bone);



	//   cc= transform.GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	//void Update () {
	

	 //   moveDirection = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;


	   //   CharacterController controller = GetComponent<CharacterController>();
	 //   if (controller.isGrounded) {
		 //   moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		 //   moveDirection = transform.TransformDirection(moveDirection);
		  //  moveDirection *= speed;
		  //  if (Input.GetButton("Jump"))
			//    moveDirection.y = jumpSpeed;
			
	   // }
	   // moveDirection.y -= gravity * Time.deltaTime;
	   // controller.Move(moveDirection * Time.deltaTime);
	


		/*
		if (cc.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;

		}
		moveDirection.y -= gravity * Time.deltaTime;
		cc.Move(moveDirection * Time.deltaTime);
   /     */
	//}

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	private float gravity = 50.0F;
	private Vector3 moveDirection = Vector3.zero;
	void Update()
	{
		animation.CrossFade("running");

		moveDirection = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

	}

	

	void FixedUpdate() {



		cc.SimpleMove(moveDirection * speed);

	  //  CharacterController controller = GetComponent<CharacterController>();
		
		
	}



}
