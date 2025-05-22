using UnityEngine;

public class SpawnAnomaly : MonoBehaviour
{
    [System.Serializable]
    public class Anomaly
    {
        public GameObject prefab;
        public Transform spawnPointA;
        public Transform spawnPointB;
    }

    public Anomaly[] anomalies; // This will show up properly in the inspector
    [Range(0f, 1f)] public float spawnChance = 1f;

    private readonly System.Collections.Generic.List<GameObject> activeAnomalies = new();

    public void TrySpawnAnomalies()
    {
        ClearAnomalies();

        foreach (var anomaly in anomalies)
        {
            if (Random.value < spawnChance)
            {
                if (anomaly.prefab != null && anomaly.spawnPointA != null)
                {
                    var instanceA = Instantiate(anomaly.prefab, anomaly.spawnPointA.position, anomaly.spawnPointA.rotation);
                    activeAnomalies.Add(instanceA);
                }

                if (anomaly.prefab != null && anomaly.spawnPointB != null)
                {
                    var instanceB = Instantiate(anomaly.prefab, anomaly.spawnPointB.position, anomaly.spawnPointB.rotation);
                    activeAnomalies.Add(instanceB);
                }

                GameManager.Instance.anomalyActive = true;
            }
        }
    }

    public void ClearAnomalies()
    {
        foreach (var anomaly in activeAnomalies)
        {
            if (anomaly != null)
            {
                Destroy(anomaly);
            }
        }

        activeAnomalies.Clear();
    }
}
