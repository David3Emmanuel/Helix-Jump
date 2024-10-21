using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float bounce = 5.0f;

    private bool allowNextCollision;
    private Vector3 startPosition;
    public int perfectPasses;
    public bool isSuperSpeedActive;

    void Awake()
    {
        startPosition = transform.position;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        allowNextCollision = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!allowNextCollision) return;

        if (isSuperSpeedActive)
        {
            if (!other.transform.GetComponent<HelixGoal>())
            {
                Destroy(other.transform.parent.gameObject);
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * bounce, ForceMode.Impulse);
        }

        StartCoroutine(AllowCollision());

        perfectPasses = 0;
        isSuperSpeedActive = false;
    }

    void Update()
    {
        if (perfectPasses >= 2 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    IEnumerator AllowCollision()
    {
        allowNextCollision = false;
        yield return new WaitForSeconds(0.2f);
        allowNextCollision = true;
    }

    public void Reset()
    {
        transform.position = startPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        isSuperSpeedActive = false;
    }

    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
