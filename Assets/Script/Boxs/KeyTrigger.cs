using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    [SerializeField]
    private Sprite keyCutsceneSprite;  

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        if (player == null)
            return;

        player.HasKey = true;
        GameManager.Instance.ShowNotiText("Key collected!");
        GameManager.Instance.ShowCutscene(keyCutsceneSprite, false);   

        Destroy(gameObject);

    }
}