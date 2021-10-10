using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public PlayerStateSO stat;
    public TMP_Text tmp;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        tmp.text = stat.Coins + "";
    }
}
