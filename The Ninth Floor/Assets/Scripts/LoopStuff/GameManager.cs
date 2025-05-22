using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int currentFloor = 9;
    public bool anomalyActive = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnForwardTrigger()
    {
        if (anomalyActive)
        {
            Debug.Log("Have anomaly.");
            currentFloor = 9;
        }
        else
        {
            currentFloor--;
        }

        Debug.Log("Now on floor: " + currentFloor);
        anomalyActive = false;

        FindFirstObjectByType<FloorDisplay>().UpdateFloorDisplay(); // Refresh the display
        EvidenceManager.Instance.TrySpawnEvidenceForFloor(currentFloor);

    }


    public void OnBackwardTrigger()
    {
        currentFloor--;
        Debug.Log("Player walked back. Now on floor: " + currentFloor);

        FindFirstObjectByType<FloorDisplay>().UpdateFloorDisplay(); // Refresh the display
        EvidenceManager.Instance.TrySpawnEvidenceForFloor(currentFloor);

    }

}