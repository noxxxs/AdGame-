using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Image image;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    bool _isRestarting = false;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartCoroutine(FadeOut(image, fadeOutDuration));
    }

    private void Update()
    {
        if (InputManager.Instance.GetAction("RestartLevel").WasPressedThisFrame())
        {
            RestartLevel();
            _isRestarting = true;
        }

        if (InputManager.Instance.GetAction("Quit").WasPressedThisFrame())
        {
            Application.Quit();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null && _isRestarting == false)
        {
            _isRestarting = true;
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        FadeInTransition(SceneManager.GetActiveScene().name);
    }

    public void FadeInTransition(string sceneToLoad)
    {
        StartCoroutine(FadeIn(image, fadeInDuration, sceneToLoad));
    }

    private IEnumerator FadeOut(Image img, float duration)
    {
        Color color = img.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0, time / duration);
            img.color = color;
            yield return null;
        }

        color.a = 0;
        img.color = color;
    }

    private IEnumerator FadeIn(Image img, float duration, string sceneToLoad)
    {
        Color color = img.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 1, time / duration);
            img.color = color;
            yield return null;
        }

        color.a = 1;
        img.color = color;
        SceneManager.LoadScene(sceneToLoad);
    }
}
