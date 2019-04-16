using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class NormalCreature : Creature {

    [Header("Base")]
    public float JumpTime;
    public AudioSource JumpSound;
    public AudioSource AttackSound;
    [Space]

    [Header("Attack")]
    public float agroDistance;
    public float attackDistnace;
    public float attackTime;
    public float Damage;

    float nowTime;
    bool IsDelay = false;
    bool IsAttack = false;
    bool IsPlayer = false;
    AudioSource AudioPlayer;

    protected override void Start()
    {
        base.Start();
        AudioPlayer = gameObject.GetComponent<AudioSource>();
        nowTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate () {
        
        if (!IsDelay && !IsAttack)
        {
            if (player.IsPlayer)
            {
                creature.loop = true;
                if (!AudioPlayer.isPlaying)
                {
                    AudioPlayer.loop = true;
                    AudioPlayer.Play();

                }

                AudioPlayer.volume = 1-player.absDistnace/player.MaxDistnace;

                if ((player.Distance >= 0? player.Distance:-player.Distance) < attackDistnace)
                {
                    
                    StartCoroutine(Attack());
                }
                else
                {
                    if(player.absDistnace < agroDistance)
                    {
                        if (player.Distance < 0)
                        {
                            gameObject.transform.localScale = new Vector3(1, 1, 1);
                            gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0);
                        }
                        else
                        {
                            gameObject.transform.localScale = new Vector3(-1f, 1f, 1);
                            gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0);
                        }
                        if (Time.time - nowTime >= JumpTime)
                        {
                            JumpSound.Play();
                            nowTime = Time.time;
                        }

                        SetAnimation("walk", true);

                    }
                    else
                    {
                        SetAnimation("idle", true);
                    }
                    
                }
                DataManager.Instance.fear -= Time.deltaTime * fear;

            }
            else
            {
                nowTime = Time.time;
                AudioPlayer.Stop();
                SetAnimation("idle", true);
            }
        }

        
	}


    IEnumerator Attack()
    {
        if (!IsDelay)
        {
            IsAttack = true;
            SetAnimation("attack", true);
            yield return new WaitForSeconds(0.5f);
            AttackSound.Play();
            yield return new WaitForSeconds(attackTime);
            
            if (IsPlayer)
                GameObject.FindWithTag("Player").GetComponent<DualJoystickPlayerController>().Hurt(Damage * 2);
            IsAttack = false;
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator DelayCorutine()
    {
        //Debug.Log("딜레이!");
            IsDelay = true;
            SetAnimation("spasticity", false);
            yield return new WaitForSeconds(DataManager.Instance.DelayTime);
        IsDelay = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsPlayer = true;
            if (!IsDelay)
            {
                GameObject.FindWithTag("Player").GetComponent<DualJoystickPlayerController>().Hurt(Damage);
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsPlayer = false;
        }
    }

    public void Delay()
    {
        if (!IsDelay)
            StartCoroutine(DelayCorutine());
    }
}
