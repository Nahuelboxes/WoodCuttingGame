using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Sprite soundOn;
    public Sprite soundOff;
    public List<Image> imgs = new List<Image>();
    public bool muted = false;
    public string saveNameMuted = "Muted";
    [Header("BG")]
    public AudioSource BG_AS;
    [Space]
    public AudioStruct startClip;
    [Space]
    public AudioStruct musicClip;

    [Header("SFX")]
    public AudioSource SFX_AS;
    [Space]
    public AudioStruct flyClip;
    public AudioStruct crashClip;
    public AudioStruct getPointClip;
    public AudioStruct changeColorClip;

    public static SoundManager instance;

    void AwakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private float BGVolume;
    private float SFXVolume;

    private void Awake()
    {
        AwakeSingleton();


    }

    public void GetData()
    {
        int savedData = PlayerPrefs.GetInt(saveNameMuted);
        if (savedData == 0 )
        {
            UnMute();
        }
        else 
        {
            Mute();
        }
    }

    public void SoundButtonInteraction()
    {
        if (muted)
            UnMute();
        else
            Mute(); 
    }

    public void Mute()
    {
        BGVolume = BG_AS.volume;
        SFXVolume = SFX_AS.volume;

        //BG_AS.volume = 0f;
        //SFX_AS.volume = 0f;

        BG_AS.mute = true;
        SFX_AS.mute = true;

        foreach (var i in imgs)
        {
            i.sprite = soundOff;
        }
        muted = true;

        PlayerPrefs.SetInt(saveNameMuted, 1);
        PlayerPrefs.Save();
    }

    public void UnMute()
    {
        BG_AS.volume = BGVolume;
        SFX_AS.volume = SFXVolume;

        BG_AS.mute = false;
        SFX_AS.mute = false;

        foreach (var i in imgs)
        {
            i.sprite = soundOn;
        }

        muted = false;

        PlayerPrefs.SetInt(saveNameMuted, 0);
        PlayerPrefs.Save();
    }



    public void PlayStartMusic()
    {
        if (this.muted) return;
        startClip.PlayIn(BG_AS);
    }

    public void PlayGameMusic()
    {
        if (this.muted) return;
        musicClip.PlayIn(BG_AS);
    }

    public void PlayFly()
    {
        if (this.muted) return;
        flyClip.PlayIn(SFX_AS);
    }

    public void PlayGetPoint()
    {
        if (this.muted) return;
        getPointClip.PlayIn(SFX_AS);
    }

    public void PlayChangeColor()
    {
        if (this.muted) return;
        changeColorClip.PlayIn(SFX_AS);
    }

    public void PlayCrash()
    {
        if (this.muted) return;
        crashClip.PlayIn(SFX_AS);
    }

}

[System.Serializable]
public class AudioStruct
{
    public AudioClip clip;
    public float volume=1f;
    public float pitch=1f;

    public void PlayIn(AudioSource AS)
    {
        AS.Stop();
        AS.clip = this.clip;
        AS.volume = this.volume;
        AS.pitch = this.pitch;

        AS.Play();
    }
}
