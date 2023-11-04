using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    static public EventManager instance;

    #region UNITY_EVENTS
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    #endregion

    #region GAME_MANAGER
    public event Action<bool> OnGameOver;
    public event Action<bool> OnTwist;

    public void EventGameOver(bool isVictory) 
    {
        if (OnGameOver != null) OnGameOver(isVictory);
    }

    public void EventTwist(bool isTwist) 
    {
        if (OnGameOver != null) OnTwist(isTwist);
    }
    #endregion

    #region IN_GAME_UI
    public event Action<float, float> OnCharacterLifeChange;
    public event Action<int> OnCoinPickup;

    public void CharacterLifeChange(float currentLife, float maxLife)
    {
        if (OnCharacterLifeChange != null) OnCharacterLifeChange(currentLife, maxLife);
    }
    #endregion
}