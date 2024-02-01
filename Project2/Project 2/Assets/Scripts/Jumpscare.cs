using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] GameObject TVGhost;
    [SerializeField] GameObject ChairGhost;
    [SerializeField] GameObject BookGhost;
    [SerializeField] GameObject ToyGhost;
    [SerializeField] GameObject PaintGhost;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "TVGhost")
        {
            TVGhost.SetActive(true);
            TVGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            //insert scream sound below

        }
        if (other.gameObject.tag == "ChairGhost")
        {
            ChairGhost.SetActive(true);
            ChairGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            //insert scream sound here below

        }
        if (other.gameObject.tag == "BookGhost")
        {
            BookGhost.SetActive(true);
            BookGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            //insert scream sound here below

        }
        if (other.gameObject.tag == "ToyGhost")
        {
            ToyGhost.SetActive(true);
            ToyGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            //insert scream sound here below

        }
        if (other.gameObject.tag == "PaintGhost")
        {
            PaintGhost.SetActive(true);
            PaintGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            //insert scream sound here below

        }
    }
}
