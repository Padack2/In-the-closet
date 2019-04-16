using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour {

    public bool IsPlayer = false;
    public float Distance;
    public float absDistnace = 0;
    public float MaxDistnace = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Distance = other.transform.position.x - gameObject.transform.position.x;
            IsPlayer = true;
        }
           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            IsPlayer = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Distance = collision.transform.position.x - gameObject.transform.position.x;    //음수면 플레이어는 왼쪽, 
            absDistnace = Vector2.Distance(collision.transform.position, gameObject.transform.position);
            if (MaxDistnace < absDistnace) MaxDistnace = absDistnace;
        }
            
    }
}
