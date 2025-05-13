using UnityEngine;

public class AnomalySpawner : MonoBehaviour
{
    public GameObject[] anomalyPrefabs; // Assign in Inspector
    public Transform[] spawnPoints;     // Array of possible spawn locations
    [Range(0f, 1f)] public float spawnChance = 1f;

    private GameObject currentAnomaly;

    public void TrySpawnAnomaly()
    {
        ClearAnomaly();

        if (Random.value < spawnChance)
        {
            int prefabIndex = Random.Range(0, anomalyPrefabs.Length);
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            Transform spawnPoint = spawnPoints[spawnIndex];
            currentAnomaly = Instantiate(anomalyPrefabs[prefabIndex], spawnPoint.position, spawnPoint.rotation);
            GameManager.Instance.anomalyActive = true;
        }
    }

    public void ClearAnomaly()
    {
        if (currentAnomaly != null)
        {
            Destroy(currentAnomaly);
        }
    }
}
