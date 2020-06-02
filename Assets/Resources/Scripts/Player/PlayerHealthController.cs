using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealthController : MonoBehaviour
{
    #region Variables

    #region Components
    private HitFlash m_hitFlash;
    private BoxCollider2D m_playerCollider;
    #endregion

    #region Public Variables

    #endregion

    #region Private Variables

    #region Serializable Variables
    [SerializeField] private uint m_maxHealth;
    [SerializeField] private HeartsHealthVisual m_HeartsHealthVisual;
    [Space]
    [SerializeField] private float m_hitFlashTime;
    [SerializeField] private float m_invulnerabilityTime;
    [Space]
    [SerializeField] private GameObject m_deathAnimation;
    #endregion

    #region Non-Serializable Variables
    private AudioManager m_audioManager;
    private CameraController m_cameraController;
    private uint m_currentHealth;
    private float m_currentInvulnerabilityTime;
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods

    void Start()
    {
        m_currentHealth = m_maxHealth;
        m_HeartsHealthVisual.InitHealth(m_currentHealth);
        m_cameraController = Camera.main.GetComponent<CameraController>();
        m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_hitFlash = GetComponent<HitFlash>();
        m_playerCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (m_currentInvulnerabilityTime > 0)
        {
            m_currentInvulnerabilityTime = Mathf.Max(0, m_currentInvulnerabilityTime - Time.deltaTime);
            
        }

        if (m_currentInvulnerabilityTime == 0)
            if (!m_playerCollider.enabled)
                m_playerCollider.enabled = true;
    }

    void LateUpdate()
    {
        if (m_currentHealth == 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //ENEMY PROJECTILES
        if (col.gameObject.layer == 14)
        {
            if (m_currentInvulnerabilityTime <= 0)
            {
                if (col.gameObject.GetComponent<EnemyProjectileController>())
                {
                    TakeDamage(col.gameObject.GetComponent<EnemyProjectileController>().GetDamage());
                    Destroy(col.gameObject);
                }

                if (col.gameObject.GetComponent<EvilProjectileController>())
                {
                    if (col.gameObject.GetComponent<EvilProjectileController>().GetDamage() > 0)
                    {
                        TakeDamage(col.gameObject.GetComponent<EvilProjectileController>().GetDamage());
                        Destroy(col.gameObject);
                    }
                    else
                    {
                        col.gameObject.GetComponent<EvilProjectileController>().CustomProjectileStart();
                        Destroy(col.gameObject);
                    }
                }
            }
        }

        //PLAYER AREA DAMAGE
        if (col.gameObject.layer == 18)
        {
            if (m_currentInvulnerabilityTime <= 0)
            {
                TakeDamage(3);
            }
        }

        //ENEMY AREA DAMAGE
        if (col.gameObject.layer == 19)
        {
            if (m_currentInvulnerabilityTime <= 0)
            {
                TakeDamage(3);
            }
        }

        //PICKABLE
        if (col.gameObject.layer == 9)
        {
            m_audioManager.Play("SFX", "Pickup");
            if (col.gameObject.CompareTag("HealingPickup"))
            {
                if (col.gameObject.GetComponent<HealingPickup>())
                    Heal(col.gameObject.GetComponent<HealingPickup>().amount);
            }
            Destroy(col.gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (m_currentInvulnerabilityTime <= 0)
            {
                TakeDamage(col.gameObject.GetComponent<EnemyMovement>().touchDamage);
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 14)
        {
            if (m_currentInvulnerabilityTime <= 0)
            {
                TakeDamage(1);
            }
        }
    }

    #endregion

    #region Custom Methods

    void TakeDamage(uint damage)
    {
        if (m_currentHealth <= damage) 
            m_currentHealth = 0;
        else 
            m_currentHealth -= damage;

        m_currentInvulnerabilityTime = m_invulnerabilityTime;
        m_hitFlash.Flash(m_hitFlashTime);
        m_audioManager.Play("SFX", "HurtPlayer");
        m_HeartsHealthVisual.UpdateHealth(m_currentHealth);
        m_cameraController.Shake();
        m_playerCollider.enabled = false;
    }

    void Heal(uint amount)
    {
        m_currentHealth += amount;
        m_HeartsHealthVisual.UpdateHealth(m_currentHealth);
    }

    private void Die()
    {
        GameObject deathAnimation = Instantiate(Instantiate(m_deathAnimation, transform.position, Quaternion.identity));
        m_audioManager.Play("SFX", "EvilLaugh");
        m_audioManager.Stop("Music", 3f);
        m_cameraController.transform.parent.GetComponent<SlowFollow>().followSpeed = 0.01f;
        m_cameraController.transform.parent.GetComponent<SlowFollow>().followTransform = deathAnimation.transform.GetChild(0);
        if (transform.GetChild(0).childCount > 0)
            PlayerPrefs.SetInt(transform.GetChild(0).GetChild(0).GetComponent<AbstractSpell>().evilName, 1);
        GameObject.Find("Load Manager").GetComponent<LoadManager>().LoadNextLevel(4);
        int currentScore = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().GetScore();
        if (currentScore > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", currentScore);
        }
        PlayerPrefs.SetInt("played", 1);
        Destroy(gameObject);
    }

    #endregion
}
