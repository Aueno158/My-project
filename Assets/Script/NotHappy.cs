using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NotHappy : MonoBehaviour
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

        foreach (var element in elementsToFade)
        {
            if (element.canvasGroup != null)
            {
                element.canvasGroup.alpha = 0f;
                element.canvasGroup.interactable = false;
                element.canvasGroup.blocksRaycasts = false;
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
        cg.interactable = true;
        cg.blocksRaycasts = true;
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
        Debug.Log("Exit button pressed");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}