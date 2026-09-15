using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] bgm;

    [SerializeField]
    private AudioSource[] sfx;

    [SerializeField]
    private AudioMixer mixer;
    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

       void Update()
    {
        
    }
    
    private void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    public void PlayBGM(int i)
    {
        StopAllBGM();
        if (i < bgm.Length)
        {
            bgm[i].Play();
        }
    }

    public void PlaySFX(int i)
    {
        if (i < sfx.Length)
        {
            sfx[i].PlayOneShot(sfx[i].clip);
        }
    }

    public void AdjustMasterVolume(float volume)
    {
        mixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("Master", volume);
        PlayerPrefs.Save();
    }

    public void AdjustSFXVolume(float volume)
    {
        mixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFX", volume);
        PlayerPrefs.Save();
    }

    public float LoadCurrentMasterVol()
    {
       return PlayerPrefs.GetFloat("Master", 0f);
    }

    public float LoadCurrentSFXVol()
    {
       return PlayerPrefs.GetFloat("SFX", 0f);
    }
}