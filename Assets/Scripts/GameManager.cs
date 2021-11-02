using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager _instan;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get {
            return _instance;
        }
    }

    public GameObject[] NPC;

    public GameObject StartTimer;

    public GameObject pacman;
    public GameObject goman1;
    public GameObject goman2;
    public GameObject goman3;
    public GameObject goman4;

    public bool isSuperPacman = false;

    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> {0,1,2,3};
    private List<GameObject> pacdotGos = new List<GameObject>();
    private int pacdotNum = 0;
    private int nowEat = 0;
    public int score = 0;
    public Text remainText;
    public Text nowText;
    public Text scoreText;

    public bool Directionzuo=false;

    public bool Directionyou=false;

    public Transform DirectionzuoPoint;

    public Transform DirectionyouPoint;

    public GameObject Player;

    public GameObject gamestartPanel;
    public GameObject gameoverPrefab;
    public GameObject gamePanel;
    public GameObject winPrefab;
    public AudioClip startClip;

    public int HealthValue = 3;

    public GameObject ReBoomPosition;

    public bool IsOnEatSuperPacdot = false;//Whether to eat super dot

    public GameObject EatSuperPacdotText;

    public Text OnEatSuperPacdotNumberText;

    public float OnEatSuperPacdotTextNumber = 11f;

    public int OnEatSuperPacdotTextNumberConvertInt;

    public float Starttimer;

    public Timer Timer;//timer

    public bool OnStartGame = true;

    private void Awake()
    {
        Timer.enabled = false;
        _instance = this;
        _instan = this;
        int tempCount = rawIndex.Count;
        for (int i = 0; i < tempCount; i++) {
           int tempIndex= Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }

        foreach (Transform t in GameObject.Find("Maze").transform)
        {
            pacdotGos.Add(t.gameObject);
        }
        pacdotNum = GameObject.Find("Maze").transform.childCount;
    }

    private void Start()
    {
        OnStartGame = true;
    }

    private void Update()
    {
        //After obtaining the highest score script, compare whether the current score is greater than the highest score, and if it is greater, re-assign the highest score
        DontDestroy dontDestroy = GameObject.Find("DontDestroy").GetComponent<DontDestroy>();
        if (dontDestroy.MaxScore < score)
        {
            dontDestroy.MaxScore = score;
        }

        if (Directionzuo)
        {
            Vector3 P = DirectionyouPoint.position;
            Player.transform.position = P;
        }

        if (Directionyou)
        {
            Vector3 P = DirectionzuoPoint.position;
            Player.transform.position = P;
        }

        if (nowEat == pacdotNum && pacman.GetComponent<PacStudentController>().enabled != false) 
        {
            gamePanel.SetActive(false);
            Instantiate(winPrefab);
            StopAllCoroutines();
            SetGameState(false);
        }
        if (nowEat == pacdotNum)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (gamePanel.activeInHierarchy)
        {
            remainText.text = "Remain:" + (pacdotNum - nowEat);
            nowText.text = "Eaten:" + (nowEat);
            scoreText.text = "Score:" + (score);
        }
        if(IsOnEatSuperPacdot==true)
        {
            EatSuperPacdotText.SetActive(true);
            OnEatSuperPacdotTextNumber -= Time.deltaTime;
            OnEatSuperPacdotTextNumberConvertInt = (int)OnEatSuperPacdotTextNumber;
            OnEatSuperPacdotNumberText.text = OnEatSuperPacdotTextNumberConvertInt.ToString();
            if(OnEatSuperPacdotTextNumberConvertInt==0)
            {
                IsOnEatSuperPacdot = false;
                EatSuperPacdotText.SetActive(false);
            }
        }

        if(IsOnEatSuperPacdot == false)
        {
            EatSuperPacdotText.SetActive(false);
        }

        if(OnStartGame)
        {
            StartTimer.SetActive(true);
            goman1.GetComponent<GhostController>().enabled = false;
            goman2.GetComponent<GhostController>().enabled = false;
            goman3.GetComponent<GhostController>().enabled = false;
            goman4.GetComponent<GhostController>().enabled = false;
            foreach(GameObject item in NPC)
            {
                item.GetComponent<Animator>().enabled = false;
            }
            pacman.GetComponent<PacStudentController>().enabled = false;
            Starttimer -= Time.deltaTime;
            if (Starttimer <= 0f)
            {
                StartTimer.SetActive(false);
                foreach (GameObject item in NPC)
                {
                    item.GetComponent<Animator>().enabled = true;
                }
                goman1.GetComponent<GhostController>().enabled = true;
                goman2.GetComponent<GhostController>().enabled = true;
                goman3.GetComponent<GhostController>().enabled = true;
                goman4.GetComponent<GhostController>().enabled = true;
                pacman.GetComponent<PacStudentController>().enabled = true;
                Timer.enabled = true;
                OnStartButton();
                OnStartGame = false;
            }
        }
    }

    public void OnStartButton()
    {
            SetGameState(true);
            Invoke("CreateSuperpacdot", 5f);
            AudioSource.PlayClipAtPoint(startClip, new Vector3(0, 0, -5));
            gamestartPanel.SetActive(false);
            gamePanel.SetActive(true);
            Invoke("PlayAudio", 5f);
    }

    public void PlayAudio() {
        GetComponent<AudioSource>().Play();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnEatPacdot(GameObject go)
    {
        nowEat++;
        score += 100;
        pacdotGos.Remove(go);  
    }

    public void OnEatSuperPacdot() {
        isSuperPacman = true;
        IsOnEatSuperPacdot = true;
        score += 200;
        Invoke("CreateSuperpacdot", 20f);
        ChangeEnemy();
        goman1.GetComponent<SpriteRenderer>().enabled = false;
        goman2.GetComponent<SpriteRenderer>().enabled = false;
        goman3.GetComponent<SpriteRenderer>().enabled = false;
        goman4.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(RecoverEnemy());
    }

    IEnumerator RecoverEnemy() {
        yield return new WaitForSeconds(10f);
        goman1.GetComponent<SpriteRenderer>().enabled = true;
        goman2.GetComponent<SpriteRenderer>().enabled = true;
        goman3.GetComponent<SpriteRenderer>().enabled = true;
        goman4.GetComponent<SpriteRenderer>().enabled = true;
        UnFreezeEnemy();
        UnChangeEnemy();
        isSuperPacman = false;
    }

    private void CreateSuperpacdot() 
    {
        if (pacdotGos.Count < 5) 
        {
            return;
        }

        int tempIndex= Random.Range(0, pacdotGos.Count);
        pacdotGos[tempIndex].transform.localScale = new Vector3(3, 3, 3);
        pacdotGos[tempIndex].GetComponent<Pacdot>().isSuperPacdot = true;
    }


    private void ChangeEnemy()//change color
    {
        goman1.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.7f, 0.7f, 0.7f);
        goman2.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        goman3.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        goman4.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    }

    private void UnChangeEnemy()//change color
    {
        goman1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        goman2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        goman3.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        goman4.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

    public void FreezeEnemy(GameObject go) {
        go.GetComponent<GhostController>().enabled = false;//shotdown enemy
    }

    public void DestroyEnemy(GameObject go)
    {
        go.SetActive(false);//DestroyEnemy
    }

    public void UnFreezeEnemy() {
        goman1.GetComponent<GhostController>().enabled = true;
        goman2.GetComponent<GhostController>().enabled = true;
        goman3.GetComponent<GhostController>().enabled = true;
        goman4.GetComponent<GhostController>().enabled = true;
    }

    private void SetGameState(bool state) {

        pacman.GetComponent<PacStudentController>().enabled = state;
        goman1.GetComponent<GhostController>().enabled = state;
        goman2.GetComponent<GhostController>().enabled = state;
        goman3.GetComponent<GhostController>().enabled = state;
        goman4.GetComponent<GhostController>().enabled = state;
    }
}
