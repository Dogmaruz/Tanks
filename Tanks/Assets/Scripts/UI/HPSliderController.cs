using UnityEngine;
using UnityEngine.UI;

public class HPSliderController : MonoBehaviour
{
    [SerializeField] private Image m_HPProgressBar;

    [SerializeField] private FP_CharacterController m_character;

    private float m_FillAmountStep;
    public float FillAmountStep { get => m_FillAmountStep; set => m_FillAmountStep = value; }

    private void Start()
    {
        m_HPProgressBar.fillAmount = 1;

        m_FillAmountStep = 1f / (float)m_character.HitPoints;

        m_character.EventOnUpdateHP?.AddListener(UpdateHPProgress);
    }

    public void UpdateHPProgress(int count)
    {
        m_HPProgressBar.fillAmount = count * m_FillAmountStep;

        m_HPProgressBar.color = Color.Lerp(Color.red, Color.green, m_HPProgressBar.fillAmount);
    }

    public void UpdateShip(FP_CharacterController caracter)
    {
        m_character = caracter;

        m_character.EventOnUpdateHP?.AddListener(UpdateHPProgress);
    }

    private void OnDestroy()
    {
        m_character.EventOnUpdateHP?.RemoveListener(UpdateHPProgress);
    }
}
