using UnityEngine;
using UnityEngine.SceneManagement;
public class Finish : MonoBehaviour
{
   [SerializeField] 
   private GameObject gameOverScreen;

   public static Finish Instance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) 
            return;
            else if ( player.HasKey )
            {
                    SceneManager.LoadScene("Ending01");
                }
            else if (!player.HasKey)
            {
               SceneManager.LoadScene("Lostscene");
            }
    }


}
