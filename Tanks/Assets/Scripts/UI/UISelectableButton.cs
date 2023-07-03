using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image m_selectImage;

    public UnityEvent OnSelect;

    public UnityEvent OnUnSelect;

    private DG.Tweening.Sequence _sequence;

    public override void SetFocuse()
    {
        base.SetFocuse();

        m_selectImage.enabled = true;

        _sequence.Kill();

        _sequence = DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f))
            .SetUpdate(true)
            .SetEase(Ease.InOutQuad)
            .SetLink(gameObject);


        OnSelect?.Invoke();
    }

    public override void SetUnFocuse()
    {
        base.SetUnFocuse();

        m_selectImage.enabled = false;

        _sequence.Kill();

        _sequence = DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f))
            .SetUpdate(true)
            .SetEase(Ease.InOutQuad)
            .SetLink(gameObject);

        OnUnSelect?.Invoke();
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }
}
