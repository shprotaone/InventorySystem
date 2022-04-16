using UnityEngine;
using System;

[Serializable]
public class Items : MonoBehaviour
{
    [SerializeField]
    private ItemSO[] _items;

    public ItemSO[] ItemsArray => _items;

    public void SortItems()
    {
        ItemSO temp;

        for (int i = 0; i < _items.Length; i++)
        {
            for (int j = i + 1; j < _items.Length; j++)
            {
                if (_items[i].Rarity > _items[j].Rarity)
                {
                    temp = _items[i];
                    _items[i] = _items[j];
                    _items[j] = temp;
                }
            }
        }
    }
}
