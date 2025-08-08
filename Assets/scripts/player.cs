using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private Vector3 input;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        rb.linearVelocity += input;
    }
    public void collect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("yay");
        }
    }
}
