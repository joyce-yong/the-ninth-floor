using UnityEngine;

namespace StarterAssets
{
    public class DoorInteract : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject interactionUI;

        [Header("Interaction Settings")]
        [SerializeField] private float interactionDistance = 3f; // Maximum interaction range
        [SerializeField] private LayerMask interactableLayer; // Layer for interactable objects

        private Transform currentTarget; // Transform of the object hit by the raycast

        [SerializeField] private Camera playerCamera; // Reference to the player's camera

        private bool hasShownUI = false; // Tracks if the UI has been shown before

        private void Update()
        {
            PerformRaycast();

            if (currentTarget != null)
            {
                // Show the UI only if it hasn't been shown before
                if (!hasShownUI)
                {
                    interactionUI.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    PerformInteraction();
                }
            }
            else
            {
                interactionUI.SetActive(false);
            }
        }

        private void PerformRaycast()
        {
            currentTarget = null;

            // Cast a ray from the camera forward
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); // Center of the screen
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
            {
                currentTarget = hit.transform; // Set the target to the object hit by the raycast
            }
        }

        private void PerformInteraction()
        {
            if (currentTarget != null)
            {
                // Check if the current target has a DoorController component
                DoorController doorController = currentTarget.GetComponent<DoorController>();

                if (doorController != null)
                {
                    // Interact with the door
                    doorController.Interact();

                    // Hide the interaction UI and prevent it from showing again
                    interactionUI.SetActive(false);
                    hasShownUI = true;
                }
            }
        }
    }
}
