using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [SerializeField] private int _cost;
    [SerializeField] private bool _canBuy;
    [SerializeField] private Sprite _image;
    [SerializeField] private Sprite _backGround;
    [SerializeField] private Sprite _dimBackground;
    [SerializeField] private ItemRarity _rarity;
    [SerializeField] private string _description;

    private Slot _currentSlot;
    
    public int Cost => _cost;
    public string Description => _description;
    public Sprite Image => _image;
    public Sprite BackGround => _backGround;
    public Sprite DimBackGround => _dimBackground;
    public ItemRarity Rarity => _rarity;
    public bool CanBuy { get { return _canBuy; } set { _canBuy = value; } }
    public Slot Slot { get { return _currentSlot; } set { _currentSlot = value; } }
}
    
