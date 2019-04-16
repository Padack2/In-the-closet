using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchObj : MonoBehaviour
{
    public float vine_HP = 7;

    private float playerSpeed;
    private GameObject player;
    private bool isHolding = false;
    private bool isManne = false;
    public bool isLight = false;
    private bool isOk = false;


    public Light gemLight;
    


    private bool one = true;


    //    Vector3 ray;

    void FixedUpdate()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit) && Time.timeScale == 1)
                {

                    if (hit.collider.CompareTag("vine"))
                    {
                        if (isHolding)
                        {
                            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5, 2);
                            vine_HP--;
                            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -0.1f);
                            if (vine_HP <= 0)
                            {
                                Destroy(gameObject);
                                isHolding = false;
                                PlayerSpeedSet();
                            }
                        }
                    }
                    else if (hit.collider.CompareTag("mannequin"))
                    {
                        if (isManne && one)
                        {
                            int random = Random.Range(0, 6);
                            if (random == 0)
                            {
                                DataManager.Instance.battery += 10;
                                if (DataManager.Instance.battery > DataManager.Instance.MaxBattery)
                                {
                                    float temp = DataManager.Instance.battery - DataManager.Instance.MaxBattery;
                                    DataManager.Instance.battery -= temp;
                                }
                            }
                            else if (random == 1)
                            {
                                DataManager.Instance.HP += 10;
                                if (DataManager.Instance.HP > DataManager.Instance.MaxHP)
                                {
                                    float temp = DataManager.Instance.HP - DataManager.Instance.MaxHP;
                                    DataManager.Instance.HP -= temp;
                                }
                            }
                            else if (random == 2)
                            {
                                DataManager.Instance.fear -= 10;
                                if (DataManager.Instance.fear < 0)
                                {
                                    float temp = 0 - DataManager.Instance.fear;
                                    DataManager.Instance.fear += temp;
                                }
                            }
                            else if (random == 3)
                            {
                                DataManager.Instance.battery -= 10;
                                if (DataManager.Instance.battery < 11)
                                {
                                    DataManager.Instance.battery = 5;
                                }
                            }
                            else if (random == 4)
                            {
                                DataManager.Instance.HP -= 10;
                                if (DataManager.Instance.HP < 11)
                                {
                                    DataManager.Instance.HP = 5;
                                }
                            }
                            else if (random == 5)
                            {
                                if (DataManager.Instance.fear + 10 > DataManager.Instance.MaxFear)
                                {
                                    DataManager.Instance.fear = DataManager.Instance.MaxFear - 5;
                                }
                                else
                                {
                                    DataManager.Instance.fear += 10;
                                }
                            }
                        }
                        //Debug.Log(DataManager.Instance.HP);
                        isManne = false;
                    }
                }
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("vine"))
            {
                isHolding = true;
                player = col.gameObject;
                playerSpeed = player.GetComponent<DualJoystickPlayerController>().moveSpeed;
                player.GetComponent<DualJoystickPlayerController>().moveSpeed = 0;
            }
            else if (gameObject.CompareTag("mannequin"))
            {
                if (one)
                {
                    isManne = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("mannequin"))
            {
                if (one)
                {
                    isManne = false;
                }
            }
        }
    }

    public void PlayerSpeedSet()
    {
        player.GetComponent<DualJoystickPlayerController>().moveSpeed = playerSpeed;
    }


    //임시
    public void OnMouseDown()
    {
        if (isHolding)
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(5, 2);
            vine_HP--;
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -0.1f);
            if (vine_HP <= 0)
            {
                Destroy(gameObject);
                isHolding = false;
                PlayerSpeedSet();
            }
        }
        if (isManne && one)
        {
            int random = Random.Range(0, 6);
            if (random == 0) {
            DataManager.Instance.battery += 10;
                if (DataManager.Instance.battery > DataManager.Instance.MaxBattery)
                {
                    float temp = DataManager.Instance.battery - DataManager.Instance.MaxBattery;
                    DataManager.Instance.battery -= temp;
                }
            }
            else if (random == 1) {
                DataManager.Instance.HP += 10;
                if (DataManager.Instance.HP > DataManager.Instance.MaxHP)
                {
                    float temp = DataManager.Instance.HP - DataManager.Instance.MaxHP;
                    DataManager.Instance.HP -= temp;
                }
            }
            else if (random == 2) {
                DataManager.Instance.fear -= 10;
                if (DataManager.Instance.fear < 0)
                {
                    float temp = 0 - DataManager.Instance.fear;
                    DataManager.Instance.fear += temp;
                }
            }
            else if (random == 3) {
                DataManager.Instance.battery -= 10;
                if (DataManager.Instance.battery < 11)
                {
                    DataManager.Instance.battery = 5;
                }
            }
            else if (random == 4) {
                DataManager.Instance.HP -= 10;
                if (DataManager.Instance.HP < 11)
                {
                    DataManager.Instance.HP = 5;
                }
            }
            else if (random == 5)
            {
                if (DataManager.Instance.fear + 10 > DataManager.Instance.MaxFear)
                {
                    DataManager.Instance.fear = DataManager.Instance.MaxFear - 5;
                }
                else
                {
                    DataManager.Instance.fear += 10;
                }
            }
        }
            //Debug.Log(DataManager.Instance.HP);
            isManne = false;
    }
}
