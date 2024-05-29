using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager: MonoBehaviour
{
    public static GlobalManager instance;
    public bool hasObjective = false;
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
