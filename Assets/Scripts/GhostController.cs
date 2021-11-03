using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour
{
    public static GhostController _instance;

    public float speed = 0.2f;

    private void Awake()
    {
        _instance = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet")
        {
            print(1);
            GameManager.Instance.FreezeEnemy(this.gameObject);
            GameManager.Instance.DestroyEnemy(this.gameObject);
            GameManager.Instance.score += 300;
        }

        if (collision.gameObject.name == "Pacman")
        {
            GameObject.FindGameObjectWithTag("HealthValue").GetComponent<Text>().text = GameManager.Instance.HealthValue.ToString();
            if (GameManager.Instance.isSuperPacman == false && GameManager.Instance.HealthValue != 0)//when the player is not a super player and the HP is not 0
            {
                GameManager.Instance.HealthValue -= 1;
                GameObject.Find("Dead").GetComponent<AudioSource>().Play();
                GameObject.Find("HealthValue").GetComponent<Text>().text = GameManager.Instance.HealthValue.ToString();
                Vector3 P = GameManager._instan.ReBoomPosition.transform.position;
                GameManager._instan.Player.transform.position = P;
                PacStudentController._instance.PlayerInput = null;
                PacStudentController._instance.LastInput = null;
                PacStudentController._instance.CurrentInput = null;
            }
            if (GameManager.Instance.isSuperPacman)//when a player becomes a super player
            {
                GameManager.Instance.FreezeEnemy(this.gameObject);
                GameManager.Instance.DestroyEnemy(this.gameObject);
                GameManager.Instance.score +=300;
                GameObject.Find("Eat").GetComponent<AudioSource>().Play();
            }
            if(GameManager.Instance.isSuperPacman==false&&GameManager.Instance.HealthValue==0)//when the player is not a super player and has 0 HP
            {
                GameManager.Instance.HealthValue -= 1;
                Vector3 P = GameManager._instan.ReBoomPosition.transform.position;
                GameManager._instan.Player.transform.position = P;
                collision.gameObject.SetActive(false);
                GameManager.Instance.gamePanel.SetActive(false);
                Instantiate(GameManager.Instance.gameoverPrefab);
                Invoke("Restart", 3f);
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
