using UnityEngine;
using UnityEngine.Events;

public class Window : MonoBehaviour
{
    [SerializeField] private UnityEvent _opened;
    [SerializeField] private UnityEvent _closed;

    public UnityEvent opened { get => _opened;}
    public UnityEvent closed { get => _closed;}

    public void Open()
    {
        var animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.SetBool("visibility", true);
        }
        else
        {
            this.gameObject.SetActive(true);
            _opened?.Invoke();
        }
    }
    public void Close()
    {

        var animator = this.GetComponent<Animator>();
        if (animator)
        {
            animator.SetBool("visibility", false);
        }
        else
        {
            this.gameObject.SetActive(false);
            _closed?.Invoke();
        }

    }
    public void OnOpened()
    {
        _opened?.Invoke();
    }
    public void OnClosed()
    {
        _closed?.Invoke();
    }
}
