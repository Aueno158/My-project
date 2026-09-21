using UnityEngine;
using UnityEngine.SceneManagement;

public class Happy : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;

    void Start()
    {
        if (Player.Instance != null)
        {
            Player.Instance.SetCursorLocked(false);   // ปลดล็อกเคอร์เซอร์ให้กดปุ่มได้
            Player.Instance.SetCanMove(false);        // หยุดไม่ให้ Player เดินต่อในหน้า Ending
            Player.Instance.SetVisible(false);        // ซ่อนตัว Player ไม่ให้เห็น เหลือแค่ Canvas
        }
    }

 public void Playgame()
    {
        if (Player.Instance != null)
        {
            Player.Instance.SetCursorLocked(false);
            Player.Instance.SetCanMove(true);
            Player.Instance.SetVisible(true);  
        }

        SceneManager.LoadScene("Cutscene01");
    }

    public void Exit()
    {
        Application.Quit();
    }
}