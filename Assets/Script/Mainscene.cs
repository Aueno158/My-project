using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainscene : MonoBehaviour
{
    [SerializeField]
    private GameObject adjustPanel;

    [SerializeField]
    private Slider volumeSlider;

void Start()
    {
        volumeSlider.value = AudioManager.instance.LoadCurrentMasterVol();
        AudioManager.instance.PlayBGM(0);
    }

  public void Playgame()
  {
    SceneManager.LoadScene("Cutscene01");
  }

  public void Exit()
  {
    Application.Quit();
  }

  public void ShowHideAdjustPanel(bool flag)
  {
    adjustPanel.SetActive(flag);
  }

  public void SetVolume(float volume)
  {
    AudioManager.instance.AdjustMasterVolume(volume);
  }
}