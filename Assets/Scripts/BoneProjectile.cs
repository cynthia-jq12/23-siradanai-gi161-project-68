using UnityEngine;
using UnityEngine.TextCore.Text;

// กระดูกของ Skeleton
public class BoneProjectile : Weapon
{
    public override void Move()
    {
        
    }

    public override void OnHit(Character target)
    {
        // ยิงโดนผู้เล่นเท่านั้น
        if (target is Player)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Character c = other.GetComponent<Character>();
        if (c != null && c != Owner)
        {
            OnHit(c);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}