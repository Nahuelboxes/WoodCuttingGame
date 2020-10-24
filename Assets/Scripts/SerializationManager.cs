using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializationManager : MonoBehaviour
{
    [Tooltip("Level save key is form by lvlType + suffix")]
    public const string lvlSuffix = "_lvl";
    public const string currencyName = "currency";

    //Singleton
    public static SerializationManager instance;
    void AwakeSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Awake()
    {
        AwakeSingleton();
    }

    #region Levels
    public int LoadCurrentLvl(lvlType lvlType)
    {
        int lvl = 1;
        string saveKey = lvlType.ToString() + lvlSuffix;
        if (PlayerPrefs.HasKey(saveKey))
        {
            lvl = PlayerPrefs.GetInt(saveKey);
        }
        
        if(lvl < 1)
            lvl = 1;

        return lvl;
    }

    public void SaveCurrentLvl(lvlType lvlType, int currLvl)
    {
        string saveKey = lvlType.ToString() + lvlSuffix;

        if (currLvl < 1) 
            currLvl = 1;

        PlayerPrefs.SetInt(saveKey, currLvl);
        PlayerPrefs.Save();
    }

    //TODO: Resets
    #endregion


    #region Currency
    public int LoadCurrency()
    {
        int amount = 0;
        if (PlayerPrefs.HasKey(currencyName))
        {
            amount = PlayerPrefs.GetInt(currencyName);
        }
        return amount;
    }

    public void SaveCurrency(int amount)
    {
        PlayerPrefs.SetInt(currencyName, amount);
        PlayerPrefs.Save();
    }
    #endregion

}
