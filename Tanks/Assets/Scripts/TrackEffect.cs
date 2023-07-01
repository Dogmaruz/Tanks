using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class TrackEffect : MonoBehaviour
{
    [Range(0f, 4f)]
    [SerializeField] private float m_ParallaxPower;

    private Material m_QuadMaterial;

    private Vector2 m_InitialOffset;

    private void Start()
    {
        m_QuadMaterial = GetComponent<SpriteRenderer>().material;

        m_InitialOffset = UnityEngine.Random.insideUnitCircle;
    }

    private void Update()
    {
        Vector2 offset = m_InitialOffset;

        //offset.x += transform.position.x / m_ParallaxPower;

        offset.y += transform.position.y / m_ParallaxPower;

        m_QuadMaterial.mainTextureOffset = offset;
    }
}

