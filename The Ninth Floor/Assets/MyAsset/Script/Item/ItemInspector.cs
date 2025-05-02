using System.Collections;
using UnityEngine;

namespace StarterAssets
{
    public class ItemInspector : MonoBehaviour
    {
        [Header("Inspection Settings")]
        [SerializeField] private float inspectionDistance = 0.6f; // Default distance from the camera during inspection
        [SerializeField] private float rotationSpeed = 150f;      // Speed of item rotation
        [SerializeField] private float zoomSpeed = 2f;            // Speed of zooming
        [SerializeField] private float minZoom = 0.4f;            // Minimum zoom distance
        [SerializeField] private float maxZoom = 0.8f;            // Maximum zoom distance
        [SerializeField] private float zoomSmoothing = 5f;        // Smoothing factor for zoom transitions

        [Header("UI Elements")]
        [SerializeField] private GameObject hud;                  // Reference to the HUD GameObject

        private bool isInspecting = false;                        // Whether the item is being inspected
        private Vector3 originalPosition;                         // Original position of the item
        private Quaternion originalRotation;                      // Original rotation of the item
        private Transform originalParent;                         // Original parent of the item
        private Camera playerCamera;                              // Reference to the player's camera
        private float targetInspectionDistance;                   // Target distance for smooth zooming
        private FirstPersonController playerController;           // Reference to the player controller

        private void Start()
        {
            targetInspectionDistance = inspectionDistance;

            // Retrieve the main camera and player controller
            if (Camera.main != null)
            {
                playerCamera = Camera.main;
                playerController = playerCamera.GetComponentInParent<FirstPersonController>();
            }
            else
            {
                Debug.LogError("No Main Camera found. Assign a camera with the 'MainCamera' tag.");
            }
        }

        private void Update()
        {
            if (isInspecting)
            {
                HandleItemRotation();
                HandleZoom();
              
            }
        }

        public void Interact()
        {
            if (!isInspecting)
            {
                StartInspection();
            }
            else
            {
                EndInspection();
            }
        }

        private void StartInspection()
        {
            // Disable player movement and interactions
            // playerController.DisableAnimationsAndSounds();
            playerController.enabled = false;

            isInspecting = true;

            // Show the HUD if assigned
            hud.SetActive(true);

            // Save the original position and rotation
            originalPosition = transform.localPosition;
            originalRotation = transform.rotation;

            // Re-parent the item to the camera for inspection
            transform.SetParent(playerCamera.transform);

            StartCoroutine(SmoothMoveToTarget());
        }

        private IEnumerator SmoothMoveToTarget()
        {
            float duration = 0.25f; // Duration of the movement in seconds
            float elapsedTime = 0f;

            Vector3 startingLocalPosition = transform.localPosition;
            Vector3 targetLocalPosition =  Vector3.forward * targetInspectionDistance;

            Vector3 cameraForward = playerCamera.transform.forward;
            Quaternion faceCameraRotation = Quaternion.LookRotation(-cameraForward, Vector3.up);

            
            Quaternion startingLocalRotation = transform.rotation;
            Quaternion targetLocationRotation = faceCameraRotation * Quaternion.Euler(0, 90, 90);

            while (elapsedTime < duration)
            {
                // Interpolate the position based on time
                transform.localPosition = Vector3.Lerp(startingLocalPosition, targetLocalPosition, elapsedTime / duration);
                transform.rotation = Quaternion.Slerp(startingLocalRotation, targetLocationRotation, elapsedTime / duration);

                // Increment elapsed time
                elapsedTime += Time.deltaTime;

                // Wait for the next frame
                yield return null;
            }
        }

        private void EndInspection()
        {
            // Re-enable player movement and interactions
            // playerController.EnableAnimationsAndSounds();
            playerController.enabled = true;

            isInspecting = false;

            // Hide the HUD if assigned
            hud.SetActive(false);

            // Start the smooth transition back to the original position
            StartCoroutine(LerpToOriginalPosition());
        }
       
        private IEnumerator LerpToOriginalPosition()
        {
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;

            Vector3 targetPosition = originalPosition;
            Quaternion targetRotation = originalRotation;

            float duration = 0.25f; // Duration of the movement in seconds
            float elapsedTime = 0f;


            while (elapsedTime < duration)
            {
                // Smoothly interpolate position and rotation
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure final position and rotation are set
            transform.position = targetPosition;
            transform.rotation = targetRotation;

            // Restore the item's parent
            transform.SetParent(originalParent);
        }


        // needs to be fixed
        private void HandleItemRotation()
        {
            if (Input.GetMouseButton(0)) // Left mouse button
            {
                // Get mouse input
                float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

                // Create rotation quaternions for both axes
                Quaternion rotationHorizontal = Quaternion.AngleAxis(-rotationX, Vector3.up); // Horizontal rotation
                Quaternion rotationVertical = Quaternion.AngleAxis(rotationY, Vector3.right); // Vertical rotation

                // Apply the rotation in the object's local space
                transform.localRotation = rotationHorizontal * transform.localRotation;
                transform.localRotation = rotationVertical * transform.localRotation;
            }
        }


        private void HandleZoom()
        {
            // Detect mouse scroll input (positive for zoom in, negative for zoom out)
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Adjust target inspection distance based on zoom speed
            targetInspectionDistance -= scrollInput * zoomSpeed * Time.deltaTime;

            // Clamp the target distance between minZoom and maxZoom
            targetInspectionDistance = Mathf.Clamp(targetInspectionDistance, minZoom, maxZoom);

            UpdateZoomPosition();
        }
     
        private void UpdateZoomPosition()
        {
            // Smoothly interpolate the item's local position to match the target inspection distance
            Vector3 targetPosition = Vector3.forward * targetInspectionDistance;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * zoomSmoothing);
        }
    }
}
