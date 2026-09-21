using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
    {
      Player player =  other.gameObject.GetComponent<Player>();
      if(player == null)
         return;
         player.HasKey = true;
      GameManager.Instance.ShowNotiText("Key collected!");

      Destroy(gameObject);
    }

}