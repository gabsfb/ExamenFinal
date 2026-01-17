using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    public static GlobalConfig Instance { get; private set; }

    // Referencia pública para asignar vía Inspector
    public GlobalConfigData GlobalConfigData;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
