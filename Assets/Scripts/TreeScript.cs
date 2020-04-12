using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [Header("Tree Props")]
    public GameObject[] treeParts;
    public int treeHeight = 10;
    private int currTreeHeight;
    public Transform basePoint;
    public float partInitY;
    public float incrementPartY;
    public int maxLogsVisible = 6;
    private Vector3 currentPos = new Vector3(0, 0, 0);
    [Space]
    public List<GameObject> currentParts = new List<GameObject>();
        
    

    private void Start() {
        
        CreateCreate();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            if (!(treeHeight == -(maxLogsVisible)))
            {
              DeleteOneLog();
            }
            
        }
    }


    [ContextMenu("Create Tree")]
    
    public void CreateCreate() {
        currTreeHeight = treeHeight;
        currentPos.y += partInitY;

        for (int i = 0; i < maxLogsVisible; i++)
        {
            int rndmIndex = RandomSelectParts();
            var p = Instantiate(treeParts[rndmIndex], this.transform);
            p.transform.position = currentPos + basePoint.position;
            if (i == 0) {
                currentPos.y += incrementPartY;
                p.name = "Part_" + (treeHeight - currTreeHeight).ToString();
                currentParts.Add(p);
            } else {
                currentPos.y += incrementPartY;
                p.name = "Part_" + (treeHeight - currTreeHeight).ToString();
                currentParts.Add(p);
            }

            currTreeHeight--;
        }
        

    }

    public void DeleteOneLog()
    {
        if (currTreeHeight > 0)
        {
            Destroy(currentParts[0]);
            currentParts.RemoveAt(0);
            
            OrganizeLogs(true);
           
        }
        else
        {
            Destroy(currentParts[0]);
            currentParts.RemoveAt(0);
            currTreeHeight--;
            OrganizeLogs(false);
        }

        if (currTreeHeight <= -(maxLogsVisible))
        {
            print("Ganaste !!!");
        }
  
    }

    private void OrganizeLogs(bool add)
    {
      currentParts.ForEach(logs =>
        {
            Vector3 localPos = transform.localPosition;
      
                localPos.y = logs.transform.localPosition.y - incrementPartY;
                localPos.x = 0;
                localPos.z = 0;
                logs.transform.localPosition = localPos;
     
        });
        Vector3 localPosNewLog = transform.localPosition;
        localPosNewLog.y = (maxLogsVisible*incrementPartY)-incrementPartY/2 ;
        localPosNewLog.x = 0;
        localPosNewLog.z = 0;
             if (add) {
                int rndmIndex = RandomSelectParts();
                var p = Instantiate(treeParts[rndmIndex], this.transform);
                p.transform.localPosition = localPosNewLog;

                p.name = "Part_" + (treeHeight - currTreeHeight).ToString();
                currentParts.Add(p);
                currTreeHeight--;
        }
    }

    private int RandomSelectParts()
    {
        int index = Random.Range(0, treeParts.Length);
        return index;

    }

    













}
