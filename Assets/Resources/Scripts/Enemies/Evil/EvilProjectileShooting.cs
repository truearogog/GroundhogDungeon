using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilProjectileShooting : MonoBehaviour
{
    #region Variables

    #region Components
    #endregion

    #region Public Variables
    #endregion

    #region Private Variables

    #region Serializable
    [SerializeField] protected uint m_damage;
    [Space]
    [SerializeField] private float m_attackSpeed;
    [Space]
    [SerializeField] private float m_projectileSpeed;
    [SerializeField] private float m_projectileAddSpeedOverTime;
    [SerializeField] private AnimationCurve m_moveCurve;
    [Space]
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private Vector2 m_projectileLocalSpawnPosition;
    [Space]
    [SerializeField] private Transform m_staffTransform;
    [Space]
    [SerializeField] private string m_shootSound;
    #endregion

    #region Non-Serializable
    // if m_shootTime <= 0 then we can shoot
    private float m_shootTime;
    private GameObject m_targetPlayer;
    private AudioManager m_audioManager;
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods

    void Start()
    {
        if (GetComponent<EvilMovement>())
        {
            m_targetPlayer = GetComponent<EvilMovement>().targetPlayer;
        }
        m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        if (m_shootTime > 0)
        {
            m_shootTime -= Time.deltaTime;
        }

        if (m_shootTime <= 0 && m_targetPlayer)
        {
            Shoot();
        }
    }

    #endregion

    #region Custom Methods

    private void Shoot()
    {
        if (m_shootTime <= 0 && gameObject.activeSelf)
        {
            m_shootTime = m_attackSpeed;
            GameObject projectile = Instantiate(m_projectilePrefab, m_staffTransform.position + m_staffTransform.right * m_projectileLocalSpawnPosition.x + m_staffTransform.up * m_projectileLocalSpawnPosition.y, m_staffTransform.rotation);
            projectile.GetComponent<EvilProjectileController>().Init(m_damage, m_projectileSpeed, m_projectileAddSpeedOverTime, m_moveCurve);
            PlayShootSound();
        }
    }

    private void PlayShootSound()
    {
        m_audioManager.Play("SFX", m_shootSound);
    }

    #endregion
}
