using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private Vector2 move_input;
    private Rigidbody rb;
    private PlayerInputActions player_input;
    [SerializeField] float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_input = new PlayerInputActions();
        player_input.Enable();
        player_input.player.collect.performed += collect_object;
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        move_input = player_input.player.move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(move_input.x, 0f, move_input.y);

        if (direction.sqrMagnitude > 1f)
            direction.Normalize();

        rb.linearVelocity = direction * speed;
    }

    public void collect_object(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("yay");
        }
    }
}
