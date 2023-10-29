using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    public float intensity = 1f;

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking() 
    {
        Vector3 startPosition = transform.position;
        Vector3 shakeY;

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/duration) * intensity;
            
            shakeY = startPosition + Random.insideUnitSphere * strength;
            shakeY.x = startPosition.x;
            shakeY.z = startPosition.z;
            transform.position = shakeY;

            yield return null;
        }

        transform.position = startPosition;
    }
}
