using UnityEngine;
using System.Collections;

public class RangerMovement : MonoBehaviour
{

	#region pubs


	#endregion

	#region privs
	private Transform Rangert2bone;
	//float speed = 10f;
  //  Vector3 moveDirectiondirection = Vector3.zero;
  //  private float jumpSpeed = 8.0F;
  //  private float gravity = 20.0F;
 //   private Vector3 moveDirection = Vector3.zero;

	CharacterController cc;
    Animator Anim;
	#endregion

	// Use this for initialization
	void Start () {

	   cc=  transform.GetComponent<CharacterController>();
       Anim = transform.GetComponent<Animator>();


	}
	
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	private float gravity = 50.0F;
	private float vertVelo = 0f;
	public Vector3 moveDirection = Vector3.zero;
	public bool isjumping = false;
	void Update()
	{

		moveDirection = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
		if (moveDirection.magnitude > 1f) moveDirection = moveDirection.normalized;

        Anim.SetFloat("speed_param", moveDirection.magnitude);	
	


		if (cc.isGrounded)
		{
			isjumping = false;
			vertVelo = -2.20f;
		}


		if ( cc.isGrounded &&  Input.GetKeyDown("space"))
		{
			vertVelo = jumpSpeed;
			animation.Stop();
			Debug.Log("AAAND JUMP");

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
