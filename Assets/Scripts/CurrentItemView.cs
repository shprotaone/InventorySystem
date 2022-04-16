using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Просмотр выбранного Item
/// </summary>
public class CurrentItemView : MonoBehaviour
{
    public static CurrentItemView instance;

    [SerializeField] private Inventory _inventory;

    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _fieldImage;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Sprite _defaultField;
    [SerializeField] private GameObject _panel;

    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _sellButton;

    private ItemSO _currentItem;

    private int _cost;

    public ItemSO itemDescription => _currentItem; 
    public int Cost => _cost;

    private void OnEnable()
    {
        if (instance == null)
        instance = this;
    }

    private void Start()
    {
        CheckSelected();
        _itemImage.enabled = false;
    }

    public void UpdateFrame(ItemSO item)
    {
        _itemImage.enabled = true;
        _currentItem = item;
        _itemImage.sprite = item.Image;
        _fieldImage.sprite = item.BackGround;
        _cost = item.Cost;
        _descriptionText.text = item.Description;

        AccessToSell();
        CheckSelected();
    }

    private void CheckSelected()
    {
        if(_currentItem == null && _panel.activeInHierarchy)
        {
            _panel.SetActive(false);
        }
        else
        {
            _panel.SetActive(true);
        }
    }

    public void AccessToSell()
    {
        if (_inventory.FindItem(_currentItem))
        {
            _sellButton.interactable = true;
        }
        else
        {
            _sellButton.interactable = false;
        }
    }

    public void EmptyView()
    {
        if (_panel.activeInHierarchy)
        {
            _panel.SetActive(false);
            _fieldImage.sprite = _defaultField;
        }
    }
}
