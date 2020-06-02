using UnityEngine;

public abstract class AbstractSpell : MonoBehaviour
{
    public enum SpellType { 
        Projectile, Beam
    }
    public SpellType spellType;
    public bool m_isBound = false;

    public string evilName;

    [SerializeField] protected string m_spellName;
    [Space]
    [SerializeField] private string m_shootSound;
    [Space]
    [SerializeField] protected float m_damage;
    [Space]
    [SerializeField] protected float m_criticalChance;
    [SerializeField] protected float m_criticalDamage;

    protected AudioManager m_audioManager;

    protected virtual void Start()
    {
        m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public virtual void OnPick()
    {
        if (GetComponent<TimeAutoDestroy>())
            GetComponent<TimeAutoDestroy>().enabled = false;
    }

    public abstract void Shoot();

    public void SetDamageStats(ref DamageManager damageManager)
    {
        damageManager.SetDamageStats(m_damage, m_criticalDamage, m_criticalChance);
    }

    protected void PlayShootSound()
    {
        m_audioManager.Play("SFX", m_shootSound);
    }
}
