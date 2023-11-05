using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /* Image References */
    [SerializeField] private Image _attack;
    [SerializeField] private Image _jump;
    [SerializeField] private Image _twist;
    [SerializeField] private Image _lifebar;

    /* Text References */
    // [SerializeField] private TextMeshPro _healthValue;

    /* Variables */
    private const int COIN_VALUE_ON_PICKUP = 10;
    private int _coinCount = -10;
    private float _characterCurrentLife;

    private void Start()
    {
        /* Suscripci�n de eventos */
        EventManager.instance.OnCharacterLifeChange += UpdateLifebar;
        EventManager.instance.OnCharacterAttack += UpdateAttack;
        EventManager.instance.OnCharacterTwist += UpdateTwist;
        EventManager.instance.OnCharacterJump += UpdateJump;

        /* Inits */
        UpdateCoinsValue();
    }

    private void UpdateLifebar(float currentLife, float maxLife)
    {
        _lifebar.fillAmount = currentLife / maxLife;
        _characterCurrentLife = currentLife;
    }

    private void UpdateAttack(float current, float max)
    {
        _attack.fillAmount = current / max;
    }

    private void UpdateTwist(float current, float max)
    {
        _twist.fillAmount = current / max;
    }

    private void UpdateJump(float current)
    {
        _jump.fillAmount = current;
    }

    private void UpdateCoinsValue()
    {
        _coinCount += COIN_VALUE_ON_PICKUP;
        // _coinsValue.text = $"$ {_coinCount}";
    }
}
