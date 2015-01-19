using UnityEngine;
using System.Collections;


public enum GestureState
{
    DoubleTap,
    SwipeLeft,
    SwipeRight,
    SwipeUp,
    SwipeDown
}

public class GestureManager : Singleton<GestureManager> 
{
    public delegate void GestureDelegate(GestureState eventData);
    public static event GestureDelegate onGesture;

    public float swipeDistanceX = 0.1f;
    public float swipeDistanceY = 0.1f;

    public static Vector2 orgLoc;

    private bool dPadDown = false;

    void Start()
    {
        swipeDistanceX = Screen.width * swipeDistanceX;
        swipeDistanceY = Screen.height * swipeDistanceY;
    }

    void Update()
    {
        Gestures();
        GamePad();
    }

    void Gestures()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).tapCount == 2)
            {
                if (onGesture != null)
                    onGesture(GestureState.DoubleTap);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
                orgLoc = Input.GetTouch(0).position;

            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (Mathf.Abs(Input.GetTouch(0).position.x - orgLoc.x) > swipeDistanceX)
                {
                    if (Input.GetTouch(0).position.x - orgLoc.x < 0)
                    {
                        if (onGesture != null)
                            onGesture(GestureState.SwipeLeft);
                    }
                    else
                    {
                        if (onGesture != null)
                            onGesture(GestureState.SwipeRight);
                    }
                }
                else if (Mathf.Abs(Input.GetTouch(0).position.y - orgLoc.y) > swipeDistanceY)
                {
                    if (Input.GetTouch(0).position.y - orgLoc.y < 0)
                    {
                        if (onGesture != null)
                            onGesture(GestureState.SwipeDown);
                    }
                    else
                    {
                        if (onGesture != null)
                            onGesture(GestureState.SwipeUp);
                    }
                }
            }
        }
    }

    void GamePad()
    {
        if (Input.GetAxis("DPadHorz") == 0 && Input.GetAxis("DPadVert") == 0)
            dPadDown = false;

        if (dPadDown == false)
        {
            if (Input.GetAxis("DPadHorz") == -1)
            {
                dPadDown = true;
                if (onGesture != null)
                    onGesture(GestureState.SwipeLeft);
            }
            else if (Input.GetAxis("DPadHorz") == 1)
            {
                dPadDown = true;
                if (onGesture != null)
                    onGesture(GestureState.SwipeRight);
            }
            else if (Input.GetAxis("DPadVert") == 1)
            {
                dPadDown = true;
                if (onGesture != null)
                    onGesture(GestureState.SwipeUp);
            }
            else if (Input.GetAxis("DPadVert") == -1)
            {
                dPadDown = true;
                if (onGesture != null)
                    onGesture(GestureState.SwipeDown);
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if (onGesture != null)
                    onGesture(GestureState.DoubleTap);
            }
        }
    }
}
