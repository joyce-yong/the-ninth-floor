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
    private HashSet<int> spawnedFloors = new();
    public Text popupText;

    private HashSet<int> collectedFloors = new();

    private List<GameObject> spawnedEvidenceObjects = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        //SpawnEvidences();
    }

    private void SpawnEvidences()
    {
        foreach (var evidence in evidences)
        {
            if (evidence.displayPrefab == null) continue;

            if (evidence.spawnPoint1 != null)
            {
                var obj1 = Instantiate(evidence.displayPrefab, evidence.spawnPoint1.position, evidence.spawnPoint1.rotation);
                spawnedEvidenceObjects.Add(obj1);
                Debug.Log($"Evidence spawned on floor {evidence.floorNumber} (Spawn 1)");
            }

            if (evidence.spawnPoint2 != null)
            {
                var obj2 = Instantiate(evidence.displayPrefab, evidence.spawnPoint2.position, evidence.spawnPoint2.rotation);
                spawnedEvidenceObjects.Add(obj2);
                Debug.Log($"Evidence spawned on floor {evidence.floorNumber} (Spawn 2)");
            }
        }
    }

    public void CollectEvidence(GameObject evidenceObject)
    {
        Destroy(evidenceObject);

        // Try to determine which floor this evidence belongs to
        foreach (var evidence in evidences)
        {
            if ((evidence.spawnPoint1 != null && Vector3.Distance(evidence.spawnPoint1.position, evidenceObject.transform.position) < 0.1f) ||
                (evidence.spawnPoint2 != null && Vector3.Distance(evidence.spawnPoint2.position, evidenceObject.transform.position) < 0.1f))
            {
                collectedFloors.Add(evidence.floorNumber);
                break;
            }
        }

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
        if (spawnedFloors.Contains(floor)) return;

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
}
