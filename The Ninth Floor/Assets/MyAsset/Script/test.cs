using UnityEngine;

public class CubeSnapper : MonoBehaviour
{
    public Transform otherCube; // Reference to the other cube
    public float snapDistance = 0.1f; // Threshold distance for snapping

    private enum Face { Front, Back, Left, Right, Top, Bottom }

    private void Start() => SnapToFace(Face.Back, Face.Back); // Snap front to back on start

    private void SnapToFace(Face thisFace, Face otherFace)
    {
        // Get the face normal and center for both cubes
        (Vector3 thisFaceCenter, Vector3 thisFaceNormal) = GetFaceInfo(thisFace, transform);
        (Vector3 otherFaceCenter, Vector3 otherFaceNormal) = GetFaceInfo(otherFace, otherCube);

        // Calculate offset and adjust for cube dimensions
        Vector3 offset = otherFaceCenter - thisFaceCenter + (otherFaceNormal * otherCube.localScale.z / 2) + (thisFaceNormal * transform.localScale.z / 2);

        // Snap the position
        transform.position += offset;

        // Align rotation
        transform.rotation = Quaternion.LookRotation(-otherFaceNormal, otherCube.up) * Quaternion.Inverse(Quaternion.LookRotation(thisFaceNormal, transform.up)) * transform.rotation;
    }

    private (Vector3 center, Vector3 normal) GetFaceInfo(Face face, Transform cube)
    {
        Vector3 center = cube.position;
        Vector3 size = cube.localScale / 2;

        Vector3 normal = face switch
        {
            Face.Front => cube.forward,
            Face.Back => -cube.forward,
            Face.Left => -cube.right,
            Face.Right => cube.right,
            Face.Top => cube.up,
            Face.Bottom => -cube.up,
            _ => cube.forward
        };

        Vector3 offset = face switch
        {
            Face.Front => cube.forward * size.z,
            Face.Back => -cube.forward * size.z,
            Face.Left => -cube.right * size.x,
            Face.Right => cube.right * size.x,
            Face.Top => cube.up * size.y,
            Face.Bottom => -cube.up * size.y,
            _ => Vector3.zero
        };

        return (center + offset, normal);
    }
}
