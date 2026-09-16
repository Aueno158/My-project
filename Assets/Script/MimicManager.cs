using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI; 

public class MimicManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text notiText;

   [SerializeField]
    private GameObject adjustPanel;
     public static MimicManager Instance;

      void Awake()
    {
        Instance = this;
    }

     public void ShowNotiText(string s)
    {
        notiText.text = s;
    }

     public void ShowHideAdjustPanel(bool flag)
  {
    adjustPanel.SetActive(flag);
  }


}