using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float horizontalSpeed = 2f;

    public bool canWalk;

    private Button shootBtn;

    [SerializeField] public bool shootOnce, shootTwice;

    private bool singleArrow, doubleArrow, singleStickyArrow, doubleStickyArrow;

    [SerializeField] private string Arrow;

    public GameObject[] arrows;

    public AnimationClip shootClip;

    float height;

    private int doubleArrowCount;

    public  bool hasSheild, isInvisible;

    public GameObject shield;

    public static Player instance;

    public int lifeCount = 3;

    public Text lifetext;
    public AudioClip shootBtnClickClip;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    
        float cameraHeight = Camera.main.orthographicSize;
         height = -cameraHeight - 0.5f;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        shootBtn = GameObject.Find("ShootBtn").GetComponent<Button>();
        shootBtn.onClick.AddListener(()=> Shoot());

        canWalk = true;
    }

    private void Update()
    {
        lifetext.text = lifeCount.ToString();
           Vector2 temp = transform.position;
        temp.x = Mathf.Clamp(temp.x, -10.36407f, 10.29238f);
        transform.position = temp;
    }

    void FixedUpdate()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        if (canWalk)
        {
            float currentSpeed = Mathf.Abs(rb.velocity.x);
            float speed = 0.0f;

            float horizontalInput = Input.GetAxisRaw("Horizontal");

            if (horizontalInput > 0)
            {
                if (currentSpeed < maxSpeed)
                {
                    speed = horizontalSpeed;

                }

                Vector2 temp = transform.localScale;
                temp.x = 1;
                transform.localScale = temp;

                anim.SetBool("walk", true);
            }

            else if (horizontalInput < 0)
            {
                if (currentSpeed < maxSpeed)
                {
                    speed = -horizontalSpeed;
                }

                Vector2 temp = transform.localScale;
                temp.x = -1;
                transform.localScale = temp;

                anim.SetBool("walk", true);
            }

            else
            {
                speed = 0;
                anim.SetBool("walk", false);
            }

            rb.AddForce(new Vector2(speed, 0));
        }
    }
       

    private void Shoot()
    {
        if (shootOnce)
        {
            if(Arrow == "normalArrow")
            {
                Instantiate(arrows[0], new Vector3(transform.position.x, height, 0), Quaternion.identity);
            }

            else if(Arrow == "stickyArrow")
            {
                Instantiate(arrows[1], new Vector3(transform.position.x, height, 0), Quaternion.identity);
            }

            StartCoroutine(PlayerTheShootAnimation());

            shootOnce = false;
        }

        else if (shootTwice)
        {
            if (Arrow == "doubleArrow")
            {
                Instantiate(arrows[2], new Vector3(transform.position.x, height, 0), Quaternion.identity);
            }

            else if (Arrow == "stickyDoubleArrow")
            {
                Instantiate(arrows[3], new Vector3(transform.position.x, height, 0), Quaternion.identity);
            }

            doubleArrowCount++;
            if(doubleArrowCount == 2)
            {

               shootTwice = false;
                doubleArrowCount = 0;
            }
        }
        AudioSource.PlayClipAtPoint(shootBtnClickClip, Camera.main.transform.position);
    }

    IEnumerator PlayerTheShootAnimation()
    {
        canWalk = false;
       
        anim.SetBool("Shoot",true);
        shootBtn.interactable = false;

        yield return new WaitForSeconds(shootClip.length);

        anim.SetBool("Shoot", false);
        canWalk = true;
        shootBtn.interactable = true;
    }

    public void PlayerShootOnce(bool p_shootOnce)
    {
        shootOnce = p_shootOnce;
    }

    public void PlayerShootTwice(bool p_shootTwice)
    {
        shootTwice = p_shootTwice;
    }

    public void DestoyShield()
    {
        StartCoroutine(PlayerInvisible());

        hasSheild = false;
        shield.SetActive(false);
    }

     IEnumerator PlayerInvisible()
    {
        isInvisible = true;

        yield return new WaitForSeconds(1f);

        isInvisible = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Shield")
        {
            hasSheild = true;
            shield.SetActive(true);

            collision.gameObject.SetActive(false);
        }

        if(collision.tag == "SingleArrow")
        {
            Arrow = "normalArrow";
            shootOnce = true;
            shootTwice = false;

            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "SingleStickyArrow")
        {
            Arrow = "stickyArrow";
            shootOnce = true;
            shootTwice = false;

            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "DoubleArrow")
        {
            Arrow = "doubleArrow";
            shootOnce = false;
            shootTwice = true;

            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "DoubleStickyArrow")
        {
            Arrow = "stickyDoubleArrow";
            shootOnce = false;
            shootTwice = true;

            collision.gameObject.SetActive(false);
        }

        if(collision.tag == "Life")
        {
            if(lifeCount < 3)
            {
                  lifeCount++;

            }

            collision.gameObject.SetActive(false);
        }

        if(collision.tag == "Bomb")
        {

        }

        if (collision.tag == "Time")
        {

        }


    }


}
