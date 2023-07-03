using UnityEngine;

public class UISelectableButtonContainer : MonoBehaviour
{
    [SerializeField] private Transform m_buttonsContainer;

    public bool Interactable = true;
    public void SetInteractable(bool interactable) => Interactable = interactable;

    private UISelectableButton[] m_buttons;

    private int _selectButtonIndex = 0;

    private void Start()
    {
        m_buttons = m_buttonsContainer.GetComponentsInChildren<UISelectableButton>();

        if (m_buttons == null) Debug.LogError("Button list is Empty");

        for (int i = 0; i < m_buttons.Length; i++)
        {
            m_buttons[i].PointerEnter += OnPointerEnter;
        }

        if (Interactable == false) return;

        m_buttons[_selectButtonIndex].SetFocuse();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < m_buttons.Length; i++)
        {
            m_buttons[i].PointerEnter -= OnPointerEnter;
        }
    }

    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    private void SelectButton(UIButton button)
    {
        if (Interactable == false) return;

        m_buttons[_selectButtonIndex].SetUnFocuse();

        for (int i = 0; i < m_buttons.Length; i++)
        {
            if (button == m_buttons[i])
            {
                _selectButtonIndex = i;

                m_buttons[i].SetFocuse();

                break;
            }
        }
    }

    public void SelectNext()
    {

    }

    public void SelectPrevious()
    {

    }
}
