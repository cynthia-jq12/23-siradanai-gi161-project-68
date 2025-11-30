using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private int maxHealth;
    [SerializeField] private float currentHealth;

    [Header("UI")]
    [SerializeField] protected HealthBar healthBar;

    public float Health
    {
        get { return currentHealth; }
        protected set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    protected Animator anim;
    protected Rigidbody2D rb;

    public virtual void Initialize(int startHealth)
    {
        maxHealth = startHealth;
        Health = startHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        if (healthBar != null) healthBar.SetHealth(Health);

        if (IsDead()) Die();
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > maxHealth) Health = maxHealth;
        if (healthBar != null) healthBar.SetHealth(Health);
    }

    public void Heal(float percentage)
    {
        int amount = Mathf.RoundToInt(maxHealth * percentage);
        Heal(amount);
    }

    public bool IsDead() => Health <= 0;

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}