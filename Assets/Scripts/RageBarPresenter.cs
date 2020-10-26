using UnityEngine;
using UnityEngine.UI;

public class RageBarPresenter : MonoBehaviour
{
    public GameObject rageIcon;
    public Image bar;
    public float speed = 5f;
    private float realAmount = 0f;
    void Start()
    {
        
    }

    public void UpdateBar(float amount)
    {
        realAmount = Mathf.Lerp(realAmount, amount, speed * Time.deltaTime);

        bar.fillAmount = realAmount;
    }

    public void ActivateRage()
    {
        rageIcon.SetActive(true);
    }

    public void EndRage()
    {
        rageIcon.SetActive(true);
    }

   
}
