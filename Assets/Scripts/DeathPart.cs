using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (!player.isSuperSpeedActive) GameManager.instance.RestartLevel();
        }
    }
}
