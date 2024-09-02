using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cinemachineCamera.Follow = null;
        }
    }
}
