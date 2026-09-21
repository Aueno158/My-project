using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("References")]
    public Camera playerCamera;

    [Header("Movement")]
    public float walkSpeed = 6f;
    public float runSpeed = 18f;
    public float crouchSpeed = 3f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    [Header("Height")]
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;

    [Header("Look (ยังไม่ได้ใช้ในสคริปต์นี้ เก็บไว้เผื่อสคริปต์กล้องอื่นอ้างถึง)")]
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Game state")]
    public bool HasKey = false;
    public bool HasPostIt = false;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Renderer[] renderers;

    private bool canMove = true;
    private bool canRun = true;
    private bool cursorLocked = true;

    private bool speedLocked = false;
    private float lockedWalkSpeed;
    private float lockedRunSpeed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);   // ปิดทั้งตัว รวมกล้องและ AudioListener ที่เป็นลูก
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        characterController = GetComponent<CharacterController>();
        renderers = GetComponentsInChildren<Renderer>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        ApplyCursorState();
        StartCoroutine(RefreshListenersRoutine());   // เช็ค Listener ตอนเริ่มเกมครั้งแรกด้วย
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        ApplyCursorState();

        bool isCrouching = canMove && Input.GetKey(KeyCode.R);
        UpdateHeight(isCrouching);
        UpdateMovement(isCrouching);
    }

    private void UpdateHeight(bool isCrouching)
    {
        characterController.height = isCrouching ? crouchHeight : defaultHeight;
    }

    private void UpdateMovement(bool isCrouching)
    {
        float currentWalk;
        float currentRun;

        if (isCrouching)
        {
            currentWalk = crouchSpeed;
            currentRun = crouchSpeed;
        }
        else if (speedLocked)
        {
            currentWalk = lockedWalkSpeed;
            currentRun = lockedRunSpeed;
        }
        else
        {
            currentWalk = walkSpeed;
            currentRun = runSpeed;
        }

        bool isRunning = canRun && Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? currentRun : currentWalk;

        float forwardInput = Mathf.Max(Input.GetAxis("Vertical"), 0f);
        float sideInput = Input.GetAxis("Horizontal");

        float moveForward = canMove ? speed * forwardInput : 0f;
        float moveSide = canMove ? speed * sideInput : 0f;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float verticalVelocity = moveDirection.y;
        moveDirection = (forward * moveForward) + (right * moveSide);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            moveDirection.y = jumpPower;
        else
            moveDirection.y = verticalVelocity;

        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public void SetCanRun(bool value)
    {
        canRun = value;
    }

    public void LockMovementSpeed(float walk, float run, bool canRunAllowed)
    {
        speedLocked = true;
        lockedWalkSpeed = walk;
        lockedRunSpeed = run;
        canRun = canRunAllowed;
    }

    public void UnlockMovementSpeed()
    {
        speedLocked = false;
        canRun = true;
    }

    public void SetVisible(bool visible)
    {
        if (renderers == null) return;

        foreach (var r in renderers)
        {
            if (r != null)
                r.enabled = visible;
        }
    }

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        if (characterController != null)
            characterController.enabled = false;

        transform.position = position;
        transform.rotation = rotation;

        moveDirection = Vector3.zero;

        if (characterController != null)
            characterController.enabled = true;
    }

    public void SetCursorLocked(bool locked)
    {
        cursorLocked = locked;
        ApplyCursorState();
    }

    private void ApplyCursorState()
    {
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !cursorLocked;
    }

    public static void ResetPlayer()
    {
        if (Instance == null) return;

        GameObject old = Instance.gameObject;
        Instance = null;
        Destroy(old);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResetState()
    {
        HasKey = false;
        HasPostIt = false;
        canMove = true;
        canRun = true;
        speedLocked = false;
    }

    // ---------- AudioListener ----------
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FixAudioListeners();                         // จัดการทันที ก่อนเฟรมแรกของซีนใหม่
        StartCoroutine(RefreshListenersRoutine());   // แล้วเช็คซ้ำอีกหลายเฟรม
    }

    private IEnumerator RefreshListenersRoutine()
    {
        // เช็คหลายรอบ เผื่อมีอะไรในซีนเปิดกล้อง/Listener ตามมาทีหลัง
        for (int i = 0; i < 5; i++)
        {
            yield return null;
            FixAudioListeners();
        }

        yield return new WaitForSeconds(1f);
        FixAudioListeners();
    }

    private void FixAudioListeners()
    {
        AudioListener mine = null;
        if (playerCamera != null)
            mine = playerCamera.GetComponent<AudioListener>();

        AudioListener sceneListener = null;
        AudioListener[] all = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);

        foreach (AudioListener l in all)
        {
            if (l == mine) continue;
            if (!l.enabled || !l.gameObject.activeInHierarchy) continue;

            if (sceneListener == null)
                sceneListener = l;      // เก็บตัวแรกของซีนไว้
            else
                l.enabled = false;      // ตัวอื่นของซีนปิดทิ้ง
        }

        // ซีนมี Listener ของตัวเอง -> Player ปิดของตัวเอง / ซีนไม่มี -> ใช้ของ Player
        if (mine != null)
            mine.enabled = (sceneListener == null);
    }
}