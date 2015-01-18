using UnityEngine;
using System.Collections;

public class specialMouseMove : MonoBehaviour {
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    private float minimumX = -360F;
    private float maximumX = 360F;

    private float minimumY = -80F;
    private float maximumY = 80F;

    private  float rotationX = 0F;
    private float rotationY = 0F;

    Quaternion originalRotation;

    Transform t2bone;


    void Awake()
    {
       t2bone = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
      //  Debug.Log("IAM   " + t2bone.name);
        originalRotation = transform.localRotation;

       
    }

    void LateUpdate()
    {

     

        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

          //  Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, -Vector3.right);
            Quaternion xBODQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.up);

           // Debug.Log(yQuaternion);
         //   transform.localRotation = originalRotation * xQuaternion * yQuaternion;
          //  t2bone.localRotation = originalRotation * xQuaternion * yQuaternion  ;
            t2bone.localRotation = originalRotation * yQuaternion;

            transform.localRotation = originalRotation * xBODQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

        //    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
         //   transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            t2bone.localRotation = originalRotation * yQuaternion;
        }
    }

    void Start()
    {
  


     //   t2bone = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
     //   Debug.Log("IAM   " + t2bone.name);
        // Make the rigid body not change rotation
       // if (rigidbody)
        //    rigidbody.freezeRotation = true;
      //  originalRotation = transform.localRotation;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}