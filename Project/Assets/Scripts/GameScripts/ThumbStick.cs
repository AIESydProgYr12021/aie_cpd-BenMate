using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




public class ThumbStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float joystickVisualDistance = 50;

    

    private Image container;
    private Image joystick;

    public Vector2 Direction { get; private set; }

    public bool showControl = false;
    private bool isControllsShowing = true;
    public bool syncWithInput = true;
    private bool isDragging = false;

   

    // Start is called before the first frame update
    void Start()
    {
        var images = GetComponentsInChildren<Image>();

        container = images[0];
        joystick = images[1];
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        showControl = true;
#endif

        if (showControl != isControllsShowing)
        {
            container.enabled = showControl;
            joystick.enabled = showControl;
            isControllsShowing = showControl;
        }

        if (syncWithInput && !isDragging)
        {
            Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            joystick.rectTransform.anchoredPosition = new Vector3(Direction.x * joystickVisualDistance, Direction.y * joystickVisualDistance, 0);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        Vector2 pos = Vector2.zero;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(container.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            return;

        pos.x /= container.rectTransform.sizeDelta.x;
        pos.y /= container.rectTransform.sizeDelta.y;

        Vector2 pivot = container.rectTransform.pivot;

        pos.x += pivot.x - 0.5f;
        pos.y += pivot.y - 0.5f;

        pos.x = Mathf.Clamp(pos.x, -1, 1);
        pos.y = Mathf.Clamp(pos.y, -1, 1);

        Direction = pos;
        joystick.rectTransform.anchoredPosition = new Vector3(pos.x * joystickVisualDistance, pos.y * joystickVisualDistance, 0);
    }
}