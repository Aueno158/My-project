using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        if (player == null)
            return;

        player.HasKey = true;
      

        Destroy(gameObject);

    }
}
