using UnityEngine;
using UnityEngine.UI;

public class ChangeImgButton : MonoBehaviour
{
    public Image imgComp;
    public Sprite normalImg;
    public Sprite changedImg;

    private bool imgChanged = false;

    public void HandleButtonClick()
    {
        if (imgChanged)
            SetNormalImg();
        else
            SetChangedImg();
    }

    public void SetNormalImg()
    {
        imgComp.sprite = normalImg;
        imgChanged = false;
    }

    public void SetChangedImg()
    {
        imgComp.sprite = changedImg;
        imgChanged = true;
    }
}
