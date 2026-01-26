using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        if (enemyData != null && meshFilter != null)
        {
            meshFilter.mesh = enemyData.EnemyMesh;
            if (meshRenderer != null && enemyData.material != null)
            {
                meshRenderer.material = enemyData.material;
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
        if (other.CompareTag("OutOfBounds"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with Player: " + enemyData.EnemyName);
        }
    }
}
