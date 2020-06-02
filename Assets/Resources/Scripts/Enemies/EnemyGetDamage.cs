using UnityEngine;

public class EnemyGetDamage : MonoBehaviour
{
    #region Variables

    #region Components
    private HitFlash m_hitFlash;
    #endregion

    #region Public Variables
    public EnemySpawnManager enemySpawnManager;
    #endregion

    #region Private Variables

    #region Serializable
    [SerializeField] private float m_maxHealth;
    [SerializeField] private GameObject m_deathParticles;
    [SerializeField] private float m_hitFlashTime;
    [SerializeField] private int m_killScore;
    #endregion

    #region Non-Serializable
    private AudioManager m_audioManager;
    private CameraController m_cameraController;
    private DamageManager m_damageManager;
    private float m_currentHealth;
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods

    void Start()
    {
        m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_damageManager = GameObject.FindGameObjectWithTag("DamageManager").GetComponent<DamageManager>();
        m_cameraController = Camera.main.GetComponent<CameraController>();
        m_hitFlash = GetComponent<HitFlash>();
        m_currentHealth = m_maxHealth;
    }

    void FixedUpdate()
    {
        if (m_currentHealth == 0)
            Die();
    }

    //PARTICLE DAMAGE
    void OnParticleCollision(GameObject other)
    {
        if (other.layer == 10)
        {
            if (m_damageManager)
                TakeDamage(m_damageManager.GetDamage());
        }
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        //PROJECTILE DAMAGE
        if (col.gameObject.layer == 11)
        {
            if (m_damageManager)
            {
                if (m_damageManager.GetDamage() > 0)
                {
                    TakeDamage(m_damageManager.GetDamage());
                }
                else
                {
                    if (col.gameObject.GetComponent<ProjectileController>())
                        col.gameObject.GetComponent<ProjectileController>().CustomProjectileStart();
                }
            }

            Destroy(col.gameObject);
        }

        //AREA DAMAGE
        if (col.gameObject.layer == 18)
        {
            if (m_damageManager)
                TakeDamage(Mathf.Abs(m_damageManager.GetDamage()));
        }
    }

    #endregion

    #region Custom Methods

    private void TakeDamage(float damage)
    {
        m_hitFlash.Flash(m_hitFlashTime);
        m_audioManager.Play("SFX", "HurtEnemy");
        m_currentHealth = Mathf.Max(0, m_currentHealth - damage);
    }

    private void Die()
    {
        Instantiate(m_deathParticles, transform.position, Quaternion.Euler(90f, 0f, 0f));
        m_cameraController.Shake();
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().AddScore(m_killScore);

        // drop
        EnemyDropController[] drops = null;
        if (GetComponent<EnemyDropController>())
        {
            drops = gameObject.GetComponents<EnemyDropController>();
        }
        if (drops != null)
        {
            foreach (EnemyDropController drop in drops)
                drop.Drop();
        }

        if (GetComponent<EvilSpawnReset>())
            GetComponent<EvilSpawnReset>().ResetSpawn();

        if (enemySpawnManager && !GetComponent<EvilSpawnReset>())
            enemySpawnManager.currentEnemyCount -= 1;
        Destroy(gameObject);
    }

    #endregion
}
