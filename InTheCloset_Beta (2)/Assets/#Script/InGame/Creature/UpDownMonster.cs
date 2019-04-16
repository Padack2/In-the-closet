using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class UpDownMonster : Creature {

    public enum Kind{
        LIGHT_UP = 1, LIGHT_DOWN = 2
    }

    public Kind kind = Kind.LIGHT_UP;
    GameObject plight;
    public float Damage;

    bool IsLighting;
    bool IsOut;


    // Use this for initialization
    protected override void Start () {
        base.Start();
        plight = GameObject.FindGameObjectWithTag("Light");
		if(kind == Kind.LIGHT_UP)
        {
            SetAnimation("in", false);
            IsOut = false;
        }
        else
        {
            SetAnimation("idle", true);
            IsOut = true;
        }
	}

    public void Light()
    {
        if (kind == Kind.LIGHT_DOWN)
        {
            SetAnimation("in", false);
            IsOut = false;
        }
        else
        {
            IsOut = true;
            StartCoroutine(Out()); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light") && plight.activeSelf)
        {
            IsLighting = true;
            Light();
        }
        if(collision.gameObject.CompareTag("Player") && IsOut)
        {
            GameObject.FindWithTag("Player").GetComponent<DualJoystickPlayerController>().Hurt(Damage);
        }
    }

    private void FixedUpdate()
    {
        if (!plight.activeSelf && IsLighting)
        {
            LightOff();
            IsLighting = false;
        }
    }

    public void LightOff()
    {
        if (IsLighting)
        {
            if (kind == Kind.LIGHT_UP)
            {
                SetAnimation("in", false);
                IsOut = false;
            }
            else
            {
                StartCoroutine(Out());
                IsOut = true;
            }
        }
    }

    IEnumerator Out()
    {
        SetAnimation("out", false);
        yield return new WaitForSeconds(1.0f);
        SetAnimation("idle", true);

    }
}
