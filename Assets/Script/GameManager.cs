using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
     [SerializeField]
    private TMP_Text KeyText;

    public static GameManager Instance { get; private set; }

     private int foundKeyCount = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
          Instance = this;
    }

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowKeyText(int n)
    {
        foundKeyCount += n;
        KeyText.text = $"Keys Found: {foundKeyCount}";
    }

     public void ShowString(string text)
    {
        KeyText.text = text;
    }

}
