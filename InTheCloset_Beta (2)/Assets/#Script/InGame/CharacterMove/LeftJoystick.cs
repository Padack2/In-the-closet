using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LeftJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public int joystickHandleDistance = 4;  //조이스틱의 핸들이 움직일 수 있는 범위

    private Image bgImage; //조이스틱의 배경 이미지를 담음
    private Image joystickKnobImage; // 조이스틱의 핸들 저장
    private Vector3 inputVector; // 정규화시켜 방향값만을 가지고 있는 위치정보
    private Vector3 unNormalizedInput; // 비정규화된(크기 포함) 위치 정보

    private void Start()
    {
        if (GetComponent<Image>() != null && transform.GetChild(0).GetComponent<Image>() != null)
        {
            bgImage = GetComponent<Image>();    //핸들의 정보와 배경의 정보를 불러와 저장
            joystickKnobImage = transform.GetChild(0).GetComponent<Image>();
        }

    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 localPoint = Vector2.zero;

        // 터치한 점이 배경이미지 Rect안에 있는 경우
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, ped.position, ped.pressEventCamera, out localPoint))
        {
            //localPoint는 Rect상의 위치를 불러옴. 크기를 1~0으로 나타내기 위해 Rect의 크기로 나눠줌.
            localPoint.x = (localPoint.x / bgImage.rectTransform.sizeDelta.x);
            localPoint.y = (localPoint.y / bgImage.rectTransform.sizeDelta.y);

            inputVector = new Vector3(localPoint.x * 2 + 1, localPoint.y * 2 - 1, 0);
            
            unNormalizedInput = inputVector;

            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector; // 규모가 1이 넘어가면 정규화 시켜 방향 정보만을 남겨 1이상 크기가 올라가지 못하도록 함.

            //핸들을 이동시키기 위한 코드. 앵커를 기준으로 이미지를 이동시켜주기 위해 다시 포지션 값을 곱해주고 이동범위 한정을 위해 핸들의 거리로 한번 더 나눠준다.
            joystickKnobImage.rectTransform.anchoredPosition =
             new Vector3(inputVector.x * (bgImage.rectTransform.sizeDelta.x / joystickHandleDistance),
                         inputVector.y * (bgImage.rectTransform.sizeDelta.y / joystickHandleDistance));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickKnobImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public Vector3 GetInputDirection()
    {
        return new Vector3(inputVector.x, inputVector.y, 0); 
    }
}