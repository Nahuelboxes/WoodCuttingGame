using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class VibrationManager : MonoBehaviour
{
    #region Variables
        public bool enableVibration = true;
        public GameObject switchBtn;

    #endregion
    #region Singleton
    public static VibrationManager instance;
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
    #endregion

    void Awake()
    {
        AwakeSingleton();
    }

    public void Vibrate(vibrationDuration vibdur)
    {
        if (enableVibration)
        {
            Vibration.Vibrate(ConvertoMs(vibdur));
        }
    }

    private long ConvertoMs(vibrationDuration vibdur)
    {
        switch (vibdur)
        {
            case vibrationDuration.small:
                return 100;
                break;
            case vibrationDuration.medium:
                return 350;
                break;
            case vibrationDuration.large:
                return 800;
                break;
            case vibrationDuration.xlarge:
                return 1200;
                break;

            default:
                return 100;
        }
    }

    void OnDestroy()
    {
        Vibration.Cancel();
    }

   public void OnToggleVibration(){
        int statesign;
        switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x, 0.2f);
        statesign = Math.Sign(-switchBtn.transform.localPosition.x);
         Debug.Log("el estado, " + statesign );
         if (statesign == 1){
             enableVibration = true;
         }else{
             enableVibration = false;
         }
    }
}
public enum vibrationDuration {small, medium, large, xlarge,}