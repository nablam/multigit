using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float hitpoint = 100;
    private float curhitpoints;

	// Use this for initialization
	void Start () {
        curhitpoints = hitpoint;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [RPC]
    public void takeDamage(float amt) {
        curhitpoints -= amt;
        if (curhitpoints <= 0) {
            Die();
        }
    }

    void Die(){
        if (GetComponent<PhotonView>().instantiationId == 0) { Destroy(this.gameObject); }
        else { 
            //only destroy it once not once per machine
            if (GetComponent<PhotonView>().isMine) {
                PhotonNetwork.Destroy(gameObject); 
            }
           
        
        }
          
    
    
    }

}
