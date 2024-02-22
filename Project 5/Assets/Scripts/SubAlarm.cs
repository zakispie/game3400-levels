using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAlarm : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private AudioSource source;
    private float timer;
    private int currClip;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        timer = 20f;
        currClip = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currClip >= clips.Length) currClip = 0;
        timer += Time.deltaTime;
        if (timer >= 30f) {
            source.clip = clips[currClip];
            source.Play();
            Debug.Log("played sound " + currClip);
            currClip++;
            timer = 0f;
        }
    }
}
