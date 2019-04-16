using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider : MonoBehaviour {

    Collider2D tempCollider;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "gem")
        {
            JemController jem = col.gameObject.GetComponent<JemController>();
            jem.nowTime = Time.time;
            jem.isLight = true;
            tempCollider = col;
        }
        else if (col.gameObject.tag == "flower")
        {
            flower flo = col.gameObject.GetComponent<flower>();
            flo.isLight = true;
            tempCollider = col;

        }
        else if(col.gameObject.CompareTag("fadeFloor"))
        {
            FadeFloor fadeflo = col.gameObject.GetComponent<FadeFloor>();

            fadeflo.isLight = true;
            tempCollider = col;

        }else if (col.gameObject.CompareTag("moveWall"))                                
        {
            MoveWall mw = col.gameObject.GetComponent<MoveWall>();

            mw.isLight = true;
            tempCollider = col;
        }else if (col.gameObject.CompareTag("ClearLamp"))
        {
            //Debug.Log(col.gameObject.tag);
            ClearLamp lamp = col.gameObject.GetComponent<ClearLamp>();

            lamp.nowTime = Time.time;
            lamp.isLight = true;
            tempCollider = col;
        }else if (col.gameObject.CompareTag("cave"))
        {
            CaveGem CG = col.GetComponent<CaveGem>();
            CG.isLight = true;
            tempCollider = col;
        }else if(col.gameObject.CompareTag("Normal"))
        {
            col.GetComponent<NormalCreature>().Delay();
        }else if (col.gameObject.CompareTag("A3"))
        {
            col.GetComponent<ReadyNormalCreature>().Delay();
        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("cave"))
        {
            CaveGem CG = col.GetComponent<CaveGem>();
            CG.isLight = true;
            tempCollider = col;
        }
        else if (col.gameObject.CompareTag("Normal"))
        {
            col.GetComponent<NormalCreature>().Delay();
        }
        else if (col.gameObject.CompareTag("A3"))
        {
            col.GetComponent<ReadyNormalCreature>().Delay();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "gem")
        {
            JemController jem = col.gameObject.GetComponent<JemController>();
            if (!jem.isOk)
            {
                jem.isLight = false;
                jem.GemLight.intensity = 0;
            }
        }
        else if (col.gameObject.tag == "flower")
        {
            flower flo = col.gameObject.GetComponent<flower>();
            flo.isLight = false;
        }
        else if (col.gameObject.CompareTag("fadeFloor"))
        {
            FadeFloor fadeflo = col.gameObject.GetComponent<FadeFloor>();

            fadeflo.isLight = false;
            if (!fadeflo.isOk)
            {
                fadeflo.gemLight.intensity = 0;
            }
        }
        else if (col.gameObject.CompareTag("moveWall"))
        {
            MoveWall mw = col.gameObject.GetComponent<MoveWall>();

            mw.isLight = false;
        }
        else if (col.gameObject.tag == "ClearLamp")
        {
            ClearLamp lamp = col.gameObject.GetComponent<ClearLamp>();

            if (!lamp.isOk)
            {
                lamp.isLight = false;
                lamp.GemLight.intensity = 0;
            }
        }else if (col.gameObject.CompareTag("cave"))
        {
            CaveGem CG = col.GetComponent<CaveGem>();
            if (!CG.one)
            {
                CG.isLight = false;

                if (CG.gemLight.intensity < 2)
                {
                    CG.gemLight.intensity = 0;
                }
            }
            
        }
    }

    public void GemNoActive()
    {
        if (tempCollider != null)
        {
            if (tempCollider.gameObject.CompareTag("gem"))
            {
                JemController jem = tempCollider.gameObject.GetComponent<JemController>();
                if (!jem.isOk)
                {
                    jem.isLight = false;
                    jem.GemLight.intensity = 0;
                }
            }
            else if (tempCollider.gameObject.CompareTag("flower"))
            {
                flower flo = tempCollider.gameObject.GetComponent<flower>();
                if (flo.isLight)
                {
                    flo.isLight = false;
                    if (!flo.isOk)
                    {
                        flo.flowerLight.intensity = 0;
                    }
                }
            }
            else if (tempCollider.gameObject.CompareTag("fadeFloor"))
            {
                FadeFloor fadeflo = tempCollider.gameObject.GetComponent<FadeFloor>();
                if (!fadeflo.isOk)
                {
                    fadeflo.isLight = false;
                    fadeflo.gemLight.intensity = 0;
                }
            }
            else if (tempCollider.gameObject.CompareTag("moveWall"))
            {
                MoveWall mw = tempCollider.gameObject.GetComponent<MoveWall>();
                if (!mw.isOk)
                {
                    mw.isLight = false;
                }
                if (!mw.isCheck)
                {
                    mw.gemLight.intensity = 0;
                }
            }
            else if (tempCollider.gameObject.CompareTag("ClearLamp"))
            {
                ClearLamp lamp = tempCollider.gameObject.GetComponent<ClearLamp>();
                if (!lamp.isOk)
                {
                    lamp.isLight = false;
                    lamp.GemLight.intensity = 0;
                }

            }else if (tempCollider.gameObject.CompareTag("cave"))
            {
                CaveGem CG = tempCollider.GetComponent<CaveGem>();
                if (!CG.isOk)
                {
                    CG.isLight = false;
                }
                if (CG.gemLight.intensity < 2)
                {
                    CG.gemLight.intensity = 0;
                }
            }
        }
    }
}
