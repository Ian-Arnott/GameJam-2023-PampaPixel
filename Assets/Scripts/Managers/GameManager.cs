using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool _isVictory = false;
    //[SerializeField] private Text _gameoverMessage;
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
        if (GlobalVictory.instance.isVictory)
        {
            Invoke("LoadWin", 1f);        
        }
        else {
            Invoke("LoadLose", 1f);        
        }

    }

    private void EnterScene()
    {
        _enterScene = true;
    }
    private void LoadWin() => SceneManager.LoadScene(6);
    private void LoadLose() => SceneManager.LoadScene(5);
}
