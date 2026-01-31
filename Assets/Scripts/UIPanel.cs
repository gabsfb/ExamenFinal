using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }
}
