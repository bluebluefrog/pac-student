using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{

    private PacStudentController Pac;

    private void Awake()
    {
        Pac = GameObject.FindGameObjectWithTag("Player").GetComponent<PacStudentController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="left")
        {
            Pac.left = false;
        }

        if (collision.tag == "right")
        {
            Pac.right = false;
        }

        if (collision.tag == "Back")
        {
            Pac.Back = false;
        }

        if (collision.tag == "Front")
        {
            Pac.Front = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "left")
        {
            Pac.left = true;
        }

        if (collision.tag == "right")
        {
            Pac.right = true;
        }

        if (collision.tag == "Back")
        {
            Pac.Back = true;
        }

        if (collision.tag == "Front")
        {
            Pac.Front = true;
        }
    }
}
