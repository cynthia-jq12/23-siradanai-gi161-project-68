using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxSpeed = 10f;
    public float jumpForce = 700.0f;

    [Header("Ground Detection")]
    public Transform groundCheck; // ลาก Empty Object ที่อยู่ใต้เท้ามาใส่
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround; // เลือก Layer เป็น 'Ground' (ต้องตั้งค่าที่พื้นด้วย)

    private bool facingRight = true;
    private bool grounded = false;

    private Rigidbody2D r2d;
    private Animator anim;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // กระโดด (กด Spacebar) เมื่ออยู่บนพื้น
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            r2d.AddForce(new Vector2(0, jumpForce));
        }
    }

    void FixedUpdate()
    {
        // เช็คว่าเท้าแตะพื้นไหม
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        // ส่งค่าความเร็วแนวดิ่งไปที่ Animator (เพื่อเล่นท่ากระโดด/ตก)
        anim.SetFloat("vSpeed", r2d.linearVelocity.y);

        // รับค่าปุ่มลูกศรซ้าย-ขวา (-1 ถึง 1)
        float move = Input.GetAxis("Horizontal");

        // ส่งค่าความเร็วไปที่ Animator (เพื่อเล่นท่าวิ่ง)
        anim.SetFloat("Speed", Mathf.Abs(move));

        // เคลื่อนที่
        r2d.linearVelocity = new Vector2(move * maxSpeed, r2d.linearVelocity.y);

        // พลิกตัวตามทิศทางที่กด
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}