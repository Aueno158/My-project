using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    [SerializeField]
    private Sprite keyCutsceneSprite; 

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        if (player == null)
            return;

        player.HasPostIt = true;   

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowPostItSequence(keyCutsceneSprite);
        }

        Destroy(gameObject);
    }
}