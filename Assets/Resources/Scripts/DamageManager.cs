using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private float m_currentSpellDamage;
    private float m_currentSpellCriticalDamage;
    private float m_currentSpellCriticalChance;

    public void SetDamageStats(float currentSpellDamage, float currentSpellCriticalDamage, float currentSpellCriticalChance)
    {
        m_currentSpellDamage = currentSpellDamage;
        m_currentSpellCriticalDamage = currentSpellCriticalDamage;
        m_currentSpellCriticalChance = currentSpellCriticalChance;
    }

    public float GetDamage()
    {
        return m_currentSpellDamage * (Random.Range(0, 99) < m_currentSpellCriticalChance ? m_currentSpellCriticalDamage : 1);
    }
}
