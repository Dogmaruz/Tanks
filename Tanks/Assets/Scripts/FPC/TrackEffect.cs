using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class TrackEffect : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float m_trackSpeed;

    private Material m_QuadMaterial;

    private Vector2 m_offset;

    private FP_CharacterController m_characterController;

    private void Start()
    {
        m_QuadMaterial = GetComponent<SpriteRenderer>().material;

        m_characterController = transform.root.GetComponent<FP_CharacterController>();
    }

    private void Update()
    {
        m_offset.y += m_characterController.GetVelosity() * m_trackSpeed * Time.deltaTime;

        m_QuadMaterial.mainTextureOffset = m_offset;
    }
}

