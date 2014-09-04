using UnityEngine;
using System.Collections;


public enum GestureState
{
    None,
    DoubleTap,
    SwipeLeft,
    SwipeRight,
    SwipeUp,
    SwipeDown
}

public class GestureManager : Singleton<GestureManager> 
{
    public float swipeDistanceX = 0.1f;
    public float swipeDistanceY = 0.1f;

    [HideInInspector]
    public static GestureState State;

    public static Vector2 orgLoc;

    private int touchID = 0;

    void Start()
    {
        swipeDistanceX = Screen.width * swipeDistanceX;
        swipeDistanceY = Screen.height * swipeDistanceY;
    }

    void Update()
    {
        State = GestureState.None;

        foreach (Touch i in Input.touches)
        {
            if (i.phase == TouchPhase.Ended && i.tapCount == 2)
            {
                State = GestureState.DoubleTap;
            }

            if (i.phase == TouchPhase.Began)
                orgLoc = i.position;
            else if (i.phase == TouchPhase.Ended)
            {
                if (Mathf.Abs(i.position.x - orgLoc.x) > swipeDistanceX)
                {
                    if (i.position.x - orgLoc.x < 0)
                        State = GestureState.SwipeLeft;
                    else
                        State = GestureState.SwipeRight;
                }
                else if (Mathf.Abs(i.position.y - orgLoc.y) > swipeDistanceY)
                {
                    if (i.position.y - orgLoc.y < 0)
                        State = GestureState.SwipeDown;
                    else
                        State = GestureState.SwipeUp;
                }
            }
        }
    }

    public static Vector2 GetPos(bool newTouch)
    {
        if (newTouch)
            Instance.touchID = Input.touches.Length - 1;
        return Input.touches[Instance.touchID].position;
    }

    public static Touch GetLastTouch()
    {
        return Input.touches[Input.touchCount - 1];
    }
}
