using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObjects/PlayerState")]
public class PlayerStateSO : ScriptableObject
{
    [SerializeField] private int _coins;
    [SerializeField] public int _currHealth;
    [SerializeField] private int _maxHealth;
    [SerializeField] public int _coinsLimit = 50;  //After that Limit player gets negative effects    

    public bool negativeCoinEffected = false;

    #region Health

    public int CurrHealth
    {
        get => _currHealth;
        set => SetHealth(value);
    }

    public int GetCurrHealth()
    {
        return _currHealth;
    }

    public void SetHealth(int value)
    {
        _currHealth = Mathf.Clamp(value, 0, _maxHealth);
        Debug.Log("health: " + _currHealth);
        if (_currHealth.Equals(0))
            GameManager.PlayerDied();
    }

    #endregion
    #region Coins

    public int Coins
    {
        get => _coins;
        set => SetCoins(value);
    }


    public void SetCoins(int value)
    {
        _coins = value;

        if (_coins >= _coinsLimit)
            negativeCoinEffected = true;
        else negativeCoinEffected = false;

        Debug.Log("coins: " + _coins);
    }

    public int GetCurrCoins()
    {
        return _coins;
    }

    #endregion

}
