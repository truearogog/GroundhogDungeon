using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private ParticleSystem m_particleSystem;

    void Awake()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (m_particleSystem)
        {
            if (!m_particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
