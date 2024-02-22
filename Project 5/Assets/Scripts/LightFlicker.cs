using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private float nextFlicker;
    private float timer;
    private Light objLight;
    
    // Start is called before the first frame update
    void Start()
    {
        nextFlicker = Random.Range(.2f, 8f);
        timer = 0f;
        objLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= nextFlicker) {
            Flicker();
        }
    }

    void Flicker() {
        timer = 0f;
        if (objLight.enabled) {
            nextFlicker = Random.Range(.1f, .5f);
        }
        else nextFlicker = Random.Range(.2f, 8f);
        objLight.enabled = !objLight.enabled;
    }
}
