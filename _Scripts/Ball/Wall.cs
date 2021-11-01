using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "LargestBall" || collision.gameObject.tag == "LargerBall" || collision.gameObject.tag == "MediumBall" || collision.gameObject.tag == "SmallBall" || collision.gameObject.tag == "SmallestBall")
        {
            if (gameObject.tag == "LeftWall")
            {
                collision.gameObject.GetComponent<Ball>().BallDirection(false);
            }

            if (gameObject.tag == "RightWall")
            {
                collision.gameObject.GetComponent<Ball>().BallDirection(true);
            }

            if (gameObject.tag == "TopWall")
            {

            }
            if (gameObject.tag == "BottomWall")
            {

            }
        }
       
    }
}
