using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EvidenceManager : MonoBehaviour
{
    [System.Serializable]
    public class EvidenceInfo
    {
        public int floorNumber;
        public GameObject displayPrefab;
        public Transform spawnPoint1;
        public Transform spawnPoint2;
    }

    public static EvidenceManager Instance;

    public EvidenceInfo[] evidences;
    public CanvasGroup evidencePopupUI;
    public Text popupText;

    private HashSet<int> spawnedFloors = new();
    private HashSet<int> collectedFloors = new();

    private List<GameObject> spawnedEvidenceObjects = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CollectEvidence(GameObject evidenceObject)
    {
        EvidenceInfo matchedEvidence = null;

        // Match collected object to its spawn location
        foreach (var evidence in evidences)
        {
            if ((evidence.spawnPoint1 != null && Vector3.Distance(evidence.spawnPoint1.position, evidenceObject.transform.position) < 1f) ||
                (evidence.spawnPoint2 != null && Vector3.Distance(evidence.spawnPoint2.position, evidenceObject.transform.position) < 1f))
            {
                matchedEvidence = evidence;
                break;
            }
        }

        if (matchedEvidence == null)
        {
            Debug.LogWarning("Collected evidence doesn't match any floor.");
            return;
        }

        int floor = matchedEvidence.floorNumber;

        // Add floor to collected set (only once)
        if (!collectedFloors.Contains(floor))
        {
            collectedFloors.Add(floor);
        }

        // Destroy all evidence objects related to this floor
        for (int i = spawnedEvidenceObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = spawnedEvidenceObjects[i];
            if ((matchedEvidence.spawnPoint1 != null && Vector3.Distance(obj.transform.position, matchedEvidence.spawnPoint1.position) < 0.1f) ||
                (matchedEvidence.spawnPoint2 != null && Vector3.Distance(obj.transform.position, matchedEvidence.spawnPoint2.position) < 0.1f))
            {
                Destroy(obj);
                spawnedEvidenceObjects.RemoveAt(i);
            }
        }

        Destroy(evidenceObject); // destroy clicked evidence

        ShowPopup($"{collectedFloors.Count} / {evidences.Length} evidences collected.");
        Debug.Log($"Collected evidence: {collectedFloors.Count} / {evidences.Length}");
    }

    public void TrySpawnEvidenceForFloor(int floor)
    {
        if (spawnedFloors.Contains(floor) || collectedFloors.Contains(floor))
            return;

        foreach (var evidence in evidences)
        {
            if (evidence.floorNumber == floor && evidence.displayPrefab != null)
            {
                if (evidence.spawnPoint1 != null)
                {
                    var obj1 = Instantiate(evidence.displayPrefab, evidence.spawnPoint1.position, evidence.spawnPoint1.rotation);
                    spawnedEvidenceObjects.Add(obj1);
                    Debug.Log($"Evidence spawned on floor {floor} (Spawn 1)");
                }

                if (evidence.spawnPoint2 != null)
                {
                    var obj2 = Instantiate(evidence.displayPrefab, evidence.spawnPoint2.position, evidence.spawnPoint2.rotation);
                    spawnedEvidenceObjects.Add(obj2);
                    Debug.Log($"Evidence spawned on floor {floor} (Spawn 2)");
                }

                spawnedFloors.Add(floor);
                break;
            }
        }
    }

    private void ShowPopup(string message)
    {
        if (popupText != null)
        {
            popupText.text = message;
            StopAllCoroutines();
            StartCoroutine(FadePopup());
        }
    }

    private IEnumerator FadePopup()
    {
        evidencePopupUI.alpha = 1;
        yield return new WaitForSeconds(2f);
        while (evidencePopupUI.alpha > 0)
        {
            evidencePopupUI.alpha -= Time.deltaTime;
            yield return null;
        }
    }
}
