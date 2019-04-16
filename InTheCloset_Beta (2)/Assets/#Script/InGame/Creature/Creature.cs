using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public abstract class Creature : MonoBehaviour {

    [Header("Base")]
    public float speed;
    public float DelayTime;
    public PlayerCheck player;
    public float fear;
    [Space]

    protected SkeletonAnimation creature;
    protected string cur_animation = "";




    // Use this for initialization
    protected virtual void Start () {
        creature = gameObject.GetComponent<SkeletonAnimation>();
    }

    protected void SetAnimation(string name, bool loop)
    {
        if (name == cur_animation)
            return;
        else
        {
            creature.state.SetAnimation(0, name, loop);
            cur_animation = name;
        }
    }

    protected void SetAnimation(string name, bool loop, float timeScale)
    {
        if (name == cur_animation)
            return;
        else
        {
            creature.state.SetAnimation(0, name, loop).timeScale = timeScale;
            cur_animation = name;
        }
    }
}
