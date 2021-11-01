using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();

        if(this.gameObject.tag != "InGameCollectables")
        {
            Invoke("DeactiavateGameObject", Random.Range(2, 6));
        }
    }

    // Update is called once per frame
   void DeactiavateGameObject()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "BottomBrick")
        {
            Vector2 temp = target.transform.position;
            temp.y += 0.8f;

            transform.position = new Vector2(transform.position.x, temp.y);
            myBody.isKinematic = true;
        }

        if(target.tag == "Player")
        {
            if(this.gameObject.tag == "InGameCollectables")
            {
                GameController.instance.collectedItems[GameController.instance.currentLevel] = true;
                GameController.instance.Save();

                if(GamePlayController.instance != null)
                {
                    if(GameController.instance.currentLevel == 0)
                    {
                        GamePlayController.instance.playerScore += 1 * 1000;
                    }
                    else
                    {
                        GamePlayController.instance.playerScore += GameController.instance.currentLevel * 1000;
                    }
                }

            }
            this.gameObject.SetActive(false);
        }
    }
}
