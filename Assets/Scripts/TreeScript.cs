using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TreeScript : MonoBehaviour
{
    public WaitForMe syncroAction;

    [Space]
    #region tree
    /*
    [Header("Tree Props")]
    public GameObject[] treeParts;
    public int treeHeight = 10;
    private  int currTreeHeight;
    public int logLeft;
    public Transform basePoint;
    public float partInitY;
    public float incrementPartY;
    public int maxLogsVisible = 6;
    private Vector3 currentPos = new Vector3(0, 0, 0);
    public TreeType typeTree;
    [Space]
    public List<GameObject> currentParts = new List<GameObject>();

    [Space]
    public TargetZoneSpawner targetZone;

    public UnityEvent OnHitCorrectly;
    public UnityEvent OnEndTree;


    [Header("Sync")]
    public WaitForMe waitSys;

    private void Start() {
        
       // CreateTree();
    }

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    HitLowestOne();

        //  //  DeleteOneLog();


        //}
    }


    [ContextMenu("Create Tree")]
    public void CreateTree()
    {
        incrementPartY = typeTree.partHeight;


        currTreeHeight = treeHeight;
        currentPos.y += partInitY;

        logLeft = currTreeHeight;

        for (int i = 0; i < maxLogsVisible; i++)
        {
            int rndmIndex = RandomSelectParts();
            var p = Instantiate(treeParts[rndmIndex], this.transform);
            var partScript = p.GetComponent<LogBehaviour>();  //Refactorizar
            partScript.myTree = this;
            partScript.type = this.typeTree;
            partScript.ChoosePart();
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


        targetZone.CreateTargets();
        // targetZone.InitializeZone();

        CompleteCondition();
    }

    public void DeleteOneLog()
    {
        if (currTreeHeight == -maxLogsVisible)
        {
            print("DEJA DE TALAR SOS TONTITO ? no hay MASSSSSSSS");
            return;
        }
        if (currTreeHeight > 0)
        {
            //   Destroy(currentParts[0]);
            currentParts[0].GetComponent<LogBehaviour>().Delete();
            currentParts.RemoveAt(0);

            logLeft--;

            OrganizeLogs(true);
           
        }
        else
        {
            //  Destroy(currentParts[0]);
            currentParts[0].GetComponent<LogBehaviour>().Delete();
            currentParts.RemoveAt(0);
            currTreeHeight--;

            logLeft--;

            OrganizeLogs(false);
        }

        if (currTreeHeight <= -(maxLogsVisible))
        {
            print("Ganaste !!!");

            OnEndTree.Invoke();
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
        if (add)
        {
                int rndmIndex = RandomSelectParts();
                var p = Instantiate(treeParts[rndmIndex], this.transform);
                var partScript = p.GetComponent<LogBehaviour>();  //Refactorizar
                partScript.myTree = this;
                partScript.type = this.typeTree;
                partScript.ChoosePart();
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

    public void HitLowestOne()
    {
        print("Le diste !");
        currentParts[0].GetComponent<LogBehaviour>().ReceiveHit();

        OnHitCorrectly.Invoke();
        //Deberia llamar a un manager del nivel que deberia controlar los valores del arbol y las animaciones del Leñador
    }

    
    */
    #endregion
    public bool treeCreated = false;
    public Transform basePoint;
    public int partsInScreen = 10;
    public float partSizeY; // take from tree type
    [SerializeField] private int partLeft = 100;
    private int totalTreeSizeInParts;
    public int TotalTreeSizeInParts
    { get { return totalTreeSizeInParts; } }

    public List<GameObject> treeParts = new List<GameObject>();

    public GameObject samplePart;

    public UnityEvent OnHitCorrectly;
    public UnityEvent OnEndTree;

    public TargetZoneSpawner targetZone;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    CutLowest();
        //}
    }

    public void CreateTree(LevelInfo lvlInfo, int treesize)
    {
        //Clean last tree first
        CleanTree();
       
        //Create and add
        for (int i = 0;
            (partsInScreen > treesize) ? i < treesize : i < partsInScreen;
            i++)
        {
            //Replace by pool... or not
            GameObject p = Instantiate(samplePart, 
                basePoint.transform.position + new Vector3(0, partSizeY*i + partSizeY/2, 0) ,
                Quaternion.identity, basePoint);

            //Set Up Log Prefab
            p.GetComponent<LogBehaviour>().SetUpLog(lvlInfo.lvlTypeInfo.treeType);
            //Fill the list from bottom to top
            treeParts.Add(p);

            p.GetComponent<LogBehaviour>().myTree = this;
        }
        totalTreeSizeInParts = treesize;
        partLeft = treesize;

        treeCreated = true;

        if (syncroAction != null)
        {
            syncroAction.CompleteAction();
        }
    }

    //call when player touch a target
    [ContextMenu ("Hit Lowest")]
    public void CutLowest()
    {
        //remove and desable first One
        var lowestPart = treeParts[0];
        lowestPart.GetComponent<LogBehaviour>().GetHit();
        treeParts[0].SetActive(false);
        
        treeParts.RemoveAt(0);

        if (partLeft > partsInScreen) // put back on top
        {
            treeParts.Add(lowestPart);
            lowestPart.SetActive(true);
        }
        partLeft--;

        if (partLeft > 0)
            OrganizeParts();
        else
        {
            Debug.Log("Ganaste!!");
            OnEndTree.Invoke();
        }

        LvlManager.instance.OnCorrectHit();
        OnHitCorrectly?.Invoke();
        //print("This tree has " + partLeft + " parts left.");
    }

    void OrganizeParts()
    {
        for (int i = 0; i < treeParts.Count; i++)
        {
            Vector3 newPos = basePoint.transform.position + new Vector3(0, partSizeY * i + partSizeY / 2, 0);
            treeParts[i].transform.position= newPos;
        }
    }

    public void CleanTree()
    {
        foreach (var item in treeParts)
        {
            Destroy(item);
        }
        treeParts.Clear();

        partLeft = 100000;
        treeCreated = false;

        targetZone.ClearTreeTargets();
    }

    public int GetPartsLeft()
    {
        return partLeft;
    }

    //Call when player touch the tree, not a target
    public void OnTouchTree()
    {
        if (LvlManager.instance.inRageMode)
        {
            OnRageTouch();
            return;
        }

        targetZone.HandleTreeTouch();
    }

    //RAGE
    public void HandleStartRage()
    {
        targetZone.HandleStartRage();
    }

    public void OnRageTouch()
    {
        targetZone.HandleRageTouch();
        CutLowest();
    }

    public void HandleEndRage()
    {
        targetZone.HandleEndRage();
    }

    public void HideAllTargets()
    {
        //targetZone.currGameMode.
        //targetZone.Hide();
    }


    //#region Syncro
    ////Sync
    //public void CompleteCondition()
    //{
    //    if (waitSys != null)
    //    { waitSys.ConditionCompleted(); }
    //    else
    //    {
    //        Debug.LogAssertion("There is no Wait System Assing!");
    //    }
    //}

    //public void GetSyncroSystem()
    //{
    //    throw new System.NotImplementedException();
    //}
    //#endregion




}
