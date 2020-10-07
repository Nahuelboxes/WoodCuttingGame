using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    #region Singleton
    public static VibrationManager instance;
    void AwakeSingleton(){
        if (instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }
  #endregion

    void Awake(){
        AwakeSingleton();
    }

    public void Vibrate(vibrationDuration vibdur){
    Vibration.Vibrate(ConvertoMs(vibdur));
    }
  
    private long ConvertoMs(vibrationDuration vibdur){
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

    void OnDestroy(){
        Vibration.Cancel();
    }                              
}
public enum vibrationDuration {small, medium, large, xlarge,}