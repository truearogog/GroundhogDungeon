using UnityEngine;

public class EvilStaff : MonoBehaviour
{
    [SerializeField] private Transform m_staffTransform;
    private EnemyMovement m_movement;
    private SpriteRenderer m_spriteRenderer;
    private Vector3 m_startScale;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_movement = GetComponent<EnemyMovement>();
        m_startScale = m_staffTransform.localScale;
    }

    void Update()
    {
        RotateStaff(GetZRotation());
        CheckForFlipX();
    }

    //VISUAL
    private float GetZRotation()
    {
        return (90f - Mathf.Atan2(m_movement.toPlayer.x, m_movement.toPlayer.y) * Mathf.Rad2Deg);
    }

    private void RotateStaff(float zrot)
    {
        /*
        float sign = m_movement.toPlayer.x - m_staffTransform.position.x;
        if (((sign < 0) && !m_spriteRenderer.flipX) || ((sign > 0) && m_spriteRenderer.flipX))
            m_staffTransform.localScale = new Vector3(m_staffTransform.localScale.x, -m_startScale.y, m_startScale.z);
        else
            m_staffTransform.localScale = new Vector3(m_staffTransform.localScale.x, m_startScale.y, m_startScale.z);
        */
        m_staffTransform.rotation = Quaternion.Euler(0, 0, zrot);
    }

    void CheckForFlipX()
    {
        float sign = m_movement.toPlayer.x - m_staffTransform.position.x;
        if (((sign < 0) && !m_spriteRenderer.flipX) || ((sign > 0) && m_spriteRenderer.flipX))
            FlipX();
    }

    void FlipX()
    {
        m_staffTransform.localPosition = new Vector3(-m_staffTransform.localPosition.x, m_staffTransform.localPosition.y, m_staffTransform.localPosition.z);
    }
}
