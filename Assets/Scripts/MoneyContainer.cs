using System;
using UnityEngine;
using TMPro;

[Serializable]
public class MoneyContainer : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private TMP_Text _moneyView;
    [SerializeField] private int _money;
    private int _startMoney = 200;

    public int Money => _money;
    public int StartMoney => _startMoney;

    private void Start()
    {
        _money = _startMoney;
        UpdateView();
    }

    public void Increase(int cost)
    {
        _money += cost;
        UpdateView();
    }

    public void Decrease(int cost)
    {
        _money -= cost;
        UpdateView();
    }  

    private void UpdateView()
    {
        _moneyView.text = _money.ToString();
    }

    public void TakeMoney()
    {
        Increase(1000);

        Debug.Log("Complete");
        
        _shop.CheckRareLimitMoney();
        UpdateView();
    }
}
