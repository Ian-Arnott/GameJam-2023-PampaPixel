using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager: MonoBehaviour
{
    public static GlobalManager instance;
    public bool isVictory;
    public bool hasObjective = false;
    public bool twist = false;
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
