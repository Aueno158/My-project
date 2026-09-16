using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

            [SerializeField]
    public GameObject cam;

     [SerializeField]
    private GameObject restartButton;

    [SerializeField]
    private int key;

    private Vector3 cameraOffset = new Vector3(0f, 1.0f, 0f);

 [SerializeField]
    private TMP_Text notext;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
   private void LateUpdate()
    {
        if (cam != null && player != null)
        {

            cam.transform.position = player.transform.position + cameraOffset;

        }
    }
      public void ShowNotext( int k)
    {
        key += k ;
        notext.text =$"key:0/ {k}\n " ;

    }

     public void ShowHideRestartButton(bool flag)
    {
        restartButton.SetActive(flag);
    }

     public void LostWay()
    {
        SceneManager.LoadScene("Lostscene");
    }
    public void HappyEnding()
    {
        SceneManager.LoadScene("Ending01");
    }
        public void Exit()
    {
       restartButton.SetActive(false);
        Application.Quit();
    }

}
