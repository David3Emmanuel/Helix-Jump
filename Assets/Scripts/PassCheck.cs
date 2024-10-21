using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{
    private bool isPassed = false;

    void OnTriggerEnter(Collider other)
    {
        if (!isPassed)
        {
            GameManager.instance.AddScore(1);
            PlayerController player = other.GetComponent<PlayerController>();
            player.perfectPasses++;
        }
        isPassed = true;
    }

    public void Reset()
    {
        isPassed = false;
    }
}
