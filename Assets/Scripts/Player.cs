using UnityEngine;

public class Player : Character, IShootable
{
    [Header("Weapon Settings")]
    [SerializeField] private GameObject daggerPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float fireRate = 0.5f;
    private float nextFireTime;

    public GameObject BulletPrefab { get { return daggerPrefab; } set { daggerPrefab = value; } }
    public Transform ShootPoint { get { return throwPoint; } set { throwPoint = value; } }

    void Start()
    {
        base.Initialize(100);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        ApplyCorruption();
    }

    void ApplyCorruption()
    {
        TakeDamage(1f * Time.deltaTime);
    }

    public void Shoot()
    {
        if (BulletPrefab != null && ShootPoint != null)
        {
            GameObject dagger = Instantiate(BulletPrefab, ShootPoint.position, Quaternion.identity);
            float direction = transform.localScale.x > 0 ? 1f : -1f;

            Dagger d = dagger.GetComponent<Dagger>();
            if (d != null) d.Setup(direction, this);
        }
    }

    protected override void Die()
    {
        Debug.Log("Game Over!");
        if (GameManager.Instance != null) GameManager.Instance.TriggerGameOver();
        base.Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            TakeDamage(enemy.ContactDamage);
        }
    }
}