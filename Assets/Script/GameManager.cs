using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]
    private TMP_Text KeyText;

    [SerializeField]
    private Player player;

    [SerializeField]
    private Image cutsceneImage;

    [Header("Cutscene Settings")]
    [SerializeField]
    private float cutsceneDuration = 2f;

    [SerializeField]
    private float shakeDuration = 0.3f;

    [SerializeField]
    private float shakeMagnitude = 15f;

    [Header("Timer")]
    [SerializeField]
    private float timeLimit = 60f;

    [SerializeField]
    private TMP_Text timerText;

    private float currentTime;
    private bool timerRunning = false;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        if (cutsceneImage != null)
            cutsceneImage.gameObject.SetActive(false);
    }

    void Start()
    {
        currentTime = timeLimit;
        timerRunning = true;
    }

    void Update()
    {
        if (!timerRunning)
            return;

        currentTime -= Time.deltaTime;

        if (timerText != null)
        {
            int displaySeconds = Mathf.CeilToInt(Mathf.Max(currentTime, 0f));
            timerText.text = displaySeconds.ToString();
        }

        if (currentTime <= 0f)
        {
            timerRunning = false;
            TimeUp();
        }
    }

    private void TimeUp()
    {
        if (player != null && !player.HasKey)
        {
            SceneManager.LoadScene("LostScene");
        }
    }

    public void ShowNotiText(string s)
    {
        KeyText.text = s;
    }

    public void ShowCutscene(Sprite cutsceneSprite, bool shake = false)
    {
        StartCoroutine(CutsceneRoutine(cutsceneSprite, shake));
    }

    private IEnumerator CutsceneRoutine(Sprite cutsceneSprite, bool shake)
    {
        Time.timeScale = 0f;
        if (player != null)
            player.SetCanMove(false);

        cutsceneImage.sprite = cutsceneSprite;
        cutsceneImage.gameObject.SetActive(true);

        RectTransform rt = cutsceneImage.rectTransform;
        Vector2 originalPos = rt.anchoredPosition;

        if (shake)
        {
            float elapsed = 0f;
            while (elapsed < shakeDuration)
            {
                float progress = elapsed / shakeDuration;
                float currentMagnitude = shakeMagnitude * (1f - progress);

                float x = Random.Range(-1f, 1f) * currentMagnitude;
                float y = Random.Range(-1f, 1f) * currentMagnitude;
                rt.anchoredPosition = originalPos + new Vector2(x, y);

                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }
            rt.anchoredPosition = originalPos;
        }

        yield return new WaitForSecondsRealtime(cutsceneDuration);

        cutsceneImage.gameObject.SetActive(false);
        Time.timeScale = 1f;
        if (player != null)
            player.SetCanMove(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}