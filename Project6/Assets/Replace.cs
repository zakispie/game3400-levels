using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replace : MonoBehaviour
{
    [SerializeField] GameObject replacement;

    public void replace() {
        replacement.transform.position = transform.position;
        replacement.SetActive(true);
        Destroy(gameObject);
    }
}
