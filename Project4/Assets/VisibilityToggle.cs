using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    private Light lght;
    // Start is called before the first frame update
    void Start()
    {
        lght = GetComponent<Light>();
        lght.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        lght.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        lght.enabled = false;
    }
}
