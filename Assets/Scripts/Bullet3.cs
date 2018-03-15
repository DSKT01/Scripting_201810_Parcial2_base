using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour, BulletInterface
{
    public IEnumerator DealEffect(Player x)
    {
        x.canShoot = false;
        yield return new WaitForSeconds(2f);
        x.canShoot = true;
    }
}
