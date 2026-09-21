using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene02 : MonoBehaviour
{
    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}