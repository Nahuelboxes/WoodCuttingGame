using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITouchable
{
    public SpriteRenderer[] SRs;
    public int orderInLayer = 10;
    public Collider2D[] Colls;

    [Space]
    public float touchDistCenter;


   
    void Start()
    {
        SRs = this.GetComponentsInChildren<SpriteRenderer>();
        GetAllSpriteOnLayer(orderInLayer);

        Colls = this.GetComponentsInChildren<Collider2D>();
        SetCollsNames();
    }


   
    void Update()
    {
        
    }

    //Handle Touch
    public void OnTouch(Vector3 touchPos)
    {
        touchDistCenter = Vector3.Distance(this.transform.position, touchPos);
        print("Distance to center: " + touchDistCenter);
    }


    //Just Config
    public void GetAllSpriteOnLayer(int layerNumber)
    {
        foreach (var item in SRs)
        {
            item.sortingOrder = layerNumber;
        }
    }

    public void SetCollsNames()
    {
        for (int i = 0; i < Colls.Length; i++)
        {
            Colls[i].gameObject.name = this.gameObject.name + " " + i;
        }

    }

}
