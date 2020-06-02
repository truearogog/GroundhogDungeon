using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using System.Collections.Generic;

public class EvilBeamShooting : MonoBehaviour
{
    #region Variables

    #region Components

    #endregion

    #region Public Variables
    public enum BeamType
    {
        ParticleBased,
        LineBased
    };
    public BeamType m_beamType;
    #endregion

    #region Private Variables

    #region Serializable
    [SerializeField] private float m_attackSpeed;
    [Space]
    [SerializeField] private ParticleSystem m_particleSystem;
    [SerializeField] private LineRenderer m_lineRenderer;
    [Space]
    [SerializeField] private Light2D m_light;
    [SerializeField] private float m_lightTime;
    [Space]
    [SerializeField] private string m_shootSound;
    #endregion

    #region Non-Serializable
    // if m_shootTime <= 0 then we can shoot
    private float m_shootTime;
    private float m_lightCurrentTime;
    private float m_lightIntensity;
    private AudioManager m_audioManager;
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods

    protected void Start()
    {
        m_lightIntensity = m_light.intensity;
        m_light.intensity = 0;
        m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    protected void FixedUpdate()
    {
        if (m_shootTime > 0)
        {
            m_shootTime -= Time.deltaTime;
        }

        if (m_shootTime <= 0)
        {
            Shoot();
        }

        if (m_lightCurrentTime > 0)
        {
            m_lightCurrentTime -= Time.deltaTime;
            m_light.intensity = m_lightCurrentTime / m_lightTime * m_lightIntensity;
        }
    }

    #endregion

    #region Customizable Methods

    private void Shoot()
    {
        if (m_beamType == BeamType.ParticleBased)
        {
            if (m_shootTime <= 0)
            {
                m_light.intensity = m_lightIntensity;
                m_particleSystem.Play();
                m_shootTime = m_attackSpeed;
                m_lightCurrentTime = m_lightTime;
                PlayShootSound();
            }
        }
    }

    private void PlayShootSound()
    {
        m_audioManager.Play("SFX", m_shootSound);
    }

    #endregion
}
