using UnityEngine;
using System.Collections;

public class DualJoystickPlayerController : MonoBehaviour
{

    
    [Header("Player")]
    public LeftJoystick     leftJoystick; 
    public RightJoystick    rightJoystick; 
    public float            moveSpeed = 6.0f; 
    public GameObject       player; 
    public Animator         animator;
    public AudioSource      AudioPlayer;
    public float            jump;
    public PlayerInventoryManager inventory;
    public GameObject       itemUse;
    [Space]

    [Header("Light")]
    public GameObject       light;
    public LightCollider    lightCol;
    [Space]

    private Vector3         leftJoystickInput; 
    private Vector3         rightJoystickInput; 
    private Rigidbody2D     rigidbody;
    private bool            isLadder;
    private bool            isFloor = true;

    public bool IsLadder { get; set; }
    public bool IsFloor { get; set; }

    Vector3 fall;
    bool fairyMagic = false;
    float timeCount;
    bool isHurt = false;

    private void Start()
    {
        light = GameObject.FindGameObjectWithTag("Light");
        lightCol = light.GetComponent<LightCollider>();
        moveSpeed += moveSpeed * PlayerPrefs.GetFloat("Speed") * 0.01f;

        if (inventory.FairyMagic > 0)
        {
            inventory.FairyMagic -= 1;
            fairyMagic = true;
        }

        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!DataManager.Instance.gameOver)
        {
            leftJoystickInput = leftJoystick.GetInputDirection();
            rightJoystickInput = rightJoystick.GetInputDirection();

            if (!IsFloor)
            {
                timeCount += Time.deltaTime;
                if(timeCount > 0.5f)
                {
                    animator.SetBool("IsFall", true);
                }
            }

            if (leftJoystickInput == Vector3.zero)  //왼쪽 조이스틱 값이 0이면 이동하지 않는다.
            {
                animator.SetBool("IsWalk", false);
            }

            if (rightJoystickInput == Vector3.zero) // 오른쪽 조이스틱 값이 0이면 빛을 비추지 않는다.
            {
                light.SetActive(false);
                lightCol.GemNoActive();
            }

            if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero)    //왼쪽은 움직이고 있지만 오른쪽은 움직이지 않고 있다.
            {
                Move();       
            }

            if (leftJoystickInput == Vector3.zero && rightJoystickInput != Vector3.zero)
            {
                animator.SetBool("IsWalk", false);
                LightOn();
            }

            if (leftJoystickInput != Vector3.zero && rightJoystickInput != Vector3.zero)
            {
                LightOn();   
                Move();
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "battery")
        {
            float maxBattery = DataManager.Instance.MaxBattery;
            float battery = DataManager.Instance.battery;

            battery += 40;
            if (battery >= maxBattery) DataManager.Instance.battery = maxBattery;
            else DataManager.Instance.battery = battery;

            Destroy(col.gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "floor")
        {
            isHurt = false;
            isFloor = true;
            if(fall != null)
            {
                float distance = fall.y - transform.position.y;
                if (distance > 10)
                {
                    DataManager.Instance.HP -= distance;
                }
            }
            animator.SetBool("IsFall", false);
            animator.SetBool("IsHurt", false);
        }
        else if (col.gameObject.CompareTag("DeadFloor"))
        {
            DataManager.Instance.HP = 0;
        }     
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        animator.SetBool("IsFall", false);
        IsFloor = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            IsFloor = false;
            fall = transform.position;
            timeCount = 0;
        }
            
    }


    public void Hurt(float hp)
    {
        if (fairyMagic)
        {
            if (itemUse.activeSelf)
            {
                itemUse.SetActive(false);
            }

            itemUse.SetActive(true);
            fairyMagic = false;
            return;
        }
        if(!isHurt && !DataManager.Instance.gameOver)
        {
            isHurt = true;
            rigidbody.AddForce(new Vector2(0, jump * Time.deltaTime));
        }
            
        DataManager.Instance.HP -= hp;

        animator.SetBool("IsHurt", true);
    }

    private void Move()
    {
        if (isFloor)
        {
            animator.SetBool("IsWalk", true);
            //x에 따라 캐릭터의 방향과 이동 조종!
            if (leftJoystickInput.x > 0)
            {
                player.transform.localScale = new Vector3(-0.7f, 0.7f, 1);            }
            else
            {
                player.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            }
            player.transform.position += new Vector3(leftJoystickInput.x * Time.deltaTime * moveSpeed, 0, 0);
        }
    }

    private void LightOn()
    {
        light.SetActive(true);
        if (DataManager.Instance.battery > 0)
            light.SetActive(true);
        animator.SetFloat("Light", rightJoystickInput.y);
    }

}