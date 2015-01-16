using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float selfDestucttime = 1.0f;
	
	// Update is called once per frame
	void Update () {
        selfDestucttime -= Time.deltaTime;
        if (selfDestucttime <= 0) {
    
            PhotonView pv = GetComponent<PhotonView>();
            if (pv != null && pv.instantiationId != 0) {
                PhotonNetwork.Destroy(gameObject);
            }
            else
                Destroy(gameObject);
        }
	}
}
