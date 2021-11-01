using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollector : MonoBehaviour
{
    private int doubleArrowCount;

    Player player;

    private void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NormalArrow")
        {
          
            player.PlayerShootOnce(true);
        }

        if (collision.gameObject.tag == "DoubleArrow")
        {
        
            player.PlayerShootOnce(true);
        }

        if (collision.tag == "StickyNormalArrow")
        {
         
            doubleArrowCount++;

            if(doubleArrowCount == 2)
            {
                player.PlayerShootTwice(true);
                doubleArrowCount = 0;
            }
        }

        if (collision.tag == "StickyDoubleArrow")
        {
          
            doubleArrowCount++;

            if (doubleArrowCount == 2)
            {
                player.PlayerShootTwice(true);
                doubleArrowCount = 0;
            }
        }
    }
}
