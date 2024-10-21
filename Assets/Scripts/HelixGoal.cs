using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixGoal : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        GameManager.instance.NextLevel();
    }
}
