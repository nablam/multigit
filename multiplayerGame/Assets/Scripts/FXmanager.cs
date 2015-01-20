using UnityEngine;
using System.Collections;

public class FXmanager : MonoBehaviour {
    public GameObject bulletPrefab;

   // public AudioClip bulletFXaudio;


    [RPC]
    void SniperBulletFX(Vector3 startPos, Vector3 endPos) {

      //  Debug.Log("BULLET EFFECT --------------->");

        if (bulletPrefab != null)
        {
            GameObject sniperFX = (GameObject)Instantiate(bulletPrefab, startPos, Quaternion.LookRotation(endPos - startPos));

            LineRenderer lr = sniperFX.transform.Find("LineFX").GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.SetPosition(0, startPos);
                lr.SetPosition(1, endPos);
            }
            else
            {
                Debug.LogError("sniperBulletFXPrefab's linerenderer is missing.");
            }
        }
        else
        {
            Debug.LogError("sniperBulletFXPrefab is missing!");
        }

    } 
}
