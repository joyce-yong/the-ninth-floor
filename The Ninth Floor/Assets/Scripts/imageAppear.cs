using UnityEngine;
using UnityEngine.UI;

public class imageAppear : MonoBehaviour
{
    public bool interactable = false;
    public bool toggle = false;

    public GameObject inttext;
    public GameObject closetext;
    public GameObject imagePanel;

    public GameObject player;
    private MonoBehaviour movementScript;

    void Start()
    {
        movementScript = player.GetComponent<SC_FPSController>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interactable = true;

            if (!toggle)
            {
                inttext.SetActive(true);
                closetext.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            interactable = false;
            inttext.SetActive(false);
            closetext.SetActive(false);
        }
    }

    void Update()
    {
        if ((interactable || toggle) && Input.GetKeyDown(KeyCode.E))
        {
            toggle = !toggle;

            imagePanel.SetActive(toggle);
            inttext.SetActive(!toggle && interactable);
            closetext.SetActive(toggle);

            // Enable/Disable movement
            if (movementScript != null)
            {
                movementScript.enabled = !toggle;
            }
        }
    }
}
