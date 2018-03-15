using System.Collections;
using UnityEngine;
using Type;

public class Enemy : MonoBehaviour , Vulnerable
{
    [SerializeField]
    TipoDeEnemigo type;
    Transform pTrans;
    Player pPlayer;
    int vida;
    float velocidad;
    int puntaje;
    float tiempoDeDisparo;
    private Rigidbody[] bulletGO;
    private float shootSpeed;
    private bool startedShoot;
    private RaycastHit rayInfo;
    Rigidbody mRig;
    ControladorDePartida puntos;
    // Use this for initialization
    private void Start()
    {
        bulletGO = Resources.LoadAll<Rigidbody>("Prefabs/Bullets");
        if (bulletGO == null)
        {
            enabled = false;
        }
        mRig = GetComponent<Rigidbody>();
        pTrans = GameObject.Find("Player").GetComponent<Transform>();
        pPlayer = GameObject.Find("Player").GetComponent<Player>();
        puntos = GameObject.Find("Canvas").GetComponent<ControladorDePartida>();
        switch (type)
        {
            case TipoDeEnemigo.Debil:
                vida = 1;
                velocidad = 1;
                puntaje = 1;
                tiempoDeDisparo = 1f;
                shootSpeed = 10f;
                transform.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
                break;
            case TipoDeEnemigo.Normal:
                vida = 2;
                velocidad = 2;
                puntaje = 2;
                tiempoDeDisparo = 3f;
                shootSpeed = 20f;
                transform.GetComponent<Renderer>().material.color = new Color(1, 1, 0, 1);
                break;          
            default:
                vida = 4;
                velocidad = 4;
                puntaje = 4;
                tiempoDeDisparo = 1f;
                shootSpeed = 50f;
                transform.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                break;

        }
        mRig.AddForce(transform.up * -1 * velocidad, ForceMode.Impulse);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * 1000F), Color.black);
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out rayInfo, 1000F) && !startedShoot)
        {
            if (rayInfo.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
            {
                print("Shooting");
                startedShoot = true;
                Shoot();
                StartCoroutine(ToggleShootCR());
            }
        }
        if (transform.position.y < pTrans.position.y)
        {
            pPlayer.Damage(100);
        }
    }

    private IEnumerator ToggleShootCR()
    {
        yield return new WaitForSeconds(tiempoDeDisparo);
        startedShoot = false;
    }

    public void Shoot()
    {
        Rigidbody bulletInstance = Instantiate(bulletGO[Random.Range(0, bulletGO.Length)], transform.position + (Vector3.down * 2.5F), transform.rotation);
        bulletInstance.AddForce((transform.up * -1F) * shootSpeed, ForceMode.Impulse);
    }
    public void Damage(int x)
    {
        vida -= x;
        puntos.Puntos += puntaje;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        int otherLayer = other.gameObject.layer;

        Player objp = other.gameObject.GetComponent<Player>();

        if (otherLayer == LayerMask.NameToLayer("Player"))
        {
            Damage(100);

        }
       

        
    }
}