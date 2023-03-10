using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override bool OnCollisionEnter2D(Collision2D collision)
    {
        bool collisionResult = base.OnCollisionEnter2D(collision);
        if (collisionResult)
        {
            AudioManager.Instance.PlayEnemyProjectileHit1();
        }
        return collisionResult;
    }
}
