using UnityEngine;
using UnityEngine.SceneManagement;
public class Cutscene02 : MonoBehaviour
{
   public void PlayGame()
  {
    SceneManager.LoadScene("Gameplay");
  }
}
