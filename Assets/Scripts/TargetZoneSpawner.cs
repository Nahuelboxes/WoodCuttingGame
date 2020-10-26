using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZoneSpawner : MonoBehaviour
{
    public TreeScript treeInfo;
    public gameMode currGameMode;
    public GameMode_TargetZone selectedMode;

    [Header("Normal Mode")]
    public Normal_GameMode normalMode;

    [Header("Simon Mode")]
    public SimonSays_GameMode simonMode;
    


    //[Header("Rain Mode")]
    //public Transform spawner;
    //public float rainSpawnRate;
    //public float targetSpeed = 2f;


    [Header("Gizmo")]
    public Transform center;
    public Vector2 zoneSize = new Vector2(2f, 5f);

    [Header("Grid")]
    public GameObject targetPrefab;
    public Vector2 targetSize = new Vector2(0.5f, 0.5f);
    public Vector2 targetPadding = new Vector2(0.2f, 0.2f);

    public Transform startPoint;
    private Vector3 currPos;
    private int targetIndex = 0;

    private bool inRageMode = false;

    public List<GameObject> targets = new List<GameObject>();

    void Start()
    {
       
    }

    public void InitializeZone()
    {
        CreateTargets();
        switch (currGameMode)
        {
            case gameMode.normal:
                selectedMode = normalMode;
                break;


            case gameMode.simon:
                selectedMode = simonMode;
                break;


            default:
                break;
        }


        selectedMode.SetUpMode();
    }

    //Set Up
    public void CreateTargets()
    {
        int colummns = (int)(zoneSize.x / (targetSize.x + targetPadding.x)) + 1;
        int rows = (int)(zoneSize.y / (targetSize.y + targetPadding.y)) + 1;

        currPos = startPoint.position;
        targetIndex = 0;

        for (int x = 0; x < colummns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var target = Instantiate(targetPrefab, currPos, Quaternion.identity, this.transform);
                target.SetActive(false);

                target.gameObject.name = targetIndex + " Target_ " + x + "," + y;

                var targetScript = target.GetComponent<Target>();
                targetScript.targetZone = this;
                targetScript.index = targetIndex;

                targets.Add(target);

                currPos.y -= (targetSize.y + targetPadding.y);

                targetIndex++;
            }
            currPos.y = startPoint.position.y;
            currPos.x += (targetSize.x + targetPadding.x);
        }

    }

    ////Random Target---> consider recursion
    //public void ActivateOneRandom()
    //{
    //    int randomIndex = Random.Range(0, targets.Count);
    //    if (targets[randomIndex].activeInHierarchy)
    //    {
    //        print("Salío Sorteado :" + targets[randomIndex].name + " y ya está activado");
    //        return;
    //    }
    //    targets[randomIndex].SetActive(true);
    //}

    //public void CoroutineActivateTargetRandom()
    //{
    //    StartCoroutine(ActivateTargetRandom());
    //}

    //public GameObject lastActivated;
    //IEnumerator ActivateTargetRandom()
    //{
    //    lastActivated = null;

    //    int iterationCount = 0;
    //    int randomIndex = Random.Range(0, targets.Count);
    //    while (targets[randomIndex].activeInHierarchy && iterationCount <= targets.Count)
    //    {
    //        //print("Salío Sorteado: " + targets[randomIndex].name + " y ya está activado");
    //        //get other random index
    //        randomIndex = Random.Range(0, targets.Count);

    //        iterationCount++;
    //        yield return null;
    //    }
    //    if (iterationCount > targets.Count)
    //    {
    //        print("Too many iterations (" + iterationCount + "). Check if there are enought targets in the list");
    //    }
    //    else
    //    {
    //        //print("Shall activate :" + targets[randomIndex].name + " with " + iterationCount + " iterations");
    //        targets[randomIndex].SetActive(true);
    //        //normalMode.normalInScreen.Add(targets[randomIndex]);
    //    }

    //    yield return null;

    //}

    public int GetRandomIndex()
    {
        return Random.Range(0, targets.Count);
    }

    public GameObject GetRandomTarget()
    {
        return targets[Random.Range(0, targets.Count)];
    }

    public void ClearTreeTargets()
    {
        selectedMode.CleanTargets();
    }

    //Handle Touch
    public void HandleTargetTouch(GameObject targetObj)
    {
        if (inRageMode) return;

        bool shallHit = false;

        selectedMode.HandleTargetTouch(targetObj, out shallHit);

        if (shallHit)
        {
            treeInfo.CutLowest();
        }
    }

    public void HandleTreeTouch()
    {
        selectedMode.HandleTreeTouch();
    }

    public void HandleStartRage()
    {
        inRageMode = true;
        selectedMode.HandleStartRage();
    }

    public void HandleRageTouch()
    {
        selectedMode.HandleRageTouch();
    }

    public void HandleEndRage()
    {
        inRageMode = false;
        selectedMode.HandleEndRage();
    }


    public void FinishGame()
    {
        selectedMode.EndTree();
    }

    //public void Hide()
    //{
    //    foreach (var item in targets)
    //    {
    //        item.SetActive(false);
    //    }
    //}




    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    InitializeZone();
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center.transform.position, zoneSize);

      
    }
}


public enum gameMode
{
    normal,
    simon,
    //rain,
    //autoDestroy,
}