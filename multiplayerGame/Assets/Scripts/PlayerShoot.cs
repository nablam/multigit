using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    public float fireRate = 0.5f;
    float cooldown = 0f;
    Transform amIcam;

	// Use this for initialization
	void Start () {
         amIcam = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1")) {
            Fire();
        }
	}

    void Fire() {
        if (cooldown > 0) return;
        cooldown = fireRate;

        Debug.Log("fireingmahlazpooor");
        Ray ray = new Ray(amIcam.position, amIcam.forward);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo)) {
            Debug.Log("we hit a " + hitinfo.transform.name);
        }

        
    }
}
