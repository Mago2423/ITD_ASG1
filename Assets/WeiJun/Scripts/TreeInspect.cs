using UnityEngine;

public class TreeInspect : MonoBehaviour
{
    public GameObject panel;

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
