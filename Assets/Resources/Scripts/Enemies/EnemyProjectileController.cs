using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    private uint m_damage;
    private Vector2 m_dir;
    private float m_currentSpeed;
    private float m_addSpeedOverTime;

    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (m_rigidbody2D)
        {
            m_currentSpeed += m_addSpeedOverTime * Time.deltaTime;
            m_rigidbody2D.velocity = m_dir * m_currentSpeed * Time.deltaTime;
        }
    }

    public void Init(uint damage, Vector2 dir, float currentSpeed, float addSpeedOverTime)
    {
        m_damage = damage;
        m_dir = dir;
        m_currentSpeed = currentSpeed;
        m_addSpeedOverTime = addSpeedOverTime;
    }

    public uint GetDamage()
    {
        return m_damage;
    }
}
