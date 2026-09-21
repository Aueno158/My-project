using UnityEngine;
using UnityEngine.SceneManagement;

public class Lastway : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        if (player == null)
            return;

        SceneManager.LoadScene("Ending02");
    }
}