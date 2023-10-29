using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light _light;
    private void Start() {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _light.intensity = Mathf.PerlinNoise(Time.time * 10, -Time.time * 10) * 3f;
    }
}
