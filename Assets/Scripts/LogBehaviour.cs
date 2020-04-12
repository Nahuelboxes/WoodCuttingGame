using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBehaviour : MonoBehaviour
{
    public Animator animator;
    public GameObject explosionPrefab;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();  


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
