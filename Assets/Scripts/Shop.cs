using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;
    [SerializeField] private Items _itemContainer;
    [SerializeField] private CurrentItemView _currentItemView;

    [SerializeField] private MoneyContainer _moneyContainer;
    [SerializeField] private Inventory _inventory;

    private int _startAmount = 99;
    private int _rareLimitMoney = 250;

    private void Start()
    {
        Init();     
    }

    private void Init()
    {
        _itemContainer.SortItems();
        _slots = GetComponentsInChildren<Slot>();

        FillShop();
    }

    private void FillShop()
    {               
        for (int i = 0; i < _itemContainer.ItemsArray.Length; i++)
        {
            _slots[i].EnableSlot();
            _slots[i].FillSlot(_itemContainer.ItemsArray[i],_startAmount);

            if(_slots[i].CurrentItem.Rarity == ItemRarity.RARE)
            {
                _slots[i].CurrentItem.CanBuy = false;
            }
        }
    }  

    public void CheckRareLimitMoney()
    {
        if (_moneyContainer.Money > _rareLimitMoney)
        {
            foreach (var slot in _slots)
            {
                if (!slot.CurrentItem.CanBuy)
                {
                    slot.CurrentItem.CanBuy = true;
                    slot.RefreshState(slot.CurrentItem);
                }              
            }
        }
    }
}
