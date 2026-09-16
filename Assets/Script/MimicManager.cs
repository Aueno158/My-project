using UnityEngine;

public class MimicManager : MonoBehaviour
{
      private MeshRenderer rd;
    public static MimicManager instance; 

    [Header("ใส่รูปกุญแจที่ต้องการโชว์")]
    public GameObject keyResultObject; 

    void Awake()
    {
        // กำหนดให้ instance คือตัวมันเองตอนที่เกมเริ่ม
        instance = this; 
    }

    void Start()
    {
         rd = GetComponent<MeshRenderer>();
        // ซ่อนกุญแจไว้ก่อนตอนเริ่มเกม
        if (keyResultObject != null) 
        {
            keyResultObject.SetActive(false);
        }
    }

    public void FoundKey()
    {
        if (keyResultObject != null)
        {
            keyResultObject.SetActive(true); // โชว์กุญแจ
        }
    }
}