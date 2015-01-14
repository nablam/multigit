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

	   animation.wrapMode = WrapMode.Loop;
		animation.AddClip(idleClip, "idleing");
		animation.AddClip(runClip, "running");
		animation.AddClip(runBackClip, "runningback");
		
		animation.wrapMode = WrapMode.Once;
		animation.AddClip(jumpClip, "jumping");
		animation.AddClip(dieClip, "dying");
		animation.wrapMode = WrapMode.Clamp;
		animation.AddClip(fallClip, "falling");


		animation["jumping"].layer = 7; 
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
	private float vertVelo = 0f;
	public Vector3 moveDirection = Vector3.zero;
	public bool isjumping = false;
	void Update()
	{
		//animation.CrossFade("running");

		moveDirection = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

		if (moveDirection.magnitude > 1f) moveDirection = moveDirection.normalized;

		if (moveDirection != Vector3.zero)
			if (moveDirection.z > 0) animation.CrossFade("running");
			else
				animation.CrossFade("runningback");
		else
			animation.CrossFade("idleing");


		if (cc.isGrounded)
		{
			isjumping = false;
			vertVelo = -2.20f;
		}
		else
			animation.CrossFade("falling");

		if ( cc.isGrounded &&  Input.GetKeyDown("space"))
		{
			vertVelo = jumpSpeed;
			animation.Stop();
			Debug.Log("AAAND JUMP");
			animation.CrossFade("jumping");
			isjumping = true;
		}
	

	}
   

	void FixedUpdate() {

		Vector3 distTravel = moveDirection * speed * Time.deltaTime;

		vertVelo += Physics.gravity.y  * Time.deltaTime;

	 
		distTravel.y = vertVelo * Time.deltaTime;
		cc.Move(distTravel);

	  //  CharacterController controller = GetComponent<CharacterController>();
		
		
	}



}
