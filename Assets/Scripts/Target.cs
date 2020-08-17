using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITouchable
{
    public TargetZoneSpawner targetZone;
    public gameMode mode;
    public int index;


    [Space]
    public SpriteRenderer[] SRs;
    public int orderInLayer = 10;
    public Collider2D[] Colls;

    public GameObject centerGameObj;
    public TextMesh numberText;

    [Space]
    public float touchDistCenter;

    [Space]
    public gameMode gameModeSelected;
    public int simonOrder=0;


   
    void Start()
    {
        SRs = this.GetComponentsInChildren<SpriteRenderer>();
        GetAllSpriteOnLayer(orderInLayer);

        Colls = this.GetComponentsInChildren<Collider2D>();
        SetCollsNames();
    }

    void OnEnable()
    {
        GetActivate();   
    }

    void GetActivate()
    {
        switch (gameModeSelected)
        {
            case gameMode.normal:

                break;
            case gameMode.simon:

                centerGameObj.SetActive(false);
                numberText.gameObject.SetActive(true);
                numberText.text = simonOrder.ToString ();

                break;
         
            default:
                break;
        }

        if (gameModeSelected != gameMode.simon || !targetZone.simonMode.showNumbers)
        {
            numberText.gameObject.SetActive(false);
            centerGameObj.SetActive (true);
        }
    }


    public void SetUp()
    {
        //get current tree
        //maybe lifetime

    }

   
    void Update()
    {
        
    }

    //Handle Touch
    public void OnTouch(Vector3 touchPos)
    {
        //touchDistCenter = Vector3.Distance(this.transform.position, touchPos);
        //print("Distance to center: " + touchDistCenter);

        targetZone.HandleTargetTouch(this.gameObject);

        //Deactivate
        this.gameObject.SetActive (false);

    }


    //Just Tools
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
