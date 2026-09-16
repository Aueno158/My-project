using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    void OnMouseDown()
    {
        // ตรวจสอบว่ามี MimicManager อยู่ในฉากหรือไม่ เพื่อป้องกัน Error
        if (MimicManager.instance != null)
        {
            // สั่งให้ MimicManager ทำงานฟังก์ชัน FoundKey
            MimicManager.instance.FoundKey();
            
            // ปิดการทำงานของ Collider ตัวนี้ทิ้งไปเลย เพื่อไม่ให้ผู้เล่นคลิกซ้ำได้อีก
            GetComponent<Collider2D>().enabled = false;
        }
    }// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
