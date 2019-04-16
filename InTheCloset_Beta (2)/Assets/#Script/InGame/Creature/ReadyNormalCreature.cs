using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class ReadyNormalCreature : Creature {

    [Header("Attack")]
    public float attackTime;
    public float Damage;

    bool IsDelay = false;
    bool IsAttack = false;
    bool IsPlayer = false;
    int movement;

    // Update is called once per frame
    void FixedUpdate () {
        
        if (!IsDelay && !IsAttack)
        {
            
            if (player.IsPlayer)
            {
                DataManager.Instance.fear -= Time.deltaTime * fear;
                StartCoroutine(Attack());
            }
            else
            {
                SetAnimation("idle", true);
            }
        }else if(!IsDelay && IsAttack)
        {
            if (movement < 0)
            {
                gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7f, 0.7f, 1);
                gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0);
            }
        }
	}


    IEnumerator Attack()
    {
        if (!IsDelay)
        {
            if (movement < 0)
            {
                gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-0.7f, 0.7f, 1);
            }
            SetAnimation("ready", false);
            if (player.Distance < 0) movement = -1;
            else movement = 1;
            yield return new WaitForSeconds(1.5f);
            IsAttack = true;
            if (!IsDelay)
            {
                SetAnimation("walk", true);
                yield return new WaitForSeconds(attackTime);
            }
            IsAttack = false;
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator DelayCorutine()
    {
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
        if(!IsDelay)
            StartCoroutine(DelayCorutine());
    }

}
