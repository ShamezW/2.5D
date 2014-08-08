using UnityEngine;
using System.Collections;

public class DollyZoom : MonoBehaviour {
    public Transform target;

    private float initHeightAtDist;
    private bool dzEnabled;

    float FrustumHeightAtDistance(float distance)
    {
        return 2f * distance * Mathf.Tan(gameObject.camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    float FovForHeightAndDistance(float height, float distance)
    {
        return 2f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    void StartDZ()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(distance);
        dzEnabled = true;
    }

    void stopDZ()
    {
        dzEnabled = false;
    }

    void Start()
    {
        StartDZ();
    }

    void Update()
    {
        if (dzEnabled)
        {
            float curDistance = Vector3.Distance(transform.position, target.position);
            gameObject.camera.fieldOfView = FovForHeightAndDistance(initHeightAtDist, curDistance);
        }
    }
}
