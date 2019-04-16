using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public bool one = true;
    public bool isLight = false;
    public bool isOk = false;
    public bool isCheck = false;
    public bool wall = true;


    public Light gemLight;
    public GameObject floorWall;
    public GameObject center;

    public float moveSpeed;

    private void Update()
    {
	
        if (one)
        {
            if (isLight)
            {
                if (gemLight.intensity< 2)
                {

                    gemLight.intensity += 0.05f;
                }
                else
                {
                    isOk = true;
                }
            }
            if (isOk)
            {
                one = false;
                isOk = false;
                isCheck = true;
                StartCoroutine(Move());
            }
        }
    }

    IEnumerator Move()
    {
        while (isCheck)
        {
            if (wall)
            {
                if (floorWall.transform.rotation.z < 0)
                {
                    wall = false;
                    isCheck = false;
                    one = true;
                    gemLight.intensity = 0;
                    yield break;
               }
                floorWall.transform.RotateAround(center.transform.position, new Vector3(0,0,1),-moveSpeed * Time.deltaTime);
            }
            else
            {
                if (floorWall.transform.rotation.z > 0.7f)
                {
                    wall = true;
                    isCheck = false;
                    one = true;
                    gemLight.intensity = 0;
                    yield break;
                }
                floorWall.transform.RotateAround(center.transform.position, new Vector3(0,0,1), moveSpeed * Time.deltaTime);
            }
            yield return null;

        }
    }
    


}
