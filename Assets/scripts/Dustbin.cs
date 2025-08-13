using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Dustbin : MonoBehaviour
{
    public enum BinType
    {
        Blue,
        Green,
        Red,
        Yellow,
    }
    [SerializeField] private float speed = 3f;
    [SerializeField] private float eps = 5f;
    [SerializeField] private int maxHealth = 1;
    private int currHealth;

    private Rigidbody rb;
    private Transform player;
    public BinType binType { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currHealth = maxHealth;
    }
    //hi

    void FixedUpdate()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        // Debug.Log(dir.magnitude + " " + eps);
        if (dir.magnitude > eps)
        {
            rb.linearVelocity = dir.normalized * speed;
        }
        else
            rb.linearVelocity = Vector3.zero;
    }

    public void SetBinType(BinType type)
    {
        binType = type;
    }

    public void Damage(int damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
