using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Transform meshTransform;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        if (enemyData != null && meshFilter != null)
        {
            meshFilter.mesh = enemyData.EnemyMesh;
            if (meshRenderer != null && enemyData.material != null)
            {
                meshRenderer.material = enemyData.material;
            }
            Vector3 meshRotation = enemyData.MeshRotation;
            if (meshRotation.x != 0f || meshRotation.y != 0f || meshRotation.z != 0f)
            {
                Transform target = meshTransform != null
                    ? meshTransform
                    : (meshRenderer != null ? meshRenderer.transform : (meshFilter != null ? meshFilter.transform : transform));
                target.localRotation = Quaternion.Euler(meshRotation);
            }
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (enemyData != null)
        {
            transform.Translate(Vector3.left * enemyData.MoveSpeed * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndZone"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with Player: " + enemyData.EnemyName);
        }
    }
}
