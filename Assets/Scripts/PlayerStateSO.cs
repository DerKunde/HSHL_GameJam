using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObjects/PlayerState")]
public class PlayerStateSO : ScriptableObject
{
    [SerializeField] private int _coins;
    [SerializeField] private int _currHealth;
    [SerializeField] private int _maxHealth;
    

    #region Health

    public int CurrHealth
    {
        get => _currHealth;
        set => SetHealth(value);
    }

    private void SetHealth(int value)
    {
        _currHealth = Mathf.Clamp(value, 0, _maxHealth);
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

    private void SetCoins(int value)
    {
        //ToDo: Checks if Player has to many coins => apply negative effects | and vise versa
        _coins = value;
    }

    #endregion
    
}
