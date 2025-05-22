using System.Collections.Generic;
using UnityEngine;

public class FloorDisplay : MonoBehaviour
{
    [System.Serializable]
    public class FloorIndicator
    {
        public int floorNumber;
        public GameObject displayPrefab;
        public Transform spawnPoint1;
        public Transform spawnPoint2;
    }

    public FloorIndicator[] floorIndicators;

    private List<GameObject> activeDisplays = new();

    private void Start()
    {
        UpdateFloorDisplay();
    }

    public void UpdateFloorDisplay()
    {
        // Clear old floor display objects
        foreach (var obj in activeDisplays)
        {
            if (obj != null)
                Destroy(obj);
        }
        activeDisplays.Clear();

        int current = GameManager.Instance.currentFloor;

        foreach (var indicator in floorIndicators)
        {
            if (indicator.floorNumber == current && indicator.displayPrefab != null)
            {
                if (indicator.spawnPoint1 != null)
                {
                    GameObject obj1 = Instantiate(indicator.displayPrefab, indicator.spawnPoint1.position, indicator.spawnPoint1.rotation);
                    activeDisplays.Add(obj1);
                }

                if (indicator.spawnPoint2 != null)
                {
                    GameObject obj2 = Instantiate(indicator.displayPrefab, indicator.spawnPoint2.position, indicator.spawnPoint2.rotation);
                    activeDisplays.Add(obj2);
                }

                break; // Only one floor should match
            }
        }
    }

    public (Transform, Transform) GetCurrentSpawnPoints()
    {
        int current = GameManager.Instance.currentFloor;

        foreach (var indicator in floorIndicators)
        {
            if (indicator.floorNumber == current)
                return (indicator.spawnPoint1, indicator.spawnPoint2);
        }

        return (null, null);
    }
}
