using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [Header("Tree Props")]
    public GameObject[] treeParts;
    
    public int treeHeight = 10;
    public float partHeight = 2;
    public Transform basePoint;
    public float baseHeight = 2f;
    private Vector3 currentPos = new Vector3(0, 0, 0);
    [Space]
    public List<GameObject> currentParts = new List<GameObject>();

    [Header("Auto-Tree Props")]
    public float partInitY;
    public float incrementPartY;
    private int maxLogsVisible = 6;

    private void Start() {
        //CreateTree();
        AutoTreeCreate();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            if (!(treeHeight == -(maxLogsVisible)))
            {            
            DeleteOneLogV2();
            }
            // DeleteOneLog();
        }
    }


    [ContextMenu("Create Tree")]
    public void CreateTree() {
        currentPos.y += baseHeight;
        //treeHeight
        for (int i = 0; i < 6; i++)
        {
            var p = Instantiate(treeParts[0], this.transform);
            p.transform.position = currentPos + basePoint.position;
            currentPos.y += partHeight;


            p.name = "Part_" + i.ToString();
            currentParts.Add(p);
        }

        treeHeight = treeHeight - maxLogsVisible;

    }

    public void AutoTreeCreate() {
        currentPos.y += baseHeight;

        for (int i = 0; i < maxLogsVisible; i++)
        {
            int rndmIndex = RandomSelectParts();
            var p = Instantiate(treeParts[rndmIndex], this.transform);
            p.transform.position = currentPos + basePoint.position;
            if (i == 0) {
                currentPos.y += partInitY;
                p.name = "Part_" + i.ToString();
                currentParts.Add(p);
            } else {
                currentPos.y += incrementPartY;
                p.name = "Part_" + i.ToString();
                currentParts.Add(p);
            }


        }
        treeHeight = treeHeight - maxLogsVisible;

    }

    public void DeleteOneLogV2()
    {
        if (treeHeight > 0)
        {
            Destroy(currentParts[0]);
            currentParts.RemoveAt(0);
            treeHeight--;
            OrganizeLogs(true);
           
        }
        else
        {
            Destroy(currentParts[0]);
            currentParts.RemoveAt(0);
            treeHeight--;
            OrganizeLogs(false);
        }

        if (treeHeight <= -(maxLogsVisible))
        {
            print("Ganaste !!!");
        }
  
    }

    private void OrganizeLogs(bool add)
    {
        bool firstEntry = true;
        currentParts.ForEach(logs =>
        {
            Vector3 localPos = transform.localPosition;
            if (firstEntry)
            {
                localPos.y = logs.transform.localPosition.y - partInitY;
                localPos.x = 0;
                localPos.z = 0;
                logs.transform.localPosition = localPos;
                firstEntry = false;
            }
            else
            {
                localPos.y = logs.transform.localPosition.y - incrementPartY;
                localPos.x = 0;
                localPos.z = 0;
                logs.transform.localPosition = localPos;
               
            }
        });
        Vector3 localPosNewLog = transform.localPosition;
        localPosNewLog.y = 36.6706f;
        localPosNewLog.x = 0;
        localPosNewLog.z = 0;
             if (add) {
                int rndmIndex = RandomSelectParts();
                var p = Instantiate(treeParts[rndmIndex], this.transform);
                p.transform.localPosition = localPosNewLog;

                p.name = "Part_" + treeHeight.ToString();
                currentParts.Add(p);
             }
    }

    private int RandomSelectParts()
    {
        int index = Random.Range(0, treeParts.Length);
        return index;

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
