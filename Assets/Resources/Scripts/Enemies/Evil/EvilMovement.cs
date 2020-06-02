using System.Collections;
using UnityEngine;

public class EvilMovement : EnemyMovement
{
    #region Variables

    #region Public Variables
    public enum MoveType { 
        chasing, random
    }    
    #endregion

    #region Private Variables

    #region Serializable
    [SerializeField] private float m_followMinDistance;
    [SerializeField] private float m_moveTime;
    [SerializeField] private float m_moveInterval;
    [SerializeField] private LayerMask m_avoidMask;
    [SerializeField] private MoveType m_moveType;
    #endregion

    #region Non-Serializable
    private float toMoveTime;
    private float currentMoveTime = 0;
    private bool isMoving = false;
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods

    protected override void Start()
    {
        base.Start();

        toMoveTime = Random.Range(0, m_moveInterval);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (toMoveTime > 0)
        {
            toMoveTime -= Time.deltaTime;
        }

        if (currentMoveTime > 0)
        {
            currentMoveTime -= Time.deltaTime;
        }

        UpdateAnimator();
    }

    #endregion

    #region Custom Methods

    protected override void Move()
    {
        if (toMoveTime <= 0 && !isMoving)
        {
            if (m_targetPlayer)
            {
                switch (m_moveType)
                {
                    case MoveType.random:
                        StartCoroutine(MoveRandomRoutine(Random.insideUnitCircle.normalized));
                        break;
                    case MoveType.chasing:
                        StartCoroutine(MoveTowardsPlayerRoutine());
                        break;
                }
                isMoving = true;
            }
        }
    }

    private IEnumerator MoveRandomRoutine(Vector2 dir)
    {
        currentMoveTime = m_moveTime;
        while (currentMoveTime > 0)
        {
            yield return new WaitForFixedUpdate();
            while (Physics2D.Linecast(transform.position, (Vector2)transform.position + dir * m_moveSpeed * Time.deltaTime, m_avoidMask))
            {
                dir = Random.insideUnitCircle.normalized;
            }
            m_rigidbody2D.velocity = dir * m_moveSpeed * Time.deltaTime;
            Debug.DrawLine(transform.position, transform.position + (Vector3)m_rigidbody2D.velocity, Color.blue);
        }
        m_rigidbody2D.velocity = Vector2.zero;
        toMoveTime = m_moveInterval;
        isMoving = false;
    }

    private IEnumerator MoveTowardsPlayerRoutine()
    {
        currentMoveTime = m_moveTime;
        while (currentMoveTime > 0)
        {
            yield return new WaitForFixedUpdate();
            if (m_followMinDistance > toPlayer.magnitude)
                break;
            m_rigidbody2D.velocity = toPlayer.normalized * m_moveSpeed * Time.deltaTime;
            Debug.DrawLine(transform.position, transform.position + (Vector3)m_rigidbody2D.velocity, Color.blue);
        }
        m_rigidbody2D.velocity = Vector2.zero;
        toMoveTime = m_moveInterval;
        isMoving = false;
    }

    private void UpdateAnimator()
    {
        m_animator.SetFloat("moveSpeed", m_rigidbody2D.velocity.magnitude);

        if ((m_rigidbody2D.velocity.x > 0 && !m_spriteRenderer.flipX) || (m_rigidbody2D.velocity.x < 0 && m_spriteRenderer.flipX))
            m_animator.SetFloat("xSign", -1);
        else 
            m_animator.SetFloat("xSign", 1);
    }

    #endregion
}
