using UnityEngine;

public abstract class Enemy : Character
{
    public int ContactDamage { get; protected set; } = 10;
    protected Transform playerTransform;

    public abstract void Behavior();

    public override void Initialize(int hp)
    {
        base.Initialize(hp);
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) playerTransform = p.transform;
    }

    void FixedUpdate()
    {
        Behavior();
    }

    protected override void Die()
    {
        if (GameManager.Instance != null) GameManager.Instance.AddScore(50);
        base.Die();
    }
}
