using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : Character, IShootable
{
    [Header("Weapon Settings")]
    [SerializeField] private GameObject daggerPrefab; // ลาก Prefab มีด/กล้วย มาใส่
    [SerializeField] private Transform throwPoint;    // ลากจุดปล่อยของ (Empty Object) มาใส่
    [SerializeField] private float fireRate = 0.5f;   // ยิงรัวแค่ไหน
    private float nextFireTime;

    // Interface Implementation
    public GameObject BulletPrefab { get { return daggerPrefab; } set { daggerPrefab = value; } }
    public Transform ShootPoint { get { return throwPoint; } set { throwPoint = value; } }

    void Start()
    {
        // เริ่มต้นเลือด 100
        base.Initialize(100);
    }

    void Update()
    {
        // กดคลิกซ้าย หรือ Ctrl เพื่อยิง
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // ระบบ Corruption: เลือดลดตลอดเวลา
        ApplyCorruption();
    }

    void ApplyCorruption()
    {
        // ลดเลือดทีละนิดตามเวลา
        TakeDamage(1f * Time.deltaTime);
    }

    public void Shoot()
    {
        if (BulletPrefab != null && ShootPoint != null)
        {
            GameObject dagger = Instantiate(BulletPrefab, ShootPoint.position, Quaternion.identity);

            // เช็คว่าผู้เล่นหันหน้าไปทางไหน (1 ขวา, -1 ซ้าย)
            float direction = transform.localScale.x > 0 ? 1f : -1f;

            // ส่งทิศทางให้มีด (Dagger)
            Dagger d = dagger.GetComponent<Dagger>();
            if (d != null) d.Setup(direction, this);
        }
    }

    protected override void Die()
    {
        Debug.Log("Game Over!");
        // แจ้ง Game Manager ว่าจบเกม
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TriggerGameOver();
        }
        base.Die();
    }

    // ชนศัตรูแล้วเจ็บ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.ContactDamage);
        }
    }
}