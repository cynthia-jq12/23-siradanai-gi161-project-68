using UnityEngine;

public interface IShootable
{
    GameObject BulletPrefab { get; set; }
    Transform ShootPoint { get; set; }
    void Shoot();
}
S