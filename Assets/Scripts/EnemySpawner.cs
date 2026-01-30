using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private Transform LimitA;
    [SerializeField] private Transform LimitB;
    [SerializeField, Min(0.01f)] private float spawnInterval = 2f;
    [SerializeField] private bool spawnOnEnable = true;

    private Coroutine spawnRoutine;

    private void OnEnable()
    {
        if (spawnOnEnable)
        {
            StartSpawning();
        }
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    public void StartSpawning()
    {
        if (spawnRoutine == null)
        {
            if (!IsConfigValid())
            {
                Debug.LogWarning("EnemySpawner: Configuración inválida. Verifica la lista Enemies y los límites A/B.", this);
                return;
            }
            spawnRoutine = StartCoroutine(SpawnLoop());
        }
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnLoop()
    {
        var wait = new WaitForSeconds(spawnInterval);
        while (true)
        {
            SpawnOne();
            yield return wait;
        }
    }

    private void SpawnOne()
    {
        if (!IsConfigValid()) return;

        GameObject prefab = Enemies[Random.Range(0, Enemies.Count)];
        Vector3 pos = RandomPointOnSegment(LimitA.position, LimitB.position);
        Instantiate(prefab, pos, Quaternion.identity);
    }

    private static Vector3 RandomPointOnSegment(Vector3 a, Vector3 b)
    {
        float t = Random.Range(0f, 1f);
        return Vector3.Lerp(a, b, t);
    }

    private bool IsConfigValid()
    {
        return Enemies != null && Enemies.Count > 0 && LimitA != null && LimitB != null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (LimitA != null && LimitB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(LimitA.position, LimitB.position);
            Gizmos.DrawSphere(LimitA.position, 0.1f);
            Gizmos.DrawSphere(LimitB.position, 0.1f);
        }
    }
#endif
}
