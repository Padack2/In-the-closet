using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour {

    public float moveDistance;
    public float moveSpeed=0.1f;

    private Vector2 leftDestination;
    private Vector2 rightDestination;

    private bool isleft = false;
    private bool playerMove = false;


    private void Start()
    {
        leftDestination = gameObject.transform.position;
        rightDestination = gameObject.transform.position + new Vector3(moveDistance,0);
    }

    // Update is called once per frame
    void Update () {
        if (isleft)
        {
            gameObject.transform.position -= new Vector3(moveSpeed, 0);
            if (gameObject.transform.position.x < leftDestination.x) isleft = false;
        }
        else
        {
            gameObject.transform.position += new Vector3(moveSpeed, 0);
            if (gameObject.transform.position.x > rightDestination.x) isleft = true;
        }
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerMove = true;
            StartCoroutine(PlayerMove(col));
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            playerMove = false;
            StopCoroutine(PlayerMove(col));
        }
    }

    IEnumerator PlayerMove(Collision2D player)
    {
        while (playerMove)
        {
            if (isleft)
            {
                player.gameObject.transform.position -= new Vector3(moveSpeed, 0);
                if (gameObject.transform.position.x < leftDestination.x) isleft = false;
            }
            else
            {
                player.gameObject.transform.position += new Vector3(moveSpeed, 0);
                if (gameObject.transform.position.x > rightDestination.x) isleft = true;
            }
            yield return null;
        }
    }
}
