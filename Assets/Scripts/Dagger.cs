using UnityEngine;

public class Dagger : Weapon
{
    [SerializeField] private float speed = 10f;
    private float direction;

    public void Setup(float dir, Character owner)
    {
        direction = dir;
        base.InitWeapon(20, owner);

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    public override void Move()
    {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }

    public override void OnHit(Character target)
    {
        if (target is Enemy)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Character c = other.GetComponent<Character>();
        if (c != null && c != Owner) OnHit(c);
    }
}