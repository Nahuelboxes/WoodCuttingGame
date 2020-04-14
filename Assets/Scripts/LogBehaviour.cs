using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBehaviour : MonoBehaviour
{
    public TreeType type;
    [SerializeField]private int hitsToDestroy;
    private int hitsLeft;
    public GameObject explosionPrefab;
    public Vector3 offset;
    public TreeScript myTree;
    // Start is called before the first frame update
    void Start()
    {

      //  ChoosePart();

    }

   public void ChoosePart()
    {
        hitsToDestroy = type.hitsToDestroyLog;
        hitsLeft = hitsToDestroy;
        var sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = type.parts[Random.Range(0, type.parts.Count)];
    }

    public void ReceiveHit()
    {
      
        hitsLeft--;
        if (hitsLeft <= 0)
        {
            Delete();
            myTree.DeleteOneLog();
        }

    }

    public void Delete()
    {
        var explosion = Instantiate(explosionPrefab, this.transform.position + offset, Quaternion.identity);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}

