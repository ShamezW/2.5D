using UnityEngine;
using System.Collections;

public class BaseCube : MonoBehaviour 
{
    public float rotSpeed = 0.3f;

    private Quaternion startAngle;
    private Quaternion targetAngle;
	
    void Start()
    {
        GameManager.onLevelCompleated += OnCompleated;
        GameManager.onMenuActive += OnMenu;
        GameManager.onGameActive += OnGame;
        GestureManager.onGesture += OnGesture;
    }

    IEnumerator Swipe()
    {
        float start = Time.time;
        float count = 0f;
        while(count < 1f)
        {
            count = (Time.time - start) / 0.2f;
            transform.rotation = Quaternion.Slerp(startAngle, targetAngle, count);
            yield return null;
        }
    }

    public void CleanUp()
    {
        foreach(Transform i in transform)
        {
            Destroy(i.gameObject);
        }
    }

    #region events
    void OnCompleated()
    {
        startAngle = transform.rotation;
        targetAngle = Quaternion.identity;
        StartCoroutine(Swipe());
    }

    void OnMenu()
    {
        gameObject.SetActive(false);
    }

    void OnGame()
    {
        gameObject.SetActive(true);
    }

    void OnGesture(GestureState eventData)
    {
        if (GameManager.mode == GameMode.Game && !GameManager.orthoToggle)
        {
            if (eventData == GestureState.SwipeLeft)
            {
                startAngle = transform.rotation;
                targetAngle = Quaternion.AngleAxis(90f, Vector3.up) * transform.rotation;
                StartCoroutine(Swipe());
            }
            else if (eventData == GestureState.SwipeRight)
            {
                startAngle = transform.rotation;
                targetAngle = Quaternion.AngleAxis(90f, -Vector3.up) * transform.rotation;
                StartCoroutine(Swipe());
            }
            else if (eventData == GestureState.SwipeUp)
            {
                startAngle = transform.rotation;
                targetAngle = Quaternion.AngleAxis(90f, Vector3.right) * transform.rotation;
                StartCoroutine(Swipe());
            }
            else if (eventData == GestureState.SwipeDown)
            {
                startAngle = transform.rotation;
                targetAngle = Quaternion.AngleAxis(90f, -Vector3.right) * transform.rotation;
                StartCoroutine(Swipe());
            }
        }
    }
    #endregion
}
