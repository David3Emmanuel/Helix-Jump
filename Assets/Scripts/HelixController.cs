using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    public float turnSpeed = 1.0f;
    public GameObject helixPlatformPrefab;
    public Transform topTransform;
    public Transform goalTransform;
    public List<Stage> allStages = new List<Stage>();

    private Vector2 lastTapPosition;
    private Vector3 startRotation;
    private float helixDistance;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(GameManager.instance.currentStage);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;

            if (lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
            }

            float horizontalSwipe = currentTapPosition.x - lastTapPosition.x;
            lastTapPosition = currentTapPosition;

            transform.Rotate(horizontalSwipe * turnSpeed * Vector3.down);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }
    }

    public void Reset()
    {
        transform.localEulerAngles = startRotation;
    }

    public void LoadStage(int stageIndex)
    {
        Stage stage = allStages[Mathf.Clamp(stageIndex, 0, allStages.Count - 1)];
        if (stage == null)
        {
            Debug.LogError("Stage " + startRotation + " is null");
            return;
        }

        // Reset the stage
        Camera.main.backgroundColor = stage.backgroundColor;
        PlayerController player = FindObjectOfType<PlayerController>();
        HelixController helix = FindObjectOfType<HelixController>();
        player.ChangeColor(stage.playerColor);
        player.Reset();
        helix.Reset();

        foreach (GameObject platform in spawnedPlatforms)
        {
            Destroy(platform);
        }
        spawnedPlatforms.Clear();

        // Populate the stage
        float platformDistance = helixDistance / stage.platforms.Count;
        float spawnY = topTransform.localPosition.y;

        for (int i = 0; i < stage.platforms.Count; i++)
        {
            Platform platform = stage.platforms[i];
            spawnY -= platformDistance;
            GameObject platformObject = Instantiate(helixPlatformPrefab, transform);
            platformObject.transform.localPosition = new Vector3(0, spawnY, 0);
            spawnedPlatforms.Add(platformObject);

            int partsToDisable = 12 - platform.partCount;
            List<GameObject> disabledParts = new List<GameObject>();
            while (disabledParts.Count < partsToDisable)
            {
                int randomIndex = Random.Range(0, platformObject.transform.childCount);
                GameObject disabledPart = platformObject.transform.GetChild(randomIndex).gameObject;
                if (!disabledParts.Contains(disabledPart))
                {
                    disabledParts.Add(disabledPart);
                    disabledPart.SetActive(false);
                }
            }

            List<GameObject> remainingParts = new List<GameObject>();
            foreach (Transform child in platformObject.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    remainingParts.Add(child.gameObject);
                    child.GetComponent<Renderer>().material.color = stage.platformColor;
                }
            }

            List<GameObject> deathParts = new List<GameObject>();
            while (deathParts.Count < platform.deathPartCount)
            {
                int randomIndex = Random.Range(0, remainingParts.Count);
                GameObject deathPart = remainingParts[randomIndex];
                if (!deathParts.Contains(deathPart))
                {
                    deathParts.Add(deathPart);
                    deathPart.AddComponent<DeathPart>();
                }
            }
        }
    }
}
