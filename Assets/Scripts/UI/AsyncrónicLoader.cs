using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncrónicLoader : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _progressText;
    [SerializeField] private string _targetSceneToLoad = "Level";

    void Start()
    {
        UpdateUIElements(0);
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_targetSceneToLoad);
        float progress = 0;

        // Permite frenar la carga automática del nivel congelando el progreso al 90%
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            progress = asyncOperation.progress;
            UpdateUIElements(progress);

            // Se verifica que la carga este terminada
            if (asyncOperation.progress >= 0.9f)
            {
                UpdateUIElements(1f);
                // Se actualiza el texto para confirmar el cambio de escena
                _progressText.text = "Press the space bar to continue";

                // Se espera al evento de presión de barra espaciadora
                if (Input.GetKeyDown(KeyCode.Space))
                    // Se activa la escena
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void UpdateUIElements(float value)
    {
        _progressBar.fillAmount = value;
        _progressText.text = $"Loading ... {value * 100} %";
    }
}
