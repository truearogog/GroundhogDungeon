using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ProjectileSpell : AbstractSpell
{
    #region Variables

    #region Components
    #endregion

    #region Public Variables
    #endregion

    #region Private Variables

    #region Serializable
    [SerializeField] private float m_attackSpeed;
    [Space]
    [SerializeField] private float m_projectileSpeed;
    [SerializeField] private float m_projectileAddSpeedOverTime;
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private AnimationCurve m_moveCurve;
    [SerializeField] private float m_projectileLifeTime;
    [SerializeField] private Vector2 m_projectileLocalSpawnPosition;
    #endregion

    #region Non-Serializable
    // if m_shootTime <= 0 then we can shoot
    private float m_shootTime;
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods
    protected override void Start()
    {
        base.Start();
    }

    protected void FixedUpdate()
    {
        if (m_shootTime > 0)
        {
            m_shootTime -= Time.deltaTime;
        }
    }

    #endregion

    #region Custom Methods

    public override void Shoot()
    {
        if (m_shootTime <= 0 && gameObject.activeSelf)
        {
            m_shootTime = m_attackSpeed;
            GameObject projectile = Instantiate(m_projectilePrefab, transform.position + transform.right * m_projectileLocalSpawnPosition.x + transform.up * m_projectileLocalSpawnPosition.y, transform.rotation);
            projectile.GetComponent<ProjectileController>().Init(m_projectileSpeed, m_projectileAddSpeedOverTime, m_moveCurve);
            PlayShootSound();
            //StartCoroutine(ExecuteAfterTime(m_projectileLifeTime, projectile));
        }
    }

    public override void OnPick()
    {
        base.OnPick();
    }

    #endregion
}
