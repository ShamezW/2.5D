using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour 
{
    public bool Shaking;
    private float ShakeDecay;
    private float ShakeIntensity;
    private Vector3 OriginalPos;
    private Quaternion OriginalRot;

    void Start()
    {
        Shaking = false;
    }

    void Update()
    {
        if (ShakeIntensity > 0)
        {
            transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
            transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity) * 0.2f,
                                      OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity) * 0.2f,
                                      OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity) * 0.2f,
                                      OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity) * 0.2f);

            ShakeIntensity -= ShakeDecay;
        }
        else if (Shaking)
        {
            Shaking = false;
            transform.position = OriginalPos;
            transform.rotation = OriginalRot;
        }

    }

    public void Play()
    {
        OriginalPos = transform.position;
        OriginalRot = transform.rotation;

        ShakeIntensity = 0.2f;
        ShakeDecay = 0.02f;
        Shaking = true;
    }   
}
