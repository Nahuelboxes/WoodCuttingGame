using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputsManager : MonoBehaviour
{
    public Camera cam;
    public Vector3 touchPos;
    private bool canTouch = true;

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
        if (cam == null)
            cam = Camera.main;
    }


    void Update()
    {
        if (!canTouch) return;
#if UNITY_EDITOR
        DetectMouse();
#elif UNITY_ANDROID
        DetectTouches();
#endif
    }

    void DetectTouches()
    {
        if (Input.touchCount <= 0) return;

        Vector3 p = Input.GetTouch(0).position;
        //p.z = 0f;

        touchPos = cam.ScreenToWorldPoint(p);
        touchPos.z = 0f;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ShootRaycast();
            OnTouch?.Invoke();
        }


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
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);
        if (hit.collider != null)
        {
            //print(hit.point + " Tocaste algo! Luis le decían... "+ hit.collider.gameObject.name);

            var touchable = hit.collider.gameObject.GetComponentInParent<ITouchable>();
            if (touchable != null)
                touchable.OnTouch(touchPos);

        }
    }

    public void DisableTouchesFor(float time)
    {
        DisableTouch();
        StartCoroutine(WaitToEnableTouches(time));

    }

    IEnumerator WaitToEnableTouches(float time)
    {
        float t = 0f;
        while (t < time)
        {
            if (!LvlManager.instance.isPaused)
                t += Time.deltaTime;
            yield return null;
        }

        EnableTouch();
    }

    public void DisableTouch()
    {
        canTouch = false;
    }

    public void EnableTouch()
    {
        canTouch = true;
    }
}
