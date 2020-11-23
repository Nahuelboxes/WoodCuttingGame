using UnityEngine;
using UnityEngine.UI;

public class DropDownButton : MonoBehaviour
{
    public Button myButton;
    public Animator anim;

    private bool showingPanel = false;

    private bool inTransition = false;

    private void OnEnable()
    {
        ResetPos();
    }

    public void HandleButtonClick()
    {
        if (!showingPanel)
            Show();
        else
            Hide();
    }

    void Show()
    {
        showingPanel = true;
        anim.SetBool("Panel", showingPanel);
        inTransition = true;

        DisableButtonFor(0.05f);
    }

    void Hide()
    {
        showingPanel = false;
        anim.SetBool("Panel", showingPanel);
        inTransition = true;

        DisableButtonFor(0.05f);
    }

    void ResetPos()
    {
        showingPanel = false;
        anim.CrossFade("SmallPanelHiden", 0f);
        inTransition = false;
    }

    void DisableButtonFor(float t)
    {
        myButton.interactable = false;
        Invoke("EnableButton", t);
    }

    void EnableButton()
    {
        myButton.interactable = true;

        inTransition = false;
    }

}
