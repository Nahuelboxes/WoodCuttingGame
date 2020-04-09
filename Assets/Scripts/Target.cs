using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float distance;


    public float maxRadius = 0.5f;
    public float centerRadius = 0.5f;

    private void OnEnable()
    {
        InputManager.OnTouch += GetDistance;
    }

    private void OnDisable()
    {
        InputManager.OnTouch -= GetDistance;
    }

    private void GetDistance(Vector2 pos)
    {
        distance = Vector3.Distance(this.transform.position, pos);
        //print(distance);
        if (distance > maxRadius)
        {
            //print("Out of range. Distance= " + distance);
            return;
        }
        else
        {
            if (distance <= centerRadius)
            {
                //print("Bullseye!  centerRadius: " + centerRadius + " Distance: " + distance);
            }
            else
            {
                //print("Just a normal hit");
            }

        }

    }


    private void OnDrawGizmos()//Selected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, maxRadius);


        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, centerRadius);
    }
}
