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
    public event Action OnObjectivePickup;
    public event Action onGameOver;
    public event Action onSceneChange;
    public event Action onGameWin;

    public void EventObjectivePickup()
    {
        if (OnObjectivePickup != null) OnObjectivePickup();
    }
    
    public void EventSceneChange()
    {
        if (onSceneChange != null) onSceneChange();
    }

    public void EventGameWin()
    {
        if (onGameWin != null) onGameWin();
    }

    public void EventGameOver(bool isVictory) 
    {
        if (OnGameOver != null) OnGameOver(isVictory);
    }

    public void EventTwist(bool isTwist) 
    {
        if (OnTwist != null) OnTwist(isTwist);
    }
    #endregion

    #region IN_GAME_UI
    public event Action<float, float> OnCharacterLifeChange;
    public event Action<float, float> OnCharacterAttack;
    public event Action<float, float> OnCharacterTwist;
    public event Action<float> OnCharacterJump;
    public event Action<int> OnCoinPickup;

    public void CharacterLifeChange(float currentLife, float maxLife)
    {
        if (OnCharacterLifeChange != null) OnCharacterLifeChange(currentLife, maxLife);
    }

    public void CharacterAttack(float current, float max)
    {
        if (OnCharacterAttack != null) OnCharacterAttack(current, max);
    }

    public void CharacterTwist(float current, float max)
    {
        if (OnCharacterTwist != null) OnCharacterTwist(current, max);
    }

    public void CharacterJump(float current)
    {
        if (OnCharacterJump != null) OnCharacterJump(current);
    }
    #endregion
}