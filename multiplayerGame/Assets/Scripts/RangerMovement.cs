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
    public float movingdircashed;
	void Update()
	{    
		moveDirection = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        movingdircashed = moveDirection.magnitude;
        if (Input.GetAxis("Vertical") < 0) movingdircashed = movingdircashed * -1;
        Anim.SetFloat("speed_param", movingdircashed);
       // Debug.Log("moving " + movingdircashed);
        if (cc.isGrounded)
        {
            Anim.SetBool("jumping_param", false);
            if (Input.GetKeyDown("space"))
            {
                vertVelo = jumpSpeed;
            }

            else
                vertVelo = -0.20f;
        }
        else
        {
            Anim.SetBool("jumping_param", true);
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
