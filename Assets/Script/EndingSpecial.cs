using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingSpecial : MonoBehaviour
{
    [System.Serializable]
    public class FadeElement
    {
        public CanvasGroup canvasGroup;
        public float delay = 0f;
    }

    [SerializeField]
    private FadeElement[] elementsToFade;

    [SerializeField]
    private float fadeDuration = 1.5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (Player.Instance != null)
        {
            Player.Instance.SetCursorLocked(false);   // ปลดล็อกเคอร์เซอร์ให้กดปุ่มได้
            Player.Instance.SetCanMove(false);        // หยุดไม่ให้ Player เดินต่อในหน้า Ending
            Player.Instance.SetVisible(false);        // ซ่อนตัว Player ไม่ให้เห็น เหลือแค่ Canvas
        }

        foreach (var element in elementsToFade)
        {
            if (element.canvasGroup != null)
            {
                element.canvasGroup.alpha = 0f;
                element.canvasGroup.blocksRaycasts = false;   // กันไม่ให้บังคลิกตอน alpha ยังเป็น 0
                element.canvasGroup.interactable = false;     // กันไม่ให้ปุ่มข้างในกดได้ก่อน fade เสร็จ
            }
        }

        StartCoroutine(FadeAllIn());
    }

    IEnumerator FadeAllIn()
    {
        foreach (var element in elementsToFade)
        {
            if (element.canvasGroup != null)
                StartCoroutine(FadeSingle(element.canvasGroup, element.delay));
        }
        yield return null;
    }

    IEnumerator FadeSingle(CanvasGroup cg, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        cg.alpha = 1f;
        cg.blocksRaycasts = true;     // fade เสร็จแล้ว เปิดให้รับคลิกได้
        cg.interactable = true;
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