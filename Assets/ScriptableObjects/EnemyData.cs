using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Enemy")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public float MoveSpeed;
    public Mesh EnemyMesh;

    public Material material;

}
