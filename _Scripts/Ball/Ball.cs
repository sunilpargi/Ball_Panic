using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public bool moveLeft, moveRight;

    public GameObject childBall;
    private GameObject ball1, ball2;
    public Ball childBall1, childBall2;
    Player player;

    [SerializeField]
    private AudioClip[] popSounds;

    private float forceX, forceY;
    [SerializeField]
    private Rigidbody2D myRigidBody;

    public AudioClip playerHurt, ballDieClip;
    private void Awake()
    {
        InstantiateBalls();
        SetBallSpeed();
    }
    void Start()
    {
        if (this.gameObject.tag == "SmallestBall")
        {
            GamePlayController.instance.smallBallsCount++;
        }
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

   
    void Update()
    {
        //if (moveLeft)
        //{
        //    rb.velocity = new Vector2(-speed , rb.velocity.y);
        //}

        //else if (moveRight)
        //{
        //    rb.velocity = new Vector2(speed, rb.velocity.y);
        //}
        MoveBall();
    }


    void MoveBall()
    {
        if (moveLeft)
        {
            Vector3 temp = transform.position;
            temp.x -= (forceX * Time.deltaTime);
            transform.position = temp;
        }

        if (moveRight)
        {
            Vector3 temp = transform.position;
            temp.x += (forceX * Time.deltaTime);
            transform.position = temp;
        }
    } // move the ball
    void InitiateChildBallsAndTurnOffOriginal()
    {
      GameObject childBall1 = Instantiate(childBall, transform.position, Quaternion.identity);
       

        GameObject childBall2 = Instantiate(childBall, transform.position, Quaternion.identity);
       

        childBall1.GetComponent<Ball>().BallDirection(true);
        childBall2.GetComponent<Ball>().BallDirection(false);
        GiveScoreAndCoins(this.gameObject.tag);
        gameObject.SetActive(false);
    }

    public void BallDirection(bool _moveLeft)
    {
        moveLeft = _moveLeft;
        moveRight = !_moveLeft;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BottomBrick")
        {
            myRigidBody.velocity = new Vector2(0, forceY);
        } // if its the bottom brick
        if (collision.tag == "LeftBrick")
        {
            moveLeft = false;
            moveRight = true;
        } // if its left brick

        if (collision.tag == "RightBrick")
        {
            moveLeft = true;
            moveRight = false;
        } // if its right brick
        if (collision.gameObject.tag == "LeftWall")
             {

                  BallDirection(false);
             }

            if (collision.gameObject.tag == "RightWall")
            {
         
            BallDirection(true);
            }

            if(collision.gameObject.tag == "NormalArrow" || collision.gameObject.tag == "StickyArrow")
            {
                if(gameObject.tag != "SmallestBall")
                {
                    collision.gameObject.SetActive(false);
                //  InitiateChildBallsAndTurnOffOriginal();
                InitializeBallsAndTurnOffCurrentBall();
                    player.PlayerShootOnce(true);
                 }
                else
            {
                player.PlayerShootOnce(true);
                GamePlayController.instance.CountSmallBalls();
                AudioSource.PlayClipAtPoint(ballDieClip, Camera.main.transform.position);
                gameObject.SetActive(false);
                }
            }

            if(collision.gameObject.tag == "Player")
                {
                    if (Player.instance.hasSheild)
                    {
                         Player.instance.DestoyShield();
                    }

                    if (!Player.instance.isInvisible)
                    {
                      if( Player.instance.lifeCount > 0)
                         {
                          Player.instance.lifeCount--;
                 //   GamePlayController.instance.PlayerDied();
                    AudioSource.PlayClipAtPoint(playerHurt, Camera.main.transform.position);

                            if (Player.instance.lifeCount == 0)
                                {
                                 Destroy(collision.gameObject);
                                }
                        }
                    }
                 
                }     
    }

    void GiveScoreAndCoins(string objTag)
    {

        switch (objTag)
        {

            case "LargestBall":
                GamePlayController.instance.coins += Random.Range(15, 20);
                GamePlayController.instance.playerScore += Random.Range(600, 700);
                break;

            case "LargeBall":
                GamePlayController.instance.coins += Random.Range(13, 18);
                GamePlayController.instance.playerScore += Random.Range(500, 600);
                break;

            case "MediumBall":
                GamePlayController.instance.coins += Random.Range(11, 16);
                GamePlayController.instance.playerScore += Random.Range(400, 500);
                break;

            case "SmallBall":
                GamePlayController.instance.coins += Random.Range(10, 15);
                GamePlayController.instance.playerScore += Random.Range(300, 400);
                break;

            case "SmallestBall":
                GamePlayController.instance.coins += Random.Range(9, 14);
                GamePlayController.instance.playerScore += Random.Range(200, 300);
                break;

        }

    }
    void InitializeBallsAndTurnOffCurrentBall()
    {

        Vector3 position = transform.position;

        childBall1.transform.position = position;
        childBall1.moveLeft = true;

        childBall2.transform.position = position;
        childBall2.moveRight =true;

        childBall1.gameObject.SetActive(true);
        childBall2.gameObject.SetActive(true);

        if (gameObject.tag != "SmallestBall")
        {
            if (transform.position.y > 1 && transform.position.y <= 1.3f)
            {
                childBall1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                childBall2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
            }
            else if (transform.position.y > 1.3f)
            {
                childBall1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                childBall2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
            }
            else if (transform.position.y < 1)
            {
                childBall1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                childBall2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
            }
        }

        AudioSource.PlayClipAtPoint(popSounds[Random.Range(0, popSounds.Length)], Camera.main.transform.position);
       // InitializeCollectableItems(transform.position);
        GiveScoreAndCoins(this.gameObject.tag);
        gameObject.SetActive(false);

    }

    void InstantiateBalls()
    {

        if (this.gameObject.tag != "SmallestBall")
        {
            ball1 = Instantiate(childBall);
            ball2 = Instantiate(childBall);

            childBall1 = ball1.GetComponent<Ball>();
            childBall2 = ball2.GetComponent<Ball>();

            ball1.SetActive(false);
            ball2.SetActive(false);
        }
    }

    void SetBallSpeed()
    {

        forceX = 2.5f;

        switch (this.gameObject.tag)
        {

            case "LargestBall":
                forceY = 11.5f;
                break;

            case "LargeBall":
                forceY = 10.5f;
                break;

            case "MediumBall":
                forceY = 9f;
                break;

            case "SmallBall":
                forceY = 8f;
                break;

            case "SmallestBall":
                forceY = 7f;
                break;

        }

    } // set ball speed
}
