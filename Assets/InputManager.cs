using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private Camera cam;
    public Vector2 inputPos;

    //public UnityEvent OnTouch;
    public delegate void DelTouch(Vector2 pos);
    public static event DelTouch OnTouch;

    public static InputManager instance;
   

    void AwakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Awake()
    {
        AwakeSingleton();
        cam = Camera.main;
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        DetectMouse();    
    }

    void DetectMouse()
    {

        Vector3 p = Input.mousePosition;
        p.z = 0f;

        inputPos = cam.ScreenToWorldPoint(p);

        if (Input.GetMouseButtonDown(0))
        {
            if(OnTouch != null)
                OnTouch(inputPos);
        }

    }
}
