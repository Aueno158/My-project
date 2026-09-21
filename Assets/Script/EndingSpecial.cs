using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingSpecial : MonoBehaviour
{
    [System.Serializable]
    public class FadeElement
    {
        public CanvasGroup canvasGroup;   // ลาก Image / Title / Button ที่มี Canvas Group มาใส่
        public float delay = 0f;          // หน่วงกี่วินาทีก่อนเริ่มเฟดตัวนี้
    }

    [SerializeField]
    private FadeElement[] elementsToFade;   // ใส่ได้หลายตัว เช่น ภาพ, ไตเติ้ล, ปุ่ม

    [SerializeField]
    private float fadeDuration = 1.5f;      // แต่ละตัวใช้เวลาเฟดกี่วินาที

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;   // ปลดล็อกเคอร์เซอร์
        Cursor.visible = true;                    // ให้เห็นเมาส์อีกครั้ง

        foreach (var element in elementsToFade)
        {
            if (element.canvasGroup != null)
                element.canvasGroup.alpha = 0f;   // ซ่อนทุกตัวไว้ก่อนเริ่ม
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
        yield return new WaitForSeconds(delay);   // รอตามเวลาที่ตั้งไว้ก่อนเริ่มเฟดตัวนี้

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        cg.alpha = 1f;
    }

    public void Playgame()
    {
        SceneManager.LoadScene("Cutscene01");
    }

    public void Exit()
    {
        Application.Quit();
    }
}