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
        foreach (Touch i in Input.touches)
        {
            if (i.phase == TouchPhase.Ended && i.tapCount == 2)
            {
                if (onGesture != null)
                    onGesture(GestureState.DoubleTap);
            }

            if (i.phase == TouchPhase.Began)
                orgLoc = i.position;

            else if (i.phase == TouchPhase.Ended)
            {
                if (Mathf.Abs(i.position.x - orgLoc.x) > swipeDistanceX)
                {
                    if (i.position.x - orgLoc.x < 0)
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
                else if (Mathf.Abs(i.position.y - orgLoc.y) > swipeDistanceY)
                {
                    if (i.position.y - orgLoc.y < 0)
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
