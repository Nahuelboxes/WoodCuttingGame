using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZoneSpawner : MonoBehaviour
{
    [Header("Gizmo")]
    public Transform center;
    public Vector2 zoneSize = new Vector2(2f, 5f);

    [Header("Grid")]
    public GameObject targetPrefab;
    public Vector2 targetSize = new Vector2(0.5f, 0.5f);
    public Vector2 targetPadding = new Vector2(0.2f, 0.2f);

    public Transform startPoint;
    private Vector3 currPos;
    private int targetIndex =0;


    public List<GameObject> targets = new List<GameObject>();


    void Start()
    {
        CreateTargets();
    }

    public void CreateTargets()
    {
        int colummns = (int)(zoneSize.x / (targetSize.x + targetPadding.x) ) +1;
        int rows = (int)(zoneSize.y / (targetSize.y + targetPadding.y) ) +1;

        currPos = startPoint.position;
        targetIndex = 0;

        for (int x = 0; x < colummns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                var target = Instantiate(targetPrefab, currPos, Quaternion.identity, this.transform);
                target.SetActive(false);

                target.gameObject.name = targetIndex + " Target_ " + x + "," + y;

                targets.Add(target);

                currPos.y -= (targetSize.y + targetPadding.y);

                targetIndex ++;
            }
            currPos.y = startPoint.position.y;
            currPos.x += (targetSize.x + targetPadding.x);
        }

    }

    public void ActivateOneRandom()
    {
        int randomIndex = Random.Range(0, targets.Count);
        if (targets[randomIndex].activeInHierarchy)
        {
            print("Salío Sorteado :" + targets[randomIndex].name +" y ya está activado");
            return;
        }
        targets[randomIndex].SetActive(true);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ActivateOneRandom();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center.transform.position, zoneSize);

      
    }
}