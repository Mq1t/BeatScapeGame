using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pianoTilePrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float initialDelay = 0f;
    [Tooltip("How far above the top of the camera to spawn (world units)")]
    [SerializeField] private float spawnOffsetY = 0.5f;

    [Header("Discrete spawn slots (optional)")]
    [Tooltip("If you assign one or more Transforms here the spawner will pick a random one each time.")]
    [SerializeField] private Transform[] spawnSlotTransforms;

    [Header("Auto grid (used when no spawn slots are assigned)")]
    [Tooltip("Number of columns across the top to choose from when no spawn slots are assigned.")]
    [SerializeField] private int columns = 5;

    private List<Vector3> spawnPositions = new List<Vector3>();

    private void Start()
    {
        if (pianoTilePrefab == null)
        {
            Debug.LogError("TileSpawner: pianoTilePrefab is not assigned.");
            enabled = false;
            return;
        }

        BuildSpawnPositions();
        InvokeRepeating(nameof(SpawnTile), initialDelay, spawnInterval);
    }

    private void BuildSpawnPositions()
    {
        spawnPositions.Clear();

        // If explicit slot transforms provided, use their world positions
        if (spawnSlotTransforms != null && spawnSlotTransforms.Length > 0)
        {
            foreach (var t in spawnSlotTransforms)
            {
                if (t != null)
                    spawnPositions.Add(t.position);
            }

            if (spawnPositions.Count > 0)
                return; // done
        }

        // Otherwise compute evenly-spaced columns across the camera top
        Camera cam = Camera.main;
        if (cam == null)
        {
            // fallback: use spawner position spread
            for (int i = 0; i < Mathf.Max(1, columns); i++)
            {
                float x = transform.position.x + (i - (columns - 1) * 0.5f) * 1.0f;
                float y = transform.position.y + spawnOffsetY;
                spawnPositions.Add(new Vector3(x, y, transform.position.z));
            }
            return;
        }

        Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0f, 1f, Mathf.Abs(cam.transform.position.z)));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(cam.transform.position.z)));
        float yPos = topLeft.y + spawnOffsetY;

        int colCount = Mathf.Max(1, columns);
        for (int i = 0; i < colCount; i++)
        {
            // place at center of each column: use (i + 0.5) / columns
            float t = (i + 0.5f) / colCount;
            float x = Mathf.Lerp(topLeft.x, topRight.x, t);
            spawnPositions.Add(new Vector3(x, yPos, 0f));
        }
    }

    private void SpawnTile()
    {
        if (spawnPositions.Count == 0)
            BuildSpawnPositions();

        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("TileSpawner: no spawn positions available.");
            return;
        }

        Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count)];
        Instantiate(pianoTilePrefab, spawnPos, Quaternion.identity);
    }

    public void StopSpawning() => CancelInvoke(nameof(SpawnTile));
}