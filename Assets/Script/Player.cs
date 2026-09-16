
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float forcePower;

     [SerializeField]
    private Rigidbody rb;
    private InputAction moveAction;
    private Vector2 moveValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         MoveBehindtoFornt();
    }

    private void MoveBehindtoFornt()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        rb.AddForce(moveValue.y *Vector3.forward * forcePower);
    }
}
