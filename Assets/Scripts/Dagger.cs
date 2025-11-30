using UnityEngine;
using UnityEngine.TextCore.Text;

// มีดของผู้เล่น
public class Dagger : Weapon
{
    [SerializeField] private float speed = 10f;
    private float direction;

    public void Setup(float dir, Character owner)
    {
        direction = dir;
        base.InitWeapon(20, owner); // ดาเมจ 20

        // หันหัวมีดไปตามทิศทาง
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    public override void Move()
    {
        // พุ่งไปข้างหน้า
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }

    public override void OnHit(Character target)
    {
        // ยิงโดนศัตรูเท่านั้น
        if (target is Enemy)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    // ชนแล้วทำงาน
    void OnTriggerEnter2D(Collider2D other)
    {
        Character c = other.GetComponent<Character>();
        // ถ้าชน Character ที่ไม่ใช่คนยิง (Owner)
        if (c != null && c != Owner)
        {
            OnHit(c);
        }
    }
}
