using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class XTremeModToggle : MonoBehaviour
{
    public Toggle Toggle;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Toggle.SetIsOnWithoutNotify(false);
        }
        else
        {
            Toggle.SetIsOnWithoutNotify(true);
        }
    }
    public void OnToggle()
    {
       if (SceneManager.GetActiveScene().name == "MainScene")
        {
            SceneManager.LoadScene("XTremeMode");
        }

        if (SceneManager.GetActiveScene().name == "XTremeMode")
        {
            SceneManager.LoadScene("MainScene");
        }

    }
}
