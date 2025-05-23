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
    private List<Vector3> collectedEvidencePositions = new(); // Use position instead of GameObject

    private List<GameObject> spawnedEvidenceObjects = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
<<<<<<< HEAD
        Vector3 collectedPos = evidenceObject.transform.position;
        collectedEvidencePositions.Add(collectedPos);

        // Determine the floor this evidence belongs to and mark as collected
        foreach (var evidence in evidences)
        {
            if ((evidence.spawnPoint1 != null && Vector3.Distance(evidence.spawnPoint1.position, collectedPos) < 1.0f) ||
                (evidence.spawnPoint2 != null && Vector3.Distance(evidence.spawnPoint2.position, collectedPos) < 1.0f))
            {
=======
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
>>>>>>> parent of 636adaa9 (fixed some evidence spawning bugs, but pop up not fixed yet)
                collectedFloors.Add(evidence.floorNumber);
                break;
            }
        }

<<<<<<< HEAD
        Destroy(evidenceObject);

=======
>>>>>>> parent of 636adaa9 (fixed some evidence spawning bugs, but pop up not fixed yet)
        ShowPopup($"{collectedFloors.Count} / {evidences.Length} evidences collected.");
        Debug.Log($"Collected evidence: {collectedFloors.Count} / {evidences.Length}");
    }

<<<<<<< HEAD
=======

>>>>>>> parent of 636adaa9 (fixed some evidence spawning bugs, but pop up not fixed yet)
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
<<<<<<< HEAD
        if (spawnedFloors.Contains(floor) || collectedFloors.Contains(floor)) return;
=======
        if (spawnedFloors.Contains(floor)) return;
>>>>>>> parent of 636adaa9 (fixed some evidence spawning bugs, but pop up not fixed yet)

        foreach (var evidence in evidences)
        {
            if (evidence.floorNumber == floor && evidence.displayPrefab != null)
            {
<<<<<<< HEAD
                if (evidence.spawnPoint1 != null && !IsEvidenceAlreadyCollected(evidence.spawnPoint1.position))
=======
                if (evidence.spawnPoint1 != null)
>>>>>>> parent of 636adaa9 (fixed some evidence spawning bugs, but pop up not fixed yet)
                {
                    var obj1 = Instantiate(evidence.displayPrefab, evidence.spawnPoint1.position, evidence.spawnPoint1.rotation);
                    spawnedEvidenceObjects.Add(obj1);
                    Debug.Log($"Evidence spawned on floor {floor} (Spawn 1)");
                }

<<<<<<< HEAD
                if (evidence.spawnPoint2 != null && !IsEvidenceAlreadyCollected(evidence.spawnPoint2.position))
=======
                if (evidence.spawnPoint2 != null)
>>>>>>> parent of 636adaa9 (fixed some evidence spawning bugs, but pop up not fixed yet)
                {
                    var obj2 = Instantiate(evidence.displayPrefab, evidence.spawnPoint2.position, evidence.spawnPoint2.rotation);
                    spawnedEvidenceObjects.Add(obj2);
                    Debug.Log($"Evidence spawned on floor {floor} (Spawn 2)");
                }

<<<<<<< HEAD
=======

>>>>>>> parent of 636adaa9 (fixed some evidence spawning bugs, but pop up not fixed yet)
                spawnedFloors.Add(floor);
                break;
            }
        }
    }
<<<<<<< HEAD

    private bool IsEvidenceAlreadyCollected(Vector3 spawnPos)
    {
        foreach (var pos in collectedEvidencePositions)
        {
            if (Vector3.Distance(pos, spawnPos) < 0.1f)
                return true;
        }
        return false;
    }
=======
>>>>>>> parent of 636adaa9 (fixed some evidence spawning bugs, but pop up not fixed yet)
}
