using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;

    public static Finish Instance;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        if (player == null)
            return;

        if (player.HasKey)
        {
            SceneManager.LoadScene("Ending01");
        }
        else
        {
            SceneManager.LoadScene("Lostscene");
        }
    }
}