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
    }

	void Update() 
    {
        if (!GameManager.orthoToggle)
            SwipeControls();

	}

    void SwipeControls()
    {
        if (GestureManager.State == GestureState.SwipeLeft)
        {
            startAngle = transform.rotation;
            targetAngle = Quaternion.AngleAxis(90f, Vector3.up) * transform.rotation;
            StartCoroutine(Swipe());
        }
        else if (GestureManager.State == GestureState.SwipeRight)
        {
            startAngle = transform.rotation;
            targetAngle = Quaternion.AngleAxis(90f, -Vector3.up) * transform.rotation;
            StartCoroutine(Swipe());
        }
        else if (GestureManager.State == GestureState.SwipeUp)
        {
            startAngle = transform.rotation;
            targetAngle = Quaternion.AngleAxis(90f, Vector3.right) * transform.rotation;
            StartCoroutine(Swipe());
        }
        else if (GestureManager.State == GestureState.SwipeDown)
        {
            startAngle = transform.rotation;
            targetAngle = Quaternion.AngleAxis(90f, -Vector3.right) * transform.rotation;
            StartCoroutine(Swipe());
        }
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

    void OnCompleated()
    {
        startAngle = transform.rotation;
        targetAngle = Quaternion.identity;
        StartCoroutine(Swipe());
    }
}
