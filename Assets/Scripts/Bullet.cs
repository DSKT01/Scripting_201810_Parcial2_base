using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Type;


[RequireComponent(typeof(Bullet1))]
[RequireComponent(typeof(Bullet2))]
[RequireComponent(typeof(Bullet2))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    TipoDeBala type;
    BulletInterface instBullet;
    int damage = 1;
    Bullet1 a;
    Bullet2 b;
    Bullet3 c;
    private void Start()
    {
        a = GetComponent<Bullet1>();
        b = GetComponent<Bullet2>();
        c = GetComponent<Bullet3>();

        switch (type)
        {
            case TipoDeBala.Normal:
                transform.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
                instBullet = a;
                break;
            case TipoDeBala.Paralizadora:
                instBullet = b;
                transform.GetComponent<Renderer>().material.color = new Color(0, 1, 1, 1);
                break;
            case TipoDeBala.Desactivadora:
                instBullet = c;
                transform.GetComponent<Renderer>().material.color = new Color(1, 0, 1, 1);  
                break;
            default:
                break;
        }
        StartCoroutine(Dead2());
    }
    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(100f);
        Destroy(this.gameObject);
    }
    private IEnumerator Dead2()
    {
        yield return new WaitForSeconds(100f);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
        int otherLayer = other.gameObject.layer;
        Vulnerable obj = other.gameObject.GetComponent<Vulnerable>();
        Player objp = other.gameObject.GetComponent<Player>();
        
        if (otherLayer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(other.gameObject);
        }
        if (otherLayer == LayerMask.NameToLayer("Player"))
        {
            if (objp != null)
            {
                StartCoroutine(instBullet.DealEffect(objp));
            }

        }
        if (obj != null)
        {
            obj.Damage(damage);

        }

        transform.position = transform.position + new Vector3(500, 500, 500);
        StartCoroutine(Dead());
    }
    
}