using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CurrentItemView _currentItemView;

    private Dictionary<ItemSO, int> _itemList;

    private Slot[] _slots;
    private Slot _selectedSlot;
    private ItemSO _currentItem;

    public CurrentItemView ItemView => _currentItemView;

    private void Start()
    {
        _slots = GetComponentsInChildren<Slot>();
        Init();
    }

    private void Init()
    {
        _itemList = new Dictionary<ItemSO, int>();

        foreach (var slot in _slots)
        {
            slot.DisableSlot();
        }
    }

    /// <summary>
    /// Заполение слота в инвентаре при покупке
    /// </summary>
    /// <param name="autoFilled"></param>
    public void Filling(bool autoFilled)
    {
        if (autoFilled)
        {
            FindFreeSlot();
            AddToItem(_currentItemView.itemDescription);
        }
        else
        {
            AddToItem(_currentItemView.itemDescription);
            ClearCurrentSlot();
        }
    }

    public void AddToItem(ItemSO item)
    {      
        _currentItem = item;

        if (_itemList.ContainsKey(item))
        {
            _itemList[item]++;
            _selectedSlot = item.Slot;
        }
        else
        {
            _selectedSlot.FillSlot(item,1);       
            _itemList.Add(item, 1);     
        }
        
        UpdateInventory(_selectedSlot);        
    }

    public void DeleteFromInventory(ItemSO item)
    {
        Slot slot = item.Slot;

        if (_itemList.ContainsKey(item))
        {
            _itemList[item]--;
        }

        if(_itemList[item] <= 0)
        {
            slot.ClearSlot();
            _itemList[item] = 0;
            _itemList.Remove(item);

        }

        UpdateInventory(slot);
    }

    private void UpdateInventory(Slot slot)
    { 
        _currentItem = slot.CurrentItem;

        if(_currentItem == null)
        {
            slot.DisableSlot();
        }
        else
        {
            slot.FillSlot(_currentItem, _itemList[_currentItem]);
            slot.EnableSlot();
        }        
    }

    private void FindFreeSlot()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if(_slots[i].CurrentItem == null)
            {
                _selectedSlot = _slots[i];
                break;
            }
        }
    }

    /// <summary>
    /// Присвоение слота при неавтоматическом заполнении
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public bool SetSlotInInventory(Slot slot)
    {
        while (slot != null)
        {
            if(slot.CurrentItem == null)
            {
                _selectedSlot = slot;
                return true;
            }
            else
            {
                Debug.Log("Slot is not empty");
                return false;
            }           
        }

        return false;
    }

    public bool FindItem(ItemSO itemIn)
    {
        foreach (var item in _itemList)
        {
            if(item.Key == itemIn)
            {
                return true;
            }
        }
        return false;
    }

    private void ClearCurrentSlot()
    {
        _selectedSlot = null;
    }
}
