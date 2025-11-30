using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Slime : Enemy
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float walkDistance = 3f;
    private float startX;
    private int direction = 1;

    void Start()
    {
        base.Initialize(30);
        startX = transform.position.x;
    }

    public override void Behavior()
    {
        rb.linearVelocity = new Vector2(speed * direction, rb.linearVelocity.y);

        if (Mathf.Abs(transform.position.x - startX) > walkDistance)
        {
            direction *= -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, 1);
            startX = transform.position.x;
        }
    }
}
