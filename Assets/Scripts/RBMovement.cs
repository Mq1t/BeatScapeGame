using UnityEngine;

public class RBMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 4f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(x, y).normalized;
    }

    //Fixed Update is called at a fixed rate, used for applying movement to the RigidBody, vs how Update is used for reading input constantly.
    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
}
