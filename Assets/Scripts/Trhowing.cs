using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent (typeof (Rigidbody2D))]
public class Trhowing : MonoBehaviour
{
    public bool withRB = false;
    public bool trhownOnEnable = true;

    [Header("RB")]
    public Vector2 direction;
    public float force;
    private Rigidbody2D myRB;

    [Header("Normal")]
    public Vector2 initial = new Vector2(-1, 1);
    public float gravity = -10;
    private Vector2 mov;

    [Header("Rotation")]
    [Tooltip("Only with coroutine")]
    public bool rotate = true;
    public float angularSpeed = -90f;


    //[Header("DoTween")]


    private void OnEnable()
    {
        if (trhownOnEnable)
        {
            if (withRB)
            {
                myRB = this.GetComponent<Rigidbody2D>();
                TrhowWithRB();
            }
            else
            {
                StartCoroutine(ThrowRoutine());
            }

        }
       
    }

    public void TrhowWithRB()
    {
        myRB.AddForce(direction.normalized * force);
    }

    IEnumerator ThrowRoutine()
    {
        mov = initial;
        while (true)
        {
            mov.y += gravity * Time.deltaTime;
            this.transform.Translate(mov);
            if(rotate)
                this.transform.Rotate(0, 0, angularSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (withRB)
        {
            Vector2 myPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Gizmos.DrawLine(myPos,
                myPos + direction.normalized * force);
        }
        else 
        {
            Vector2 myPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Gizmos.DrawLine(myPos,
                myPos + mov);

        }
    }

    //private void Update()
    //{
    //    this.transform.DOMoveX()
    //}

}
