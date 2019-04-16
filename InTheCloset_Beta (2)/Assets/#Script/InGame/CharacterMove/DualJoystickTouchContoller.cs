/*
about this script: 

if a joystick is not set to stay in a fixed position
 for the left joystick, this script makes it appear and disappear within the left-side half of the screen where the screen was touched 
 for the right joystick, this script makes it appear and  disappear within the right-side half of the screen where the screen was touched 

if a joystick is set to stay in a fixed position
 for the left joystick, this script makes it appear and disappear if the user touches within the area of its background image (even if it is not currently visible)
 for the right joystick, this script makes it appear and disappear if the user touches within the area of its background image (even if it is not currently visible)
 
this script also keeps one or both joysticks always visible
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DualJoystickTouchContoller : MonoBehaviour
{
    public Image leftJoystickBackgroundImage; // background image of the left joystick (the joystick's handle (knob) is a child of this image and moves along with it)
    public Image rightJoystickBackgroundImage; // background image of the right joystick (the joystick's handle (knob) is a child of this image and moves along with it)
    //조이스틱 항상 보이게 할거임?

    private Image leftJoystickHandleImage; // handle (knob) image of the left joystick
    private Image rightJoystickHandleImage; // handle (knob) image of the right joystick
    private LeftJoystick leftJoystick; // script component attached to the left joystick's background image
    private RightJoystick rightJoystick; // script component attached to the right joystick's background image
    private int leftSideFingerID = 0; // unique finger id for touches on the left-side half of the screen
    private int rightSideFingerID = 0; // unique finger id for touches on the right-side half of the screen


    void Start()
    {

        if (leftJoystickBackgroundImage.GetComponent<LeftJoystick>() == null)
        {
            //Debug.LogError("There is no LeftJoystick script attached to the Left Joystick game object.");
        }
        else
        {
            leftJoystick = leftJoystickBackgroundImage.GetComponent<LeftJoystick>(); // gets the left joystick script
        }

        if (leftJoystick.transform.GetChild(0).GetComponent<Image>() == null)
        {
            //Debug.LogError("There is no left joystick handle image attached to this script.");
        }
        else
        {
            leftJoystickHandleImage = leftJoystick.transform.GetChild(0).GetComponent<Image>(); // gets the handle (knob) image of the left joystick
        }


        if (rightJoystickBackgroundImage.GetComponent<RightJoystick>() == null)
        {
            //Debug.LogError("There is no RightJoystick script attached to Right Joystick game object.");
        }
        else
        {
            rightJoystick = rightJoystickBackgroundImage.GetComponent<RightJoystick>(); // gets the right joystick script
        }

        if (rightJoystick.transform.GetChild(0).GetComponent<Image>() == null)
        {
            //Debug.LogError("There is no right joystick handle attached to this script.");
        }
        else
        {
            rightJoystickHandleImage = rightJoystick.transform.GetChild(0).GetComponent<Image>(); // gets the handle (knob) image of the right joystick
        } 
    }

    void Update()
    {
        // can move code from FixedUpdate() to Update() if your controlled object does not use physics
        // can move code from Update() to FixedUpdate() if your controlled object does use physics
        // can see which one works best for your project
    }

    void FixedUpdate()
    {
        //입력이 있으면 
        if (Input.touchCount > 0)
        {
            Touch[] myTouches = Input.touches; //터치(들)를 배열에 저장

            // loops through all the current touches
            for (int i = 0; i < Input.touchCount; i++)  //터치가 감지된 수만큼 루프
            {
                // 터치가 방금 눌린 거라면
                if (myTouches[i].phase == TouchPhase.Began)
                {
                        // 이터치가 화면 왼쪽 절반 안에 있으면
                        if (myTouches[i].position.x < Screen.width / 2)
                        {
                            leftSideFingerID = myTouches[i].fingerId; // 이 터치의 아이디를 저장함.

                            if ((myTouches[i].position.x <= leftJoystickBackgroundImage.rectTransform.position.x) && (myTouches[i].position.x >= (leftJoystickBackgroundImage.rectTransform.position.x - leftJoystickBackgroundImage.rectTransform.sizeDelta.x)))
                            {
                                // and the touch also happens within the left joystick's background image y coordinate
                                if ((myTouches[i].position.y >= leftJoystickBackgroundImage.rectTransform.position.y) && (myTouches[i].position.y <= (leftJoystickBackgroundImage.rectTransform.position.y + leftJoystickBackgroundImage.rectTransform.sizeDelta.y)))
                                {
                                    // 조이스틱이 나타난드아
                                    leftJoystickBackgroundImage.enabled = true;
                                    leftJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                                }
                            }
                        }

                    // if this touch is on the right-side half of screen
                    if (myTouches[i].position.x > Screen.width / 2)
                    {
                        rightSideFingerID = myTouches[i].fingerId; // stores the unique id for this touch that happened on the right-side half of the screen

                        // if the right joystick will drag with any touch
                        if (rightJoystick.joystickStaysInFixedPosition == false)
                        {
                            var currentPosition = rightJoystickBackgroundImage.rectTransform.position; // gets the current position of the right joystick
                            currentPosition.x = myTouches[i].position.x + rightJoystickBackgroundImage.rectTransform.sizeDelta.x / 2; // calculates the x position of the right joystick to where the screen was touched
                            currentPosition.y = myTouches[i].position.y - rightJoystickBackgroundImage.rectTransform.sizeDelta.y / 2; // calculates the y position of the right joystick to where the screen was touched

                            // keep the right joystick on the right-side half of the screen
                            currentPosition.x = Mathf.Clamp(currentPosition.x, Screen.width / 2 + rightJoystickBackgroundImage.rectTransform.sizeDelta.x, Screen.width);
                            currentPosition.y = Mathf.Clamp(currentPosition.y, 0, Screen.height - rightJoystickBackgroundImage.rectTransform.sizeDelta.y);

                            rightJoystickBackgroundImage.rectTransform.position = currentPosition; // sets the position of the right joystick to where the screen was touched (limited to the right half of the screen)

                            // enables right joystick on touch
                            rightJoystickBackgroundImage.enabled = true;
                            rightJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                        }
                        else
                        {
                            // right joystick stays fixed, does not set position of right joystick on touch

                            // if the touch happens within the fixed area of the right joystick's background image within the x coordinate
                            if ((myTouches[i].position.x <= rightJoystickBackgroundImage.rectTransform.position.x) && (myTouches[i].position.x >= (rightJoystickBackgroundImage.rectTransform.position.x - rightJoystickBackgroundImage.rectTransform.sizeDelta.x)))
                            {
                                // and the touch also happens within the right joystick's background image y coordinate
                                if ((myTouches[i].position.y >= rightJoystickBackgroundImage.rectTransform.position.y) && (myTouches[i].position.y <= (rightJoystickBackgroundImage.rectTransform.position.y + rightJoystickBackgroundImage.rectTransform.sizeDelta.y)))
                                {
                                    // makes the right joystick appear
                                    rightJoystickBackgroundImage.enabled = true;
                                    rightJoystickBackgroundImage.rectTransform.GetChild(0).GetComponent<Image>().enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
