using UnityEngine;
using System.Collections;

public class DollyZoom : MonoBehaviour
{
    //public Transform target;
    public Vector3 target;
    private float initHeightAtDist;

    //TweenVars
    public float perspFov = 90f;
    public float orthoFov = 1f;
    public float TweenSpeed = 0.5f;

    //ShakeVars
    public Vector3 ShakeRange = Vector3.one;

    public delegate void DollyZoomEvents();
    public static event DollyZoomEvents onOrtho;

    float FrustumHeightAtDistance(float distance)
    {
        return 2f * distance * Mathf.Tan(gameObject.camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FovForHeightAndDistance(float height, float distance)
    {
        return 2f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }
    // new func
    float DistanceForHeightAndFov(float height, float fov)
    {
        return height * 0.5f / Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
    }

    void Start()
    {
        float distance = Vector3.Distance(transform.position, target);
        initHeightAtDist = FrustumHeightAtDistance(distance);
    }

    void LateUpdate()
    {
        float curDistance = Vector3.Distance(transform.position, target);
        float needDistance = DistanceForHeightAndFov(initHeightAtDist, gameObject.camera.fieldOfView);
        float diff = curDistance - needDistance;
        transform.Translate(0, 0, diff);
    }

    public void StartOrthoTween(bool value)
    {
        if (value && camera.fieldOfView == 90)
            StartCoroutine(OrthoTween(false));
        else if (!value && camera.fieldOfView == 1)
            StartCoroutine(OrthoTween(true));
    }

    IEnumerator OrthoTween(bool value)
    {
        float start = (value) ? orthoFov : perspFov;
        float end = (value) ? perspFov : orthoFov;

        float startTime = Time.time;
        float count = 0f;
        while (count < 1f)
        {
            count = (Time.time - startTime) / TweenSpeed;
            camera.fieldOfView = Mathf.Lerp(start, end, count);
            yield return null;
        }
        if (onOrtho != null && !value)
            onOrtho();
    }

    public void StartShakeTween()
    {
        StartCoroutine(ShakeTween());
    }

    IEnumerator ShakeTween()
    {
        float ShakeSpeed = 50f;
        while(ShakeSpeed > 0)
        {
            Camera.main.transform.position = Vector3.Scale(SmoothRandom.GetVector3(ShakeSpeed), Vector3.one);
            transform.position = transform.position + Vector3.Scale(SmoothRandom.GetVector3(ShakeSpeed--), ShakeRange);
            yield return null;
        }
    }
}
