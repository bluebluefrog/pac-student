using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public static PacStudentController _instance;

    public float speed;

    public string PlayerInput=null;

    public string LastInput=null;

    public string CurrentInput=null;

    public ParticleSystem VFX;

    public bool IsGun = false;

    public bool IsBullet = false;

    public GameObject BulletPerfab;

    public GameObject CurrentBullet;

    public bool IsShow = false;

    public float Guntimer = 0f;

    public float ShowTmer = 0f;

    public Transform FirePosition;

    public Quaternion Rotation;

    public bool left = true;

    public bool right = true;

    public bool Back = true;

    public bool Front = true;

    public bool IsInput = true;

    public bool SetAnimLeft = false;
    public bool SetAnimRight = false;
    public bool SetAnimDwon = false;
    public bool SetAnimUp= false;



    private void Awake()
    {
        _instance = this;
    }


    private void Update()
    {
        if (SetAnimDwon)
        {
            GetComponent<Animator>().SetBool("Down", true);
            GetComponent<Animator>().SetBool("Up", false);
            GetComponent<Animator>().SetBool("Right", false);
            GetComponent<Animator>().SetBool("Left", false);
            SetAnimDwon = false;
        }

        if (SetAnimUp)
        {
            GetComponent<Animator>().SetBool("Down", false);
            GetComponent<Animator>().SetBool("Up", true);
            GetComponent<Animator>().SetBool("Right", false);
            GetComponent<Animator>().SetBool("Left", false);
            SetAnimUp = false;
        }

        if (SetAnimLeft)
        {
            GetComponent<Animator>().SetBool("Down", false);
            GetComponent<Animator>().SetBool("Up", false);
            GetComponent<Animator>().SetBool("Right", false);
            GetComponent<Animator>().SetBool("Left", true);
            SetAnimLeft = false;
        }

        if (SetAnimRight)
        {
            GetComponent<Animator>().SetBool("Down", false);
            GetComponent<Animator>().SetBool("Up", false);
            GetComponent<Animator>().SetBool("Right", true);
            GetComponent<Animator>().SetBool("Left", false);
            SetAnimRight = false;
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            if (Front)
            {
                if (PlayerInput == null)
                {
                    PlayerInput = "D";
                    LastInput = PlayerInput;
                    CurrentInput = LastInput;
                }

                if (PlayerInput != null)
                {
                    PlayerInput = "D";
                    if (PlayerInput != LastInput)
                    {
                        LastInput = PlayerInput;
                        CurrentInput = LastInput;
                    }
                }
            }

            if(Front==false)
            {
                PlayerInput = "D";
                if (PlayerInput != LastInput)
                {
                    LastInput = PlayerInput;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Back)
            {
                if (PlayerInput == null)
                {
                    PlayerInput = "A";
                    LastInput = PlayerInput;
                    CurrentInput = LastInput;
                }

                if (PlayerInput != null)
                {
                    PlayerInput = "A";
                    if (PlayerInput != LastInput)
                    {
                        LastInput = PlayerInput;
                        CurrentInput = LastInput;
                    }
                }
            }

            if (Back == false)
            {
                PlayerInput = "A";
                if (PlayerInput != LastInput)
                {
                    LastInput = PlayerInput;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (left)
            {
                if (PlayerInput == null)
                {
                    PlayerInput = "W";
                    LastInput = PlayerInput;
                    CurrentInput = LastInput;
                }

                if (PlayerInput != null)
                {
                    PlayerInput = "W";
                    if (PlayerInput != LastInput)
                    {
                        LastInput = PlayerInput;
                        CurrentInput = LastInput;
                    }
                }
            }

            if (left == false)
            {
                PlayerInput = "W";
                if (PlayerInput != LastInput)
                {
                    LastInput = PlayerInput;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (right)
            {
                if (PlayerInput == null)
                {
                    PlayerInput = "S";
                    LastInput = PlayerInput;
                    CurrentInput = LastInput;
                }

                if (PlayerInput != null)
                {
                    PlayerInput = "S";
                    if (PlayerInput != LastInput)
                    {
                        LastInput = PlayerInput;
                        CurrentInput = LastInput;
                    }

                    if (PlayerInput == LastInput)
                    {
                        LastInput = PlayerInput;
                        CurrentInput = LastInput;
                    }
                }
            }

            if (right == false)
            {
                PlayerInput = "S";
                if (PlayerInput != LastInput)
                {
                    LastInput = PlayerInput;
                }
            }
        }




        if (LastInput=="S"&&right)
        {
            CurrentInput = LastInput;
            SetAnimDwon = true;
        }

        if (LastInput == "W" && left)
        {
            CurrentInput = LastInput;
            SetAnimUp = true;
        }

        if (LastInput == "A" && Back)
        {
            CurrentInput = LastInput;
            SetAnimLeft = true;
        }

        if (LastInput == "D" && Front)
        {
            CurrentInput = LastInput;
            SetAnimRight = true;
        }

        if (CurrentInput!=null)
        {
            GetComponent<AudioSource>().Play();
        }

        if (CurrentInput=="D"&&Front)
        {
            SetAnimRight = true;
            transform.Translate(new Vector3(speed, 0, 0));
        }

        if (CurrentInput == "A"&&Back)
        {
            SetAnimLeft = true;
            transform.Translate(new Vector3(-speed, 0, 0));
        }

        if (CurrentInput == "W"&&left)
        {
            SetAnimUp = true;
            transform.Translate(new Vector3(0, speed, 0));
        }

        if (CurrentInput == "S"&&right)
        {
            SetAnimDwon = true;
            transform.Translate(new Vector3(0, -speed, 0));
        }

       if(SetAnimUp||SetAnimDwon||SetAnimLeft||SetAnimRight)
        {
            VFX.Play();
        }

        if(IsGun)
        {
           if(Input.GetMouseButtonDown(0)&&IsBullet==false)
           {
                CurrentBullet = GameObject.Instantiate(BulletPerfab,FirePosition.transform.position,FirePosition.transform.rotation);//增加一个开火的位置使其在开火位置产生并从该位置的X轴射出，开火的X轴会随着Player动画的改变方向而同时改变X轴的方向
                IsBullet = true;
           }
        }

        if (IsBullet)
        {
            CurrentBullet.transform.Translate(new Vector3(0.5f, 0, 0));
            Guntimer += Time.deltaTime;
            if(Guntimer>=0.2f)
            {
                CurrentBullet = null;
                IsBullet = false;
                Guntimer = 0f;
            }
        }

        if(IsShow)
        {
            speed = 0.1f;
            ShowTmer += Time.deltaTime;
            if(ShowTmer>=5f)
            {
                speed = 0.05f;
                ShowTmer = 0f;
                IsShow = false;
            }
        }
    }
}
