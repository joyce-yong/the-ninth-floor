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
    private List<Vector3> collectedEvidencePositions = new(); // Use position instead of GameObject

    private List<GameObject> spawnedEvidenceObjects = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CollectEvidence(GameObject evidenceObject)
    {
        Vector3 collectedPos = evidenceObject.transform.position;
        collectedEvidencePositions.Add(collectedPos);

        // Determine the floor this evidence belongs to and mark as collected
        foreach (var evidence in evidences)
        {
            if ((evidence.spawnPoint1 != null && Vector3.Distance(evidence.spawnPoint1.position, collectedPos) < 1.0f) ||
                (evidence.spawnPoint2 != null && Vector3.Distance(evidence.spawnPoint2.position, collectedPos) < 1.0f))
            {
                collectedFloors.Add(evidence.floorNumber);
                break;
            }
        }

        Destroy(evidenceObject);

        ShowPopup($"{collectedFloors.Count} / {evidences.Length} evidences collected.");
        Debug.Log($"Collected evidence: {collectedFloors.Count} / {evidences.Length}");
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

    public void TrySpawnEvidenceForFloor(int floor)
    {
        if (spawnedFloors.Contains(floor) || collectedFloors.Contains(floor)) return;

        foreach (var evidence in evidences)
        {
            if (evidence.floorNumber == floor && evidence.displayPrefab != null)
            {
                if (evidence.spawnPoint1 != null && !IsEvidenceAlreadyCollected(evidence.spawnPoint1.position))
                {
                    var obj1 = Instantiate(evidence.displayPrefab, evidence.spawnPoint1.position, evidence.spawnPoint1.rotation);
                    spawnedEvidenceObjects.Add(obj1);
                    Debug.Log($"Evidence spawned on floor {floor} (Spawn 1)");
                }

                if (evidence.spawnPoint2 != null && !IsEvidenceAlreadyCollected(evidence.spawnPoint2.position))
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

    private bool IsEvidenceAlreadyCollected(Vector3 spawnPos)
    {
        foreach (var pos in collectedEvidencePositions)
        {
            if (Vector3.Distance(pos, spawnPos) < 0.1f)
                return true;
        }
        return false;
    }
}
