using UnityEngine;

public class Mimic : MonoBehaviour
{
    [SerializeField]
    private Sprite mimicCutsceneSprite;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        if (player == null)
            return;

        player.SetCanRun(false);

        if (player.HasKey)
        {
            player.HasKey = false;
            GameManager.Instance.ShowNotiText("The Mimic stole your key!");
        }
        else
        {
            GameManager.Instance.ShowNotiText("Mimic encountered!");
        }

        GameManager.Instance.ShowCutscene(mimicCutsceneSprite, true);   

        Destroy(gameObject);
    }
}