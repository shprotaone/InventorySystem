using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Сбрасывает выделенный Item
/// </summary>
public class Deselector : MonoBehaviour, IPointerClickHandler
{
    private CurrentItemView _currentItemView;
    private void Start()
    {
        _currentItemView = CurrentItemView.instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _currentItemView.EmptyView();
    }
}
