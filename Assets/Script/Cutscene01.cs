using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene01 : MonoBehaviour
{
    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Cutscene02");
    }
}