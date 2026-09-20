using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    public GameObject[] myObjects;
    public int spawnCount = 8; // จำนวนกล่องที่ต้องการสุ่มเกิด

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // ตรวจสอบว่ามี Prefab ในอาเรย์หรือไม่
        if (myObjects == null || myObjects.Length == 0)
        {
            Debug.LogWarning("กรุณาใส่ Prefab ลงในอาเรย์ myObjects ก่อน!");
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            // สุ่มเลือก Object จากอาเรย์
            int randomIndex = Random.Range(0, myObjects.Length);

            // คำนวณตำแหน่งสุ่มตามสเกลแมพ (X: 60, Z: 150)
            float randomX = Random.Range(-30f, 30f);
            float randomZ = Random.Range(-75f, 75f);
            float spawnY = 10f; // ความสูงในการเกิด

            Vector3 randomSpawnPosition = new Vector3(randomX, spawnY, randomZ);

            // สร้าง Object ลงในซีน
            Instantiate(
                myObjects[randomIndex],
                randomSpawnPosition,
                Quaternion.identity
            );
        }
    }
}