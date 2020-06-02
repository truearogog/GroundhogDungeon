using System.Collections;
using UnityEngine;

public class EnemyShootingController : MonoBehaviour
{
    #region Variables

    #region Components
    private Animator m_animator;
    private EnemyMovement m_movement;
    #endregion

    #region Public Variables
    #endregion

    #region Private Variables

    #region Serializable Variables
    [SerializeField] private uint m_damage;
    [SerializeField] private float m_projectileSpeed;
    [SerializeField] private float m_projectileAddSpeedOverTime;
    [SerializeField] private LayerMask m_cantSeeBehind;
    [SerializeField] private float m_attackSpeed;
    [SerializeField] private GameObject m_projectilePrefab;
    #endregion

    #region Non-Serializable Variables
    private float m_shootTime = 0;
    private Transform m_playerTransform;
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods

    void Start()
    {
        m_movement = GetComponent<EnemyMovement>();
        m_animator = GetComponent<Animator>();
        m_shootTime = Random.Range(0, m_attackSpeed*3);
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (m_shootTime > 0)
        {
            m_shootTime -= Time.deltaTime;
        }

        if (m_movement.m_isMoving)
        {
            if (m_playerTransform)
            {
                if (CheckForPlayerVisibility() && m_shootTime <= 0)
                {
                    StartAttackAnimation();
                }
            }
        }
    }

    #endregion

    #region Custom Methods

    private bool CheckForPlayerVisibility()
    {
        return !(Physics2D.Linecast(transform.position, m_playerTransform.position, m_cantSeeBehind));
    }

    private void StartAttackAnimation()
    {
        m_animator.SetTrigger("Shoot");
        m_movement.m_isMoving = false;
        StartCoroutine(ResetMovement(m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
    }

    public void ShootProjectile()
    {
        if (m_playerTransform)
        {
            Vector3 dir = (m_playerTransform.position - transform.position).normalized;
            GameObject projectile = Instantiate(m_projectilePrefab, transform.position + dir * 0.2f, Quaternion.identity);
            projectile.GetComponent<EnemyProjectileController>().Init(m_damage, dir, m_projectileSpeed, m_projectileAddSpeedOverTime);
        }
    }

    private IEnumerator ResetMovement(float time)
    {
        yield return new WaitForSeconds(time);
        m_shootTime = m_attackSpeed;
        m_movement.m_isMoving = true;
    }

    #endregion
}
