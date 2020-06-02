using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyMovement : MonoBehaviour
{
    #region Variables

    #region Components
    protected Rigidbody2D m_rigidbody2D;
    protected Collider2D m_collider2D;
    protected SpriteRenderer m_spriteRenderer;
    protected Animator m_animator;
    #endregion

    #region Public Variables
    public GameObject targetPlayer { get { return m_targetPlayer; } set { m_targetPlayer = value; } }
    public uint touchDamage { get { return m_touchDamage; } set { m_touchDamage = value; } }
    public bool m_isMoving = true;
    public Vector2 toPlayer;
    #endregion

    #region Private Variables

    #region Serializable
    [SerializeField] private uint m_touchDamage;
    #endregion

    #region Non-Serializable

    #endregion

    #endregion

    #region Protected Variables
    [SerializeField] protected float m_moveSpeed;
    [SerializeField] protected GameObject m_targetPlayer;
    #endregion

    #endregion

    #region BuiltIn Methods

    protected virtual void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_collider2D = GetComponent<Collider2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void FixedUpdate()
    {
        if (m_targetPlayer)
        {
            toPlayer = ((Vector2)m_targetPlayer.transform.position - (Vector2)transform.position);
            if (m_isMoving)
            {
                Move();
                CheckForFlipX();
                ChangeYSign();
            }
        }
        else
        {
            m_rigidbody2D.velocity = Vector2.zero;
        }
    }

    #endregion

    #region Custom Methods

    protected abstract void Move();

    protected virtual void FlipX()
    {
        m_collider2D.offset = new Vector2(-m_collider2D.offset.x, m_collider2D.offset.y);
        m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
    }
    private void CheckForFlipX()
    {
        float sign = m_targetPlayer.transform.position.x - transform.position.x;
        if (((sign > 0) && !m_spriteRenderer.flipX) || ((sign < 0) && m_spriteRenderer.flipX))
        {
            FlipX();
        }
    }

    private void ChangeYSign()
    {
        m_animator.SetFloat("ySign", m_targetPlayer.transform.position.y - transform.position.y);
    }

    #endregion
}
