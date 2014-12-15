using UnityEngine;
using System.Collections;

public class DollyZoom : MonoBehaviour
{
    //public Transform target;
    public Vector3 target;
    float initHeightAtDist;

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
}
