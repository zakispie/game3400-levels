using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] AudioClip ScreamSFX;
    [SerializeField] GameObject TVGhost;
    [SerializeField] GameObject ChairGhost;
    [SerializeField] GameObject BookGhost;
    [SerializeField] GameObject ToyGhost;
    [SerializeField] GameObject PaintGhost;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("TVGhost"))
        {
            TVGhost.SetActive(true);
            TVGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            AudioSource.PlayClipAtPoint(ScreamSFX, PlayerController.Position);

        }
        if (other.gameObject.CompareTag("ChairGhost"))
        {
            ChairGhost.SetActive(true);
            ChairGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            AudioSource.PlayClipAtPoint(ScreamSFX, PlayerController.Position);

        }
        if (other.gameObject.CompareTag("BookGhost"))
        {
            BookGhost.SetActive(true);
            BookGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            AudioSource.PlayClipAtPoint(ScreamSFX, PlayerController.Position);

        }
        if (other.gameObject.CompareTag("ToyGhost"))
        {
            ToyGhost.SetActive(true);
            ToyGhost.gameObject.GetComponent<Animator>().Play("jumpscare");

            AudioSource.PlayClipAtPoint(ScreamSFX, PlayerController.Position);

        }
        if (other.gameObject.CompareTag("PaintGhost"))
        {
            PaintGhost.SetActive(true);
            PaintGhost.gameObject.GetComponent<Animator>().Play("jumpscare");
            
            AudioSource.PlayClipAtPoint(ScreamSFX, PlayerController.Position);
        }
    }
}
