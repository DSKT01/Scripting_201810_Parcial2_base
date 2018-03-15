using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour, BulletInterface
{
 
    public IEnumerator DealEffect(Player x)
    {
        yield return new WaitForEndOfFrame();
    }
}
