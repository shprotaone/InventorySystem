using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem instance;

    [SerializeField] private CurrentItemView _currentItemView;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Shop _shop;
    [SerializeField] private MoneyContainer _moneyContainer;
    [SerializeField] private GameObject _notification;
    [SerializeField] private Toggle _autoFilled;

    private ItemSO _itemForBuy;
    private TMP_Text _notificationText;

    public Slot CurrentSlot { get; set; }

    private void Start()
    {
        _notificationText = _notification.GetComponent<TMP_Text>();

        if(instance == null)
        instance = this;
    }

    public void Buy()
    {
        _itemForBuy = _currentItemView.itemDescription;
        if (CheckEnoughMoney(_currentItemView.Cost))
        {
            if (_autoFilled.isOn)
            {
                _inventory.Filling(true);
                Buying();
            }
            else
            {                
                StartCoroutine(BuyWithManualSlot());
            }
        }
        else
        {
            StartCoroutine(Notify());
        }
        
    }

    private bool CheckEnoughMoney(int cost)
    {
        if (_moneyContainer.Money > cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Sell()
    {
        _moneyContainer.Increase(_currentItemView.Cost / 2);
        _inventory.DeleteFromInventory(_currentItemView.itemDescription);

        _currentItemView.AccessToSell();
        _shop.CheckRareLimitMoney();
    }

    private IEnumerator BuyWithManualSlot()
    {
        CurrentSlot = null;
        _notification.SetActive(true);

        _notificationText.text = "<= Choose slot";
        _notificationText.color = Color.yellow;

        if (_inventory.FindItem(_itemForBuy))       //если item уже есть, автоматически прибавляет
        {
            _notification.SetActive(false);
            _inventory.Filling(true);
            Buying();
        }
        else
        {
            yield return new WaitUntil(() => _inventory.SetSlotInInventory(CurrentSlot));

            _notification.SetActive(false);

            Buying();
            _inventory.AddToItem(_itemForBuy);
        }        
    }

    private void Buying()
    {
        _moneyContainer.Decrease(_itemForBuy.Cost);
        _currentItemView.AccessToSell();
        _shop.CheckRareLimitMoney();
    }

    private IEnumerator Notify()
    {
        _notification.SetActive(true);
        _notificationText.color = Color.red;
        _notificationText.text = "Not enough money";

        yield return new WaitForSeconds(5f);

        _notification.SetActive(false);

        yield return null;
    }

}
