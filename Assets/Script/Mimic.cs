using UnityEngine;
using UnityEngine.SceneManagement;
public class Mimic : MonoBehaviour
{
     private MeshRenderer rd;

       [SerializeField]
    private GameObject gameOverScreen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHideGameOverScreen(bool flag)
    {
        gameOverScreen.SetActive(flag);
        return;
    }

     public void Nextscene()
  {
    SceneManager.LoadScene("Lostscene");
  }

}
