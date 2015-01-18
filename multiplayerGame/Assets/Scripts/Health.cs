using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float hitpoint = 100;
    private float curhitpoints;
        Animator Anim;

        RangerMovement RM;
	// Use this for initialization
	void Start () {

        RM = transform.GetComponent<RangerMovement>();

        Anim = transform.GetComponent<Animator>();
        curhitpoints = hitpoint;
	}
	
	

    [RPC]
    public void takeDamage(float amt) {
        curhitpoints -= amt;
        if (curhitpoints <= 0) {
            Die();
        }
    }

  public  void Die(){
        //if not a player
        
        if (GetComponent<PhotonView>().instantiationId == 0) { Destroy(this.gameObject); }
        else { 
            //only destroy it once not once per machine if it is a player
            if (GetComponent<PhotonView>().isMine) {
                if(gameObject.tag=="Player" ){
                //if it's my actual plater object then init respawn process 
                    GameObject.Find("stdbyCam").SetActive(true);
                    
                    //lbl 1337 grabbing NetworkManager
                    GameObject.FindObjectOfType<NetworkManager>().respawnTimer = 2f;


                }
                PhotonNetwork.Destroy(gameObject); 
            }
        }
    }










    /*
   void OnGUI() {
   
   //if this healthobject is mine AND we are the, player
       if (GetComponent<PhotonView>().isMine && gameObject.tag == "Player") {
           if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 40), "kill")) {
               //
               Anim.SetBool("die_param", true);
               StartCoroutine("C1");
               }
       }
    
   }
    */

 IEnumerator C1(){
 yield return new WaitForSeconds(2);
 Die();
}
        

}
