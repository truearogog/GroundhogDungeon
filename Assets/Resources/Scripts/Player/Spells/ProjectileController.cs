using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region Variables

    #region Components
    private Rigidbody2D m_rigidbody2D;
    #endregion

    #region Private Variables
    [SerializeField] private string m_destroySound;
    [SerializeField] private LayerMask m_destroyMask;
    [SerializeField] private CustomProjectile m_customProjectileScript;
    
    private float m_currentSpeed;
    private float m_addSpeedOverTime;
    private AnimationCurve m_moveCurve;
    private AudioManager m_audioManager;
    #endregion

    #endregion

    #region BuiltIn Methods

    void Start()
    {
        m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        m_currentSpeed += m_addSpeedOverTime * Time.deltaTime;
        m_rigidbody2D.velocity = transform.right * m_currentSpeed * Time.deltaTime + transform.up * m_moveCurve.Evaluate(Time.time % m_moveCurve.length);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (m_destroyMask == (m_destroyMask | (1 << col.gameObject.layer)))
        {
            if (m_customProjectileScript)
                m_customProjectileScript.Init(transform);
            if (m_audioManager)
                m_audioManager.Play("SFX", m_destroySound);
            Destroy(gameObject);
        }
    }

    #endregion

    #region Custom Methods

    public void Init(float currentSpeed, float addSpeedOverTime, AnimationCurve moveCurve)
    {
        m_currentSpeed = currentSpeed;
        m_addSpeedOverTime = addSpeedOverTime;
        m_moveCurve = moveCurve;
    }

    public void CustomProjectileStart()
    {
        if (m_customProjectileScript)
        {
            m_customProjectileScript.Init(transform);
        }
    }

    #endregion
}
