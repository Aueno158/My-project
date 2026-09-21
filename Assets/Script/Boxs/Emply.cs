using UnityEngine;

public class Emply : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        if (player == null)
            return;
        GameManager.Instance.ShowNotiText("Is empty...");
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
