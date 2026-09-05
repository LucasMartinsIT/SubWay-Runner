using UnityEngine;

public class EnviromentSpawner : MonoBehaviour
{
    [Tooltip("The prefab of your entire enviroment segment")]
    public GameObject enviromentPrefab;

    private bool hasSpawned = false;

    void OnTriggerEnter(Collider col)
    {
        if (hasSpawned) return;
        if (!col.CompareTag("Player")) return;

        var segment = GetComponentInParent<EnviromentSegment>();
        if(segment == null || segment.spawnPoint == null)
        {
            Debug.LogError("Missing EnviromentSegment or spawmpoint on prefab");
            return;
        }

        Instantiate(
            enviromentPrefab,
            segment.spawnPoint.position,
            segment.spawnPoint.rotation
            );

        hasSpawned = true;
    }
}
