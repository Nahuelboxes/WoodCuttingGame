using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputsManager : MonoBehaviour
{
    public Camera cam;
    public Vector3 touchPos;

    [Space]
    public int mask;


    //public delegate void DelInputsManager();
    //public static event DelInputsManager OnTouch;

    public UnityEvent OnTouch;


    public static InputsManager instance;
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
    }


    void Start()
    {
        cam = Camera.main;
    }

   
    void Update()
    {
        DetectMouse();
    }

    void DetectMouse()
    {

        Vector3 p = Input.mousePosition;
        //p.z = 0f;

        touchPos = cam.ScreenToWorldPoint(p);
        touchPos.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            ShootRaycast();
            OnTouch?.Invoke();
        }

    }

    void ShootRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f);
        if (hit.collider != null )
        {
            print(hit.point + " Tocaste algo! Luis le decían... "+ hit.collider.gameObject.name);

            var touchable = hit.collider.gameObject.GetComponentInParent<ITouchable>();
            if(touchable != null)
                touchable.OnTouch(touchPos);

        }
    }
}
