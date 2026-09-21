using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager2 : MonoBehaviour
{

      [SerializeField]
    private Player p;
    public static GameManager2 Instance ;

    void Awake()
    {
          Instance = this;
    }

    void Start()
    {
    }
   
    void Update()
    {
        
    }


    

}

