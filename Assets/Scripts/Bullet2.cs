using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour, BulletInterface
{
    public IEnumerator DealEffect(Player x)
    {
        x.canMove = false;
        yield return new WaitForSeconds(2f);
        x.canMove = true;
    }
}
