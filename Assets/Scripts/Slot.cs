using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerClickHandler,ISelectHandler,IDeselectHandler
{
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private Image _background;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _coin;
    [SerializeField] private Image _selectedFrame;

    [SerializeField] private Sprite _emptyBackGround;
    [SerializeField] private Sprite _dimCoinImage;
    [SerializeField] private Sprite _coinImage;

    [SerializeField] private ItemSO _currentItem;

    private CurrentItemView _currentItemView;
    private int _cost;

    private void Awake()
    {
        _currentItemView = CurrentItemView.instance;
        DisableSlot();
        _selectedFrame.enabled = false;
    }

    public ItemSO CurrentItem => _currentItem;

    public void FillSlot(ItemSO item, int counter)
    {
        _currentItem = item;

        _cost = item.Cost;
        _costText.text = _cost.ToString();
        _counterText.text = counter.ToString();
        _itemImage.sprite = item.Image;

        item.Slot = this;

        RefreshState(item);        
    }

    public void RefreshState(ItemSO item)
    {
        if (item.CanBuy)
        {
            _background.sprite = item.BackGround;
            _coin.sprite = _coinImage;
        }
        else
        {
            _background.sprite = item.DimBackGround;
            _coin.sprite = _dimCoinImage;
        }
    }

    public void DisableSlot()
    {
        _costText.text = "";
        _counterText.text = "";
        _itemImage.enabled = false;
        _background.sprite = _emptyBackGround;
    }

    public void EnableSlot()
    {
        _itemImage.enabled = true;
    }

    public void ClearSlot()
    {
        _currentItem = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.selectedObject = this.gameObject;

        GetSlot();

        if (_currentItem != null)
        {
            if (_currentItem.CanBuy)
            {
                _currentItem = gameObject.GetComponent<Slot>().CurrentItem;
                _currentItemView.UpdateFrame(_currentItem);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _selectedFrame.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _selectedFrame.enabled = false;
    }

    private void GetSlot()
    {
        ShopSystem.instance.CurrentSlot = this;
    }
}
