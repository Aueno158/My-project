using UnityEngine;
using UnityEngine.SceneManagement;
public class NotHappy : MonoBehaviour
{
   [SerializeField] 
   private GameObject gameOverScreen;

   public void Playgame()
  {
    SceneManager.LoadScene("Cutscene01");
  }

  public void Exit()
  {
    Application.Quit();
  }
}
