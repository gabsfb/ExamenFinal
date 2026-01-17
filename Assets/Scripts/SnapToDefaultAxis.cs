using UnityEngine;

public class SnapToDefaultAxis : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(GlobalConfig.Instance != null || GlobalConfig.Instance.GlobalConfigData != null)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.z = GlobalConfig.Instance.GlobalConfigData.DefaultAxis_Z;
            transform.position = currentPosition;
        }
            

    }


}
