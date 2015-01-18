using UnityEngine;
using System.Collections;

public class MagicCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//float rotationSpeed = 300 * Time.deltaTime;
		//transform.Rotate(new Vector3(0, 0, 2) * rotationSpeed);

		this.transform.Translate(Vector3.up  * Time.deltaTime, Space.World);

		if (transform.position.y >= 100) {
			PhotonNetwork.Destroy(gameObject); 
		}
	
	}
}
