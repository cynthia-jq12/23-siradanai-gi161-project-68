using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public Character Owner;

    public abstract void Move();
    public abstract void OnHit(Character target);

    public void InitWeapon(int dmg, Character owner)
    {
        damage = dmg;
        Owner = owner;
        Destroy(gameObject, 5f);
    }
}