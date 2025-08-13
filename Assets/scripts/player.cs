using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class player : MonoBehaviour
{
    private Vector2 move_input;
    private Rigidbody rb;
    private PlayerInputActions player_input;
    [SerializeField] float speed;
    [SerializeField] float object_detect_radius;
    public LayerMask object_mask;
    public Transform holdPoint;
    private GameObject heldObject;
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
        // if (heldObject != null)
        // {
        //     heldObject.transform.position = holdPoint.position;
        // }
    }

    public void collect_object(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (heldObject == null)
            {
                heldObject = near();
                if (heldObject != null)
                {
                    heldObject.transform.SetParent(holdPoint);
                    heldObject.transform.localPosition = Vector3.zero;
                }

            }
            else
            {
                heldObject.transform.SetParent(null);
                heldObject = null;
            }

        }
    }
    public GameObject near()
    {
        float minDist = float.PositiveInfinity;
        float dist;
        GameObject nearestObject = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position,object_detect_radius,object_mask);

        foreach (var hitCollider in hitColliders)
        {
            dist = (hitCollider.transform.position - transform.position).sqrMagnitude; 
            if (dist < minDist)
            {
                minDist = dist;
                nearestObject = hitCollider.gameObject;
            }
        }

        return nearestObject;
    }
}
