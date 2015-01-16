using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    public float fireRate = 0.5f;
    float cooldown = 0f;
    Transform amIcam;
    float damage = 25f;

    FXmanager fxmngr;

	// Use this for initialization
	void Start () {
        fxmngr = GameObject.FindObjectOfType<FXmanager>();
        if (fxmngr == null) {
            Debug.LogError("Yo add an Fxmanager on an object in game");
        }

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
        Transform hitTransform;
        Vector3 hitPoint;
        hitTransform = FindClosestHitObject(ray, out hitPoint); //hitpoint is teh coordiante that we hit 

        if (hitTransform != null)
        {

            //do ricocheyt effect at info.point
            Debug.Log("we hit a " + hitTransform.transform.name);
            Health h = hitTransform.GetComponent<Health>();

            while (h == null && transform.parent)
            {
                hitTransform = hitTransform.parent;
                h = hitTransform.GetComponent<Health>();
            }
            //once we are here , hit transfiorm may not be the original hit transform 
            if (h != null)
            {
                PhotonView pv = h.GetComponent<PhotonView>();
                // h.takeDamage(damage);  //next is the networked version
                if (pv == null) { Debug.Log(" freak out ! no photonview attached"); }
                else
                {
                    pv.RPC("takeDamage", PhotonTargets.All, damage);
                    //   pv.RPC("takeDamage", PhotonTargets.MasterClient, damage);

                }

            }

            if (fxmngr != null)
            {
                fxmngr.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, amIcam.position, hitPoint);
            }

        }
        else
        { 
        //we hit jack shit, but lets do a visual efect anyway 
            //Vector3 low =  new Vector3 (amIcam.position.x,  amIcam.position.y-0.5f,  amIcam.position.z +0.5f);
            hitPoint = amIcam.position + (amIcam.forward *100);
            fxmngr.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, amIcam.position, hitPoint);
           // fxmngr.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, Camera.main.transform.position, hitPoint);
            Debug.Log("name " + amIcam.name);
            Debug.Log("pos " + amIcam.position);
        }

        cooldown = fireRate;


      //  if (Physics.Raycast(ray, out hitinfo)) {  Debug.Log("we hit a " + hitinfo.transform.name);}    
    }

    Transform FindClosestHitObject(Ray ray, out Vector3 hitPoint)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);
        Transform closestHit  = null;
        float distance = 0;
        hitPoint = Vector3.zero;

        foreach (RaycastHit hit in hits) {
            if (hit.transform != this.transform && (closestHit == null || hit.distance < distance))
            {

                closestHit = hit.transform;
                distance = hit.distance;
                hitPoint = hit.point;
            }
        
        }

        return closestHit;
    }

}
