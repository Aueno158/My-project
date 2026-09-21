using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Player p; 
    [SerializeField] 
    private Player player;

    [Header("UI References")]
    [SerializeField]
    private TMP_Text KeyText;

    [SerializeField]
    private Image cutsceneImage;

    [Header("Spawn")]
    [SerializeField]
    private Transform playerSpawnPoint;   // ลาก Empty GameObject ที่วางตรงจุดเริ่มต้นของ Gameplay มาใส่

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

    [Header("Post-it Special Sequence")]
    [SerializeField]
    private CanvasGroup blackScreenGroup;

    [SerializeField]
    private CanvasGroup storyTextGroup;

    [SerializeField]
    private TMP_Text storyText;

    [SerializeField]
    private CanvasGroup cutsceneImageGroup;

    [SerializeField]
    private string line1 = "you try to remember something ...";

    [SerializeField]
    private string line2 = "you didn't lost everything  you just...";

    [SerializeField]
    private string nevermindLine = "nevermind!";

    [SerializeField]
    private float textFadeDuration = 1f;

    [SerializeField]
    private float line1HoldDuration = 1.5f;

    [SerializeField]
    private float imageFadeDuration = 1f;

    [SerializeField]
    private float imageHoldDuration = 2f;

    [SerializeField]
    private float line2HoldDuration = 3f;

    [SerializeField]
    private float nevermindDuration = 2f;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        if (cutsceneImage != null)
            cutsceneImage.gameObject.SetActive(false);

        if (blackScreenGroup != null)
        {
            blackScreenGroup.alpha = 0f;
            blackScreenGroup.gameObject.SetActive(false);
        }

        if (storyTextGroup != null)
        {
            storyTextGroup.alpha = 0f;
            storyTextGroup.gameObject.SetActive(false);
        }
    }

    void Start()
{
    Time.timeScale = 1f;
    currentTime = timeLimit;
    timerRunning = true;

    player = Player.Instance;   // ตัวจริงที่รอดข้ามซีน
    p = player;

    if (player != null)
    {
        player.ResetState();    // ดูข้อ 3
        if (playerSpawnPoint != null)
            player.Teleport(playerSpawnPoint.position, playerSpawnPoint.rotation);
        else
            Debug.LogWarning("ยังไม่ได้ใส่ Player Spawn Point ใน GameManager");

        player.UnlockMovementSpeed();
        player.SetVisible(true);
        player.SetCanMove(true);
        player.SetCursorLocked(true);
    }
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
        if (KeyText != null)
            KeyText.text = s;
    }

    public void ShowNotiTextTemporary(string s, float duration)
    {
        StartCoroutine(NotiTextTemporaryRoutine(s, duration));
    }

    private IEnumerator NotiTextTemporaryRoutine(string s, float duration)
    {
        ShowNotiText(s);
        yield return new WaitForSecondsRealtime(duration);
        ShowNotiText("");
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

    // ---------- ลำดับคัตซีนพิเศษตอนเก็บโพสอิต ----------
    public void ShowPostItSequence(Sprite boxCutsceneSprite)
    {
        StartCoroutine(PostItSequenceRoutine(boxCutsceneSprite));
    }

    private IEnumerator PostItSequenceRoutine(Sprite boxCutsceneSprite)
    {
        Time.timeScale = 0f;
        if (player != null)
            player.SetCanMove(false);

        blackScreenGroup.gameObject.SetActive(true);
        blackScreenGroup.alpha = 1f;

        storyText.text = line1;
        storyTextGroup.gameObject.SetActive(true);
        yield return FadeCanvasGroup(storyTextGroup, 0f, 1f, textFadeDuration);
        yield return new WaitForSecondsRealtime(line1HoldDuration);
        yield return FadeCanvasGroup(storyTextGroup, 1f, 0f, textFadeDuration);
        storyTextGroup.gameObject.SetActive(false);

        cutsceneImage.sprite = boxCutsceneSprite;
        cutsceneImage.gameObject.SetActive(true);
        cutsceneImageGroup.alpha = 0f;

        yield return FadeCanvasGroup(cutsceneImageGroup, 0f, 1f, imageFadeDuration);
        yield return FadeCanvasGroup(blackScreenGroup, 1f, 0f, imageFadeDuration);

        yield return new WaitForSecondsRealtime(imageHoldDuration);

        yield return FadeCanvasGroup(blackScreenGroup, 0f, 1f, imageFadeDuration);
        cutsceneImage.gameObject.SetActive(false);

        storyText.text = line2;
        storyTextGroup.gameObject.SetActive(true);
        storyTextGroup.alpha = 0f;
        yield return FadeCanvasGroup(storyTextGroup, 0f, 1f, textFadeDuration);
        yield return new WaitForSecondsRealtime(line2HoldDuration);
        yield return FadeCanvasGroup(storyTextGroup, 1f, 0f, textFadeDuration);
        storyTextGroup.gameObject.SetActive(false);

        yield return FadeCanvasGroup(blackScreenGroup, 1f, 0f, textFadeDuration);
        blackScreenGroup.gameObject.SetActive(false);

        Time.timeScale = 1f;
        if (player != null)
            player.SetCanMove(true);

        ShowNotiTextTemporary(nevermindLine, nevermindDuration);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
    {
        float elapsed = 0f;
        cg.alpha = from;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        cg.alpha = to;
    }

    public void Exit()
    {
        Application.Quit();
    }
}