using UnityEngine;

namespace StarterAssets
{
    public class ItemInteract : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject interactionUI;

        [Header("Interaction Settings")]
        [SerializeField] private float interactionDistance = 3f; // Maximum interaction range
        [SerializeField] private LayerMask interactableLayer; // Layer for interactable objects

        private Transform currentTarget; // Transform of the object in range

        [SerializeField] private Camera playerCamera; // Reference to the player's camera


    
        private void Update()
        {
            FindClosestInteractable();
            if (currentTarget != null && IsVisible(currentTarget))
            {

                
                interactionUI.SetActive(true);
                UpdateUIPosition();

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

        private void FindClosestInteractable()
        {
            // Get all colliders in range on the interactable layer
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactionDistance, interactableLayer);
            currentTarget = null;

            foreach (Collider collider in colliderArray)
            {
                currentTarget = collider.transform;  
            }
        }

        private void UpdateUIPosition()
        {

            // Position the UI above the target and face it towards the player
            interactionUI.transform.position = currentTarget.position + Vector3.up * 0.25f;

        Vector3 cameraForward = playerCamera.transform.forward; 
        cameraForward.Normalize();

        // Make the UI element rotate to face the camera
        interactionUI.transform.rotation = Quaternion.LookRotation(cameraForward, Vector3.up);

        }


        private bool IsVisible(Transform target)
        {
            if(target == null) return false;

            Vector3 viewportPoint = playerCamera.WorldToViewportPoint(target.position);
            return viewportPoint.z > 0 &&
                   viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                   viewportPoint.y >= 0 && viewportPoint.y <= 1;
        }

        private void PerformInteraction()
        {
            if (currentTarget != null)
            {
                // Check if the current target has an ItemInspector component
                ItemInspector inspector = currentTarget.GetComponent<ItemInspector>();

                if (inspector != null)
                {
                    interactionUI.SetActive(false);

                    // Start inspection
                    inspector.Interact();
                }
            }
        }
      
    }
}
