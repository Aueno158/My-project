using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] bgm;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioMixer mixer;


    private const string MasterKey = "Master";
    private const string SFXKey = "SFX";

    private void Awake()
    {
      
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
      
        if (instance != this) return;

        ApplyVolume(MasterKey, LoadCurrentMasterVol());
        ApplyVolume(SFXKey, LoadCurrentSFXVol());
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

 
    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i] != null)
                bgm[i].Stop();
        }
    }

    public void PlayBGM(int i)
    {
        StopAllBGM();

        if (i >= 0 && i < bgm.Length && bgm[i] != null)
            bgm[i].Play();
    }

    public void PlaySFX(int i)
    {
        if (i >= 0 && i < sfx.Length && sfx[i] != null)
            sfx[i].PlayOneShot(sfx[i].clip);
    }

    // ---------------------------------------------------------------
    //  Volume (หน่วยเป็น dB เหมือนเดิม)
    // ---------------------------------------------------------------
    public void AdjustMasterVolume(float volume)
    {
        ApplyVolume(MasterKey, volume);
        SaveVolume(MasterKey, volume);
    }

    public void AdjustSFXVolume(float volume)
    {
        ApplyVolume(SFXKey, volume);
        SaveVolume(SFXKey, volume);
    }

    public float LoadCurrentMasterVol()
    {
        return PlayerPrefs.GetFloat(MasterKey, 0f);
    }

    public float LoadCurrentSFXVol()
    {
        return PlayerPrefs.GetFloat(SFXKey, 0f);
    }

    private void ApplyVolume(string key, float volume)
    {
        if (mixer != null)
            mixer.SetFloat(key, volume);
    }

    private void SaveVolume(string key, float volume)
    {
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
    }
}