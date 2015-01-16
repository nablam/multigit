using UnityEngine;
using System.Collections;

public class FXmanager : MonoBehaviour {

    public AudioClip bulletFXaudio;
   // public AudioClip bulletRicochet;

    [RPC]
    void SniperBulletFX(Vector3 startPos, Vector3 endPos) {
        Debug.Log("BULLET EFFECT --------------->");
        AudioSource.PlayClipAtPoint(bulletFXaudio, startPos);
    
    } 
}
