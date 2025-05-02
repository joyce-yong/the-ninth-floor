using UnityEngine;
using UnityEngine.Events;

public class HallwayManager : MonoBehaviour
{
    public GameObject HallwayPrefab; // The hallway prefab to instantiate

    private Transform TopMostParent;
    private GameObject NewHallway;

    private bool doorwayMoved = false; // Track if the doorway has already been moved

    public void AddNewHallway()
    {
        // Instantiate a new hallway prefab
        NewHallway = Instantiate(HallwayPrefab);

        // Get SnapPointRed from the new hallway
        Transform newHallwaySnapPointRed = GetSnapPointFromObject(NewHallway, "SnapPointRed");

        // Get the topmost parent and its SnapPointBlue
        TopMostParent = GetTopmostParent(transform);
        Transform currentHallwaySnapPointBlue = GetSnapPointFromObject(TopMostParent.gameObject, "SnapPointBlue");

        // Add SnapToPoint and align
        AlignAndSnapHallway(newHallwaySnapPointRed, currentHallwaySnapPointBlue);



    }

    public void DestroyOldHallway()
    {
        // Ensure the doorway has been moved before destroying the hallway
        MoveDoorwayToNewHallway();


        Destroy(TopMostParent.gameObject);

    }

    private Transform GetSnapPointFromObject(GameObject obj, string snapPointName)
    {
        return obj.transform.Find(snapPointName);
    }

    private Transform GetTopmostParent(Transform currentTransform)
    {
        Transform topmostParent = currentTransform;
        while (topmostParent.parent != null)
        {
            topmostParent = topmostParent.parent;
        }
        return topmostParent;
    }

    private void AlignAndSnapHallway(Transform newHallwaySnapPointRed, Transform currentHallwaySnapPointBlue)
    {
        // Add the SnapToPoint component dynamically to newHallwaySnapPointRed
        SnapToPoint snapToPoint = newHallwaySnapPointRed.gameObject.AddComponent<SnapToPoint>();

        // Set the targetSnapPoint to the currentHallwaySnapPointBlue
        snapToPoint.targetSnapPoint = currentHallwaySnapPointBlue;

        // Align and snap the new hallway
        snapToPoint.AlignAndSnapTo();
    }

    private void MoveDoorwayToNewHallway()
    {
        if (doorwayMoved)
        {
            Debug.Log("Doorway has already been moved. Skipping.");
            return;
        }

        if (TopMostParent == null || NewHallway == null)
        {
            Debug.LogError("TopMostParent or NewHallway is not set! Ensure AddNewHallway was called first.");
            return;
        }

        // Find the "DoorwayAndFrameLast" object in the current hallway
        Transform doorwayAndFrameLast = TopMostParent.Find("DoorwayAndFrameLast");

        if (doorwayAndFrameLast != null)
        {
            Debug.Log("Moving DoorwayAndFrameLast to new hallway.");

            // Rename the object for clarity (optional)
            doorwayAndFrameLast.gameObject.name = "DoorwayAndFrameMoved";

            // Move it directly to the new hallway
            doorwayAndFrameLast.SetParent(NewHallway.transform, true); // Maintain world position

            doorwayMoved = true; // Prevent further processing
        }
        else
        {
            Debug.LogError("DoorwayAndFrameLast not found in TopMostParent!");
        }
    }

   
}
