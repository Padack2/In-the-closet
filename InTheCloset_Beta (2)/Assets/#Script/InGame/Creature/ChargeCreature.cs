using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeCreature : Creature {

    [Header("Attack")]
    public float attackSpeed;
    public float Damage;

    bool IsAttack = false;

    private void FixedUpdate()
    {
        if (IsAttack)
        {
            SetAnimation("WALK", true, attackSpeed);
            gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0);
            
        }
        else if (player.IsPlayer)
        {
            DataManager.Instance.fear -= Time.deltaTime * fear;
            IsAttack = true;
            if (player.Distance > 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1);
            }
            else
            {
                speed = -speed;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<DualJoystickPlayerController>().Hurt(Damage);
        }
    }
}
