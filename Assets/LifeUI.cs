using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    public PlayerStateSO stat;
    private TMP_Text tmp;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        tmp.text = stat.CurrHealth + "";
    }
}
