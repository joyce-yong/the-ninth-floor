using Unity.VisualScripting;
using UnityEngine;

public class SnapToPoint : MonoBehaviour
{
    public Transform targetSnapPoint; // The specific SnapPoint to snap to


    public void AlignAndSnapTo()
    {
        Align();
        SnapTo();
    }

    private void Align()
    {
        // Align the parent object to match the target SnapPoint's rotation
        Quaternion rotationOffset = Quaternion.Inverse(transform.rotation) * transform.parent.rotation;
        transform.parent.rotation = targetSnapPoint.rotation * rotationOffset;
    }

    private void SnapTo()
    {
        // Calculate the position offset between this SnapPoint and its parent
        Vector3 positionOffset = transform.position - transform.parent.position;

        // Move the parent object to align this SnapPoint with the target SnapPoint's position
        transform.parent.position = targetSnapPoint.position - positionOffset;

    }
}
