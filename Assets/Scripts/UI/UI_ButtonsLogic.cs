using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_ButtonsLogic : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene("Menu");
    public void LoadLevelScene() => SceneManager.LoadScene("LevelLoader");
    public void LoadEndgameScene() => SceneManager.LoadScene("Endgame");
    public void LoadInfoScene() => Debug.Log("Information scene in development!!!");
    public void LoadSettingsScene() => Debug.Log("Settings scene in development!!!");
    public void CloseGame() => Application.Quit();

}
