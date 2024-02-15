using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip voiceLine;
    [SerializeField] private AudioSource playerSource;
    [SerializeField] private Light indicatorLight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playerSource.Stop();
        playerSource.clip = voiceLine;
        playerSource.Play();
        Destroy(gameObject);
        Destroy(indicatorLight);
    }
}
