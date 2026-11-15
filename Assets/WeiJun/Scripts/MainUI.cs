using UnityEngine;

public class MainUI : MonoBehaviour
{
    public GameObject Log;
    public GameObject Shop;

    public void ToggleLog()
    {
        Log.SetActive(!Log.activeSelf);
    }
    public void ToggleShop()
    {
        Shop.SetActive(!Shop.activeSelf);
    }
}
