using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string _targetSceneToLoad = "Level";
    private bool _enterScene;

    private void Start()
    {
        _enterScene = false;
        EventManager.instance.OnGameOver += OnGameOver;
        EventManager.instance.onSceneChange += EnterScene;
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_targetSceneToLoad);
        float progress = 0;
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            progress = asyncOperation.progress;
            if (asyncOperation.progress >= 0.9f)
            {
                if (_enterScene)
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void OnGameOver(bool isVictory)
    {
        if (isVictory)
        {
            Invoke("LoadWin", 0.5f);        
        }
        else {
            Invoke("LoadLose", 0.5f);        
        }

    }

    private void EnterScene()
    {
        _enterScene = true;
    }
    
    private void LoadWin() => SceneManager.LoadScene("EndSceneV");
    private void LoadLose() => SceneManager.LoadScene("EndSceneD");
}
