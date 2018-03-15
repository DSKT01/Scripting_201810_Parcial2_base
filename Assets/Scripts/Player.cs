using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour, Vulnerable
{

    public int vida = 10;
    [SerializeField]
    private int moveCount;
    [SerializeField]
    Rigidbody bulletGO;
    [SerializeField]
    private Transform[] movementPoints;
    Collider mCollider;
    Renderer mRender;
    private int currentPointIndex;
    public bool canShoot = true;
    public bool canMove = true;
    GameObject panel;

    private void Awake()
    {
        panel = GameObject.Find("Panel");
        panel.SetActive(false);
    }
    private void Start()
    {
        //HACK: This is to ensure central point can move leftwards and rightwards.
        currentPointIndex = 3;
        mCollider = GetComponent<Collider>();
        mRender = GetComponent<Renderer>();
       
        if (movementPoints == null || movementPoints.Length == 0)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentPointIndex > 0)
        {
            if (canMove)
            {
                currentPointIndex--;
                transform.position = movementPoints[currentPointIndex].position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentPointIndex < movementPoints.Length - 1)
        {
            if (canMove)
            {
                currentPointIndex++;
                transform.position = movementPoints[currentPointIndex].position;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canShoot)
            {
                Shoot();
            }

        }
       

    }
    private IEnumerator Invulnerable()
    {
        yield return new WaitForSeconds(2f);
        mRender.material.color = new Color(mRender.material.color.r, mRender.material.color.g, mRender.material.color.b, 1f);
        mCollider.enabled = true;
        canMove = true;
        canShoot = true;
    }

    public void Shoot()
    {
        Rigidbody bulletInstance = Instantiate(bulletGO, transform.position + (Vector3.up * 2.5F), transform.rotation);
        bulletInstance.AddForce((transform.up) * 10, ForceMode.Impulse);
    }
    public void Damage(int x)
    {
        vida -= x;
        mCollider.enabled = false;
        mRender.material.color = new Color(mRender.material.color.r, mRender.material.color.g, mRender.material.color.b, 0.1f);
        StartCoroutine(Invulnerable());
        if (vida <= 0)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }
    }
}