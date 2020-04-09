using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Tree Size")]
    public Vector2 treeSize = new Vector2(2f, 5f);

    [Header("Targets")]
    public GameObject targetPrefab;

    public int targetsAmount = 3;
    public List<GameObject> ownTargets = new List<GameObject>();
    //public float minSeparation = 3f;


    //Tree partition
    //public List<TreePart> parts = new List<TreePart>();
    //public float xMargenPercent;

    //Grid
    public GridTree grid = new GridTree();



    public bool completed = false;
   
    void Start()
    {
        
    }

  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //GenerateTargets();
            //DivideTree();
            GridOnTree();
        }
    }

    void GenerateTargets()
    {
        ClearCurrentTargets();

        for (int i = 0; i < targetsAmount; i++)
        {
            var go = Instantiate(targetPrefab, this.transform);
            go.SetActive(false);

            //iteration = 0;
            //go.transform.localPosition = GetNewPos();

            ownTargets.Add(go);
           
        }
        completed = true;
        ActivateTargets();
    }


    /// <summary>
    /// Tirar random hasata que todos los puntos esten en la posicion correcta
    /// </summary>

    //private int iteration = 0;
    //private Vector2 pos;
    //private Vector2 GetNewPos()
    //{
    //    ////Vector2 pos = Vector2.zero;
    //    //Vector2 pos = this.transform.position;

    //    pos.x = Random.Range(-treeSize.x / 2, treeSize.x / 2);
    //    pos.y = Random.Range(-treeSize.y / 2, treeSize.y / 2);

    //    foreach (var item in ownTargets)
    //    {
    //        if (Vector2.Distance(item.transform.position, pos) <= minSeparation)
    //        {
    //            pos = GetNewPos();
    //            iteration++;
    //            //print("Iteration");
    //        }
    //    }

    //    return pos;
    //}

    public void ActivateTargets()
    {
        foreach (var item in ownTargets)
        {
            item.SetActive(true);
        }
    }

    public void ClearCurrentTargets()
    {
        completed = false;
        foreach (var item in ownTargets)
        {
            Destroy(item);
        }
        ownTargets.Clear();
    }


    /// <summary>
    /// Dividir en franjas y usar Perlin Noise para determinar la pos
    /// </summary>
    //public void DivideTree()
    //{
    //    for (int i = 0; i < targetsAmount; i++)
    //    {
    //        var p = new TreePart();
    //        p.YSize = treeSize.y / targetsAmount;
    //        //Modify size 

    //        float newCenter = -treeSize.y / 2f;
    //        if (parts.Count >= 1)
    //        {
    //            for (int j = 0; j < i - 1; j++)
    //            {
    //                newCenter += parts[j].YSize;
    //            }
    //        }

    //        p.YCenter= newCenter + p.YSize /2f;

    //        parts.Add(p);
    //    }
    //}

    ///<summary>
    ///Generar una grilla y tirar random para las coordenadas
    ///</summary>
    public void GridOnTree()
    {
        grid.grid = grid.Create();
        //determinar por random donde quiero poner
        // sabiendo que la posicion la puedo determinar por el remap
       
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(this.transform.position, treeSize);

        //for (int i = 0; i < grid.size.x; i++)
        //{
        //    for (int j = 0; j < grid.size.y; j++)
        //    {


        //    }
        //}
    }

}

//[System.Serializable]
//public class TreePart
//{
//    //public float YSize;
//    //public float YTop;
//    //public float YCenter;
//    //public float YBottom;

//    //public float XSize;

//}
[System.Serializable]
public class GridTree
{
    public Vector2Int size;
    public Column[] grid;

    public GridTree()
    {
        this.size = new Vector2Int(1, 1);
    }

    public GridTree(Vector2Int size)
    {
        this.size = size;
    }

    public Column[] Create()
    {
        grid = new Column[size.x];
        for (int i = 0; i < size.x; i++)
        {
           
            grid[i] = new Column();
            grid[i].col = new Cell[size.y];

            for (int j = 0; j < size.y; j++)
            {
                grid[i].col[j] = new Cell();
                grid[i].col[j].cord = new Vector2Int(i, j);
                grid[i].col[j].used = false;
            }
        }

        return grid;
    }
}
[System.Serializable]
public class Column
{
    public Cell[] col;
}
[System.Serializable]
public class Cell
{
    public bool used = false;
    public Vector2Int cord;
}
