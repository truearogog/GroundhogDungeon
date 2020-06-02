using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilProjectileController : MonoBehaviour
{
    [SerializeField] private CustomProjectile m_customProjectileScript;
    [SerializeField] private LayerMask m_destroyMask;
    [Space]
    [SerializeField] private string m_destroySound; 
    private AudioManager m_audioManager;
    private Rigidbody2D m_rigidbody2D;
    private uint m_damage;
    private float m_currentSpeed;
    private float m_addSpeedOverTime;
    private AnimationCurve m_moveCurve;

    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        if (m_rigidbody2D)
        {
            m_currentSpeed += m_addSpeedOverTime * Time.deltaTime;
            m_rigidbody2D.velocity = transform.right * m_currentSpeed * Time.deltaTime + transform.up * m_moveCurve.Evaluate(Time.time % m_moveCurve.length);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (m_customProjectileScript)
        {
            if (m_destroyMask == (m_destroyMask | (1 << col.gameObject.layer)))
            {
                if (m_customProjectileScript)
                    m_customProjectileScript.Init(transform);
                m_audioManager.Play("SFX", m_destroySound);
                Destroy(gameObject);
            }
        }
    }

    public void Init(uint damage, float currentSpeed, float addSpeedOverTime, AnimationCurve moveCurve)
    {
        m_damage = damage;
        m_currentSpeed = currentSpeed;
        m_addSpeedOverTime = addSpeedOverTime;
        m_moveCurve = moveCurve;
    }

    public uint GetDamage()
    {
        return m_damage;
    }

    public void CustomProjectileStart()
    {
        if (m_customProjectileScript)
        {
            m_customProjectileScript.Init(transform);
        }
    }
}
