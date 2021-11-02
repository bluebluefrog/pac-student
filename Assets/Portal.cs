using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
           if(direction=="zuo")
            {
                GameManager._instan.Directionzuo = true;
            }

            if (direction == "you")
            {
                GameManager._instan.Directionyou = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (direction == "zuo")
            {
                GameManager._instan.Directionzuo = false;
            }

            if (direction == "you")
            {
                GameManager._instan.Directionyou = false;
            }
        }
    }
}
