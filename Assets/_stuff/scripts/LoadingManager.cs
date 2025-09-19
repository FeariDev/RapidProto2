using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class LoadingManager : MonoBehaviour
{
    public Image progressBar;
    public TextMeshProUGUI progressText;

    private void Start()
    {
        PlayerPrefs.SetString("SceneToLoad", "Pool_Area");
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        string SceneToLoad = PlayerPrefs.GetString("SceneToLoad");
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneToLoad);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if(progressBar != null) progressBar.fillAmount = progress;
            if(progressText != null) progressText.text = (progress * 100f).ToString("F0") + "%";
            if(operation.progress >= 0.9f) operation.allowSceneActivation = true;
            yield return null;
        }
    }
}
