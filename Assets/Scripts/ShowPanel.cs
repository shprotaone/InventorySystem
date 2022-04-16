using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Show()
    {
        _animator.gameObject.SetActive(true);      

        _animator.SetBool("Open", true);  
    }

    public void Close()
    {
        _animator.SetBool("Open", false);
    }

    public void DisablePanel()
    {
        _animator.gameObject.SetActive(false);
    }
}
