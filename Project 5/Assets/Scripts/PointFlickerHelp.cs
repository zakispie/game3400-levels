using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFlickerHelp : MonoBehaviour
{
    [SerializeField] private Light spotLight;
    [SerializeField] private Light pointLight;
    // Update is called once per frame
    void Update()
    {
        pointLight.enabled = spotLight.enabled;
    }
}
