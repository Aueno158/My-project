using UnityEngine;
using UnityEngine.SceneManagement;
public class Cutscene01 : MonoBehaviour
{
   public void NextScene()
  {
    SceneManager.LoadScene("Cutscene02");
  }
}
