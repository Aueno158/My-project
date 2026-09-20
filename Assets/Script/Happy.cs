using UnityEngine;

public class Happy : MonoBehaviour
{

    [SerializeField] 
   private GameObject gameOverScreen;

   public static Happy Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowHideGameOverScreen(true);
        }
    }

     public void ShowHideGameOverScreen(bool flag)
    {
        gameOverScreen.SetActive(flag);
        return;
    }
}