using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
     [SerializeField]
    private TMP_Text KeyText;

        [SerializeField]
    private GameObject restartButton;

    public static GameManager Instance { get; private set; }


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

    public void ShowHideRestartButton(bool flag)
    {
        restartButton.SetActive(flag);
    }

    public void Exit()
    {
        ShowHideRestartButton(false);
        Application.Quit();
    }

}
