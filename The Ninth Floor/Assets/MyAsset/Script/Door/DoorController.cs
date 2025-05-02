using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator doorAnimator;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioClip doorSwingSound;
    [Range(0, 1)] public float doorAudioVolume = 0.5f;

    private bool isOpen = false;
    private bool isLocked = false; // Lock state of the door

    // Expose isLocked as a public property
    public bool IsLocked
    {
        get => isLocked;
        set => isLocked = value;
    }

    public void ToggleDoor()
    {
        if (isLocked)
        {
            Debug.LogWarning("Door is locked and cannot be toggled.");
            return;
        }

        isOpen = !isOpen;

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("IsOpen", isOpen);
        }
        else
        {
            Debug.LogError("Animator is not assigned.");
        }
    }

    //bug fix here pls with 'isOpen'
    public void InteractWithAutomaticDoor()
    {
        if (isOpen)
        {
            Debug.Log("exit triggered");
            ToggleDoor();
            IsLocked = true; // Lock the door when the trigger is entered
        }
        else {
            Debug.Log("Enter triggered");

            IsLocked = false; 
        }
    }

    public void PlayDoorSound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, doorAudioVolume);
        }
    }

    // Methods for animation events
    public void PlayOpenSound() => PlayDoorSound(doorOpenSound);
    public void PlayCloseSound() => PlayDoorSound(doorCloseSound);
    public void PlaySwingSound() => PlayDoorSound(doorSwingSound);

    public void Interact()
    {
        if (isLocked)
        {
            Debug.LogWarning("Door is locked and cannot be interacted with.");
            return;
        }

        ToggleDoor();
    }
}
