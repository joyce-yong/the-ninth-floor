using UnityEngine;
using UnityEngine.Events;

public class DynamicTrigger : MonoBehaviour
{
    [Tooltip("Methods to invoke when the trigger is activated.")]
    public UnityEvent onTriggerEnter;

    // private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        //if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            // Invoke all assigned methods
            onTriggerEnter.Invoke();
            // hasTriggered = true;
        }
    }
}
