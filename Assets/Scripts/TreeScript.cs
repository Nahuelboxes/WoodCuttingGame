using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
[Header("Tree Props")]
public GameObject[] treeParts;
public int treeHeight=10;
public float partHeight=2;
public Transform basePoint;
public float baseHeight = 2f;
private Vector3 currentPos = new Vector3(0,0,0);
[Space]
public List<GameObject> currentParts = new List<GameObject>();
private void Start(){
CreateTree();

}

private void Update(){
    if (Input.GetKeyDown(KeyCode.D)){

        DeleteOneLog();
    }
}


[ContextMenu("Create Tree")]
public void CreateTree(){
currentPos.y += baseHeight;
//treeHeight
    for (int i = 0; i < 6; i++)
    {
        var p = Instantiate(treeParts[0],this.transform);
        p.transform.position = currentPos + basePoint.position;
        currentPos.y += partHeight;


        p.name = "Part_"+i.ToString();
        currentParts.Add(p);
    } 

treeHeight = treeHeight - 6;

}

public void DeleteOneLog(){

    if (treeHeight > 0){
    Destroy(currentParts[0]);
    currentParts.RemoveAt(0);
    treeHeight--;
    var p = Instantiate(treeParts[0],this.transform);
    p.transform.position = currentPos + basePoint.position;
      
    p.name = "Part_"+ treeHeight.ToString();
    currentParts.Add(p);
    }else{
    Destroy(currentParts[0]);
    currentParts.RemoveAt(0);
    treeHeight--;
    }

    if (treeHeight<=-6){
        print("Ganaste !!!");
    }
    


}














}
