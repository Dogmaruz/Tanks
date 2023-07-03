using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected bool m_Interactable;

    public UnityEvent OnClick;

    public event UnityAction<UIButton> PointerEnter;

    public event UnityAction<UIButton> PointerExit;

    public event UnityAction<UIButton> PointerClick;


    private bool _focuse = false;
    public bool Focuse => _focuse;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (m_Interactable == false) return;

        PointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (m_Interactable == false) return;

        PointerExit?.Invoke(this);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (m_Interactable == false) return;

        PointerClick?.Invoke(this);

        OnClick?.Invoke();
    }

    public virtual void SetFocuse()
    {
        if (m_Interactable == false) return;

        _focuse = true;
    }

    public virtual void SetUnFocuse()
    {
        if (m_Interactable == false) return;

        _focuse = false;
    }
}
