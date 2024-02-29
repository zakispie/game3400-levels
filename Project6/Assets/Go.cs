using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : MonoBehaviour
{
    private bool goingUp;
    private Vector3 startPosition;
    [SerializeField] private Replace replace;
    private float timer;
    private float callTimer;
    private bool startScene;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip newClip;
    [SerializeField] AudioClip whuh;
    [SerializeField] Material earthMaterial;
    private MeshRenderer mesh;
    private bool startCall;

    private void Start()
    {
        goingUp = true;
        startPosition = transform.position;
        timer = 0;
        startScene = false;
        startCall = false;
        mesh = GetComponent<MeshRenderer>();
    }
    public void go() {
        mesh.material = earthMaterial;
        
        if (goingUp)
        {
            transform.position += new Vector3(0, Time.deltaTime * 12f, 0);
            if (transform.position.y - startPosition.y >= 9f)
            {
                goingUp = false;
            }
        }
        if (!goingUp) {
            transform.position += new Vector3(0, 0, -Time.deltaTime * 40f);

            if (timer >= 3f && !startScene)
            {
                startScene = true;
                replace.replace();
                sound.clip = whuh;
                sound.Play();
            }
            else {
                timer += Time.deltaTime;
            }

            if (startScene && callTimer >= 20f && !startCall)
            {
                startCall = true;
                sound.clip = newClip;
                sound.Play();
            }
            else if (startScene) {
                callTimer += Time.deltaTime;
            }
        }
    }
}
