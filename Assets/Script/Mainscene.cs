using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainscene : MonoBehaviour
{
    [SerializeField]
    private GameObject adjustPanel;

void Start()
    {
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
}