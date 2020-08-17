using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBehaviour : MonoBehaviour, ITouchable
{
    public TreeType type;
    [SerializeField]
    private SpriteRenderer SR;
    [SerializeField]private int hitsToDestroy;
    private int hitsLeft;
    public GameObject explosionPrefab;
    public Vector3 offset;
    public TreeScript myTree;



    //public void ChoosePart()
    // {
    //     hitsToDestroy = type.hitsToDestroyLog;
    //     hitsLeft = hitsToDestroy;
    //     var sr = this.GetComponent<SpriteRenderer>();
    //     sr.sprite = type.parts[Random.Range(0, type.parts.Count)];
    // }

    public void SetUpLog(TreeType type)
    {
        SR.sprite = type.parts[Random.Range(0, type.parts.Count)];
        
    }

    public void GetHit()
    {
        var explosion = Instantiate(explosionPrefab, this.transform.position + offset, Quaternion.identity);
    }

    public void OnTouch(Vector3 touchPos)
    {
        print("Player touch a Log");
        //tell tree a log has been touch
        myTree.OnTouchTree();
        //tree shall call the game mode
        //the game mode decide what to do
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position + offset, 0.4f);
    }
}

