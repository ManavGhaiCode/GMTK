using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDir;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        Vector2 Force = moveDir.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + Force);
    }
}
