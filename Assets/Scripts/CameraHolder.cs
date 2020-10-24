using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Camera cam;
    public float camFieldOfView = 50f;

    private void OnEnable()
    {
       
    }

    public void AdaptCamera(Camera c)
    {
        c.gameObject.transform.position = this.transform.position;
        cam = c;
        c.orthographicSize = camFieldOfView;
    }

    public void OnDrawGizmos()
    {
        //Draw frustrum example
        //Gizmos.DrawFrustum(new Vector3(this.transform.position.x, this.transform.position.y, cam.nearClipPlane),
        //    cam.fieldOfView,
        //    cam.farClipPlane,
        //    cam.nearClipPlane,
        //    cam.aspect);

        //print(cam.aspect); //width divided by height
    }
}
