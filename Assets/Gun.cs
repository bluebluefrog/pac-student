using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string WeaponName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag=="Player")
        {
            if(WeaponName=="Gun")
            {
                PacStudentController._instance.IsGun = true;
                gameObject.SetActive(false);
            }

            if(WeaponName=="Show")
            {
                PacStudentController._instance.IsShow = true;
                gameObject.SetActive(false);
            }
        }
    }
}
