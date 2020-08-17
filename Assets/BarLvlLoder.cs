using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarLvlLoder : MonoBehaviour
{
    public float minTime = 0.5f;

    [Space]
    public GameObject loadingScreen;
    public Slider progressSlider;
    public Text progressText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadLevel(1);
        }
    }


    public void LoadLevel( int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
        
    }

    IEnumerator LoadAsync(int index)
    {
        float t = 0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync(index,LoadSceneMode.Additive);
        //operation.allowSceneActivation = false;
        

        loadingScreen.SetActive(true);
        progressSlider.value = 0f;
        progressText.text = "00%";

        while (!operation.isDone || t <= minTime)
        {
            t += Time.deltaTime;

            float p = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Progress: " + p +"  Time: "+ t);
            progressSlider.value = p;
            progressText.text = (p * 100) +"%";
            yield return null;
        }
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(index) );
        SceneManager.UnloadSceneAsync (SceneManager.GetSceneAt(0));

    }
}
