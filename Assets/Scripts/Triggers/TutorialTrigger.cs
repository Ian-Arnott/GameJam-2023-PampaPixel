using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    [SerializeField] private String message;

    void OnTriggerEnter()
    {
        text.text = message;
    }
}
