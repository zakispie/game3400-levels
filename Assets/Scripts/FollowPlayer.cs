using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform PlayerPos;
    void Update()
    {
        transform.position = PlayerPos.position;
    }
}
