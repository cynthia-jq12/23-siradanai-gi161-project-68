using UnityEngine;

public class Skeleton : Enemy, IShootable
{
    [SerializeField] private GameObject bonePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackRange = 8f;
    [SerializeField] private float shootCooldown = 3f;
    private float lastShootTime;

    public GameObject BulletPrefab { get { return bonePrefab; } set { bonePrefab = value; } }
    public Transform ShootPoint { get { return firePoint; } set { firePoint = value; } }

    void Start()
    {
        base.Initialize(50);
        ContactDamage = 5;
    }

    public override void Behavior()
    {
        if (playerTransform == null) return;

        float dist = Vector2.Distance(transform.position, playerTransform.position);

        if (playerTransform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        if (dist <= attackRange && Time.time > lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    public void Shoot()
    {
        if (BulletPrefab != null && ShootPoint != null)
        {
            GameObject bone = Instantiate(BulletPrefab, ShootPoint.position, Quaternion.identity);
            Vector2 dir = (playerTransform.position - ShootPoint.position).normalized;

            BoneProjectile b = bone.GetComponent<BoneProjectile>();
            if (b != null) b.InitWeapon(15, this);

            Rigidbody2D bRb = bone.GetComponent<Rigidbody2D>();
            if (bRb != null) bRb.AddForce(dir * 300f);
        }
    }
}