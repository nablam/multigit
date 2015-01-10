using UnityEngine;
using System.Collections;

public class RangerMovement : MonoBehaviour {

    public AnimationClip idleClip;
    public AnimationClip runClip;
    public AnimationClip runBackClip;
    public AnimationClip jumpClip;
    public AnimationClip fallClip;
    public AnimationClip dieClip;

	float speed = 10f;
	Vector3 direction = Vector3.zero;

    CharacterController cc;
	// Use this for initialization
	void Start () {
        animation.AddClip(idleClip, "idleing");
        animation.AddClip(runClip, "running");
        animation.AddClip(runBackClip, "runningback");
        animation.AddClip(jumpClip, "jimping");
        animation.AddClip(fallClip, "falling");
        animation.AddClip(dieClip, "deying");
       




       cc= transform.GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {
        animation.CrossFade("running");

		direction =transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

	}

	void FixedUpdate() {
        cc.SimpleMove(direction * speed);
    }



}
