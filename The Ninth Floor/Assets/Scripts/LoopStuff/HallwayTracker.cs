using UnityEngine;

public class PlayerHallwayTracker : MonoBehaviour
{
    public string currentHallway;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HallwayZone"))
        {
            currentHallway = other.gameObject.name;
            Debug.Log("Entered " + currentHallway);
        }
    }
}
