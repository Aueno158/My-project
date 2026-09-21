using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    [SerializeField]
    private Player p; 
    [SerializeField]
    private Transform playerSpawnPoint;   // ลาก GameObject ว่างที่วางไว้ตรงจุดเริ่มต้นของ Lostscene มาใส่

    public static GameManager2 Instance;

    [Header("Lost Text Sequence")]
    [SerializeField]
    private CanvasGroup lostTextGroup;   // CanvasGroup ครอบ lostText ไว้เฟด

    [SerializeField]
    private TMP_Text lostText;

    [SerializeField]
    private string[] lines = new string[]
    {
        "Look...",
        "You still can't alive",
        "lier lier",
        "because you everyone will die",
        "LIER"
    };

    [SerializeField]
    private float startDelay = 2f;       // รอกี่วิก่อนขึ้นบรรทัดแรก

    [SerializeField]
    private float fadeDuration = 1f;     // เวลาเฟดเข้า/ออกของแต่ละบรรทัด

    [SerializeField]
    private float holdDuration = 2f;     // เวลาค้างให้อ่านแต่ละบรรทัด

    [SerializeField]
    private float gapBetweenLines = 1.5f; // ช่วงว่างมืดๆ ระหว่างบรรทัด (จอไม่มีอะไรขึ้น)

    [SerializeField]
    private float shakeMagnitude = 2.5f;  // ความแรงของการสั่น (พิกเซล) ยิ่งมากยิ่งสั่นแรง

    [SerializeField]
    private float lastLineShakeMultiplier = 3f;  // บรรทัดสุดท้าย (LIER) จะสั่นแรงกว่าปกติกี่เท่า

    void Awake()
    {
        Instance = this;

        if (lostTextGroup != null)
            lostTextGroup.alpha = 0f; 
    }

    void Start()
{
    if (AudioManager.instance != null)
        AudioManager.instance.StopAllBGM();

    Time.timeScale = 1f;

    p = Player.Instance;  

    if (p != null)
    {
        if (playerSpawnPoint != null)
            p.Teleport(playerSpawnPoint.position, playerSpawnPoint.rotation);
        else
            Debug.LogWarning("ยังไม่ได้ใส่ Player Spawn Point");

        p.SetCanMove(true);
        p.SetVisible(true);
        p.SetCursorLocked(true);
        p.LockMovementSpeed(10f, 10f, false);
    }
    else
    {
        Debug.LogWarning("ไม่เจอ Player.Instance");
    }

    StartCoroutine(PlayLostSequence());
}

    private IEnumerator PlayLostSequence()
    {
        yield return new WaitForSeconds(startDelay);

        RectTransform textRect = lostText.rectTransform;
        Vector2 originalPos = textRect.anchoredPosition;

        for (int i = 0; i < lines.Length; i++)
        {
            lostText.text = lines[i];

            bool isLastLine = (i == lines.Length - 1);
            float magnitude = isLastLine ? shakeMagnitude * lastLineShakeMultiplier : shakeMagnitude;

            Coroutine shakeRoutine = StartCoroutine(ShakeText(textRect, originalPos, magnitude));

            yield return FadeCanvasGroup(lostTextGroup, 0f, 1f, fadeDuration);
            yield return new WaitForSeconds(holdDuration);
            yield return FadeCanvasGroup(lostTextGroup, 1f, 0f, fadeDuration);

            StopCoroutine(shakeRoutine);
            textRect.anchoredPosition = originalPos;   // รีเซ็ตตำแหน่งกลับก่อนขึ้นบรรทัดถัดไป

            yield return new WaitForSeconds(gapBetweenLines);
        }
        // เล่นจบครบ 5 บรรทัดแล้วก็ปล่อยให้ Player เดินหาทางออกต่อไปเงียบๆ
    }

    private IEnumerator ShakeText(RectTransform rt, Vector2 originalPos, float magnitude)
    {
        while (true)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            rt.anchoredPosition = originalPos + new Vector2(x, y);
            yield return null;
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
    {
        float elapsed = 0f;
        cg.alpha = from;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;   // ใช้ deltaTime ปกติ เพราะ Player ยังเดินได้ ไม่ได้ pause เกม
            cg.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        cg.alpha = to;
    }
}