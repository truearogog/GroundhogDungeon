using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    #region Variables

    #region Components
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private Collider2D m_collider2D;
    #endregion

    #region Public Variables

    #endregion

    #region Private Variables
    [SerializeField] private AbstractSpell m_Spell;
    [SerializeField] private Transform m_staffTransform;
    [SerializeField] private DamageManager m_damageManager;
    private Camera m_mainCamera;
    private Vector3 m_mousePos;
    #endregion

    #endregion

    #region BuiltIn Methods

    void Start()
    {
        m_collider2D = GetComponent<Collider2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        m_mousePos = GetMouseWorldPos();
        RotateStaff(GetZRotation());
        CheckForFlipX();

        if (!m_Spell)
            return;

        if (Input.GetMouseButton(0) && m_Spell)
        {
            m_Spell.Shoot();
        }

        if (m_Spell.spellType == AbstractSpell.SpellType.Beam)
        {
            if (Input.GetMouseButtonUp(0))
            { 
                
            }
        }
    }

    //Check for staff pickup
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 12)
        {
            if (m_Spell != null)
                Destroy(m_Spell.gameObject);
            col.transform.SetParent(m_staffTransform);
            col.transform.localPosition = Vector3.zero;
            col.transform.localRotation = Quaternion.identity;
            col.gameObject.transform.localScale = new Vector3(Mathf.Abs(col.transform.localScale.x), Mathf.Abs(col.transform.localScale.y), col.transform.localScale.z);
            col.enabled = false;
            m_Spell = col.gameObject.GetComponent<AbstractSpell>();
            m_Spell.m_isBound = true;
            m_Spell.OnPick();
            m_Spell.SetDamageStats(ref m_damageManager);
        }
    }

    #endregion

    #region Custom Methods
    //VISUAL
    private float GetZRotation()
    {
        Vector3 vector = m_mousePos - m_staffTransform.position;
        Debug.DrawLine(m_staffTransform.position, m_staffTransform.position + Vector3.right * 0.5f, Color.yellow);
        Debug.DrawLine(m_staffTransform.position, m_staffTransform.position + vector.normalized * 0.5f, Color.red);
        return (90f - Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg);
    }

    private void RotateStaff(float zrot)
    {
        float sign = m_mousePos.x - m_staffTransform.position.x;
        //FLIP STAFF BY Y
        if (((sign < 0) && !m_spriteRenderer.flipX) || ((sign > 0) && m_spriteRenderer.flipX))
            m_staffTransform.localScale = new Vector3(m_staffTransform.localScale.x, -m_staffTransform.localScale.y, m_staffTransform.localScale.z);
        m_staffTransform.rotation = Quaternion.Euler(0, 0, zrot);
    }

    void CheckForFlipX()
    {
        //FLIP BY MOUSE
        float sign = m_mousePos.x - m_staffTransform.position.x;
        if (((sign < 0) && !m_spriteRenderer.flipX) || ((sign > 0) && m_spriteRenderer.flipX))
            FlipX();
    }

    void FlipX()
    {
        m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
        m_staffTransform.localPosition = new Vector3(-m_staffTransform.localPosition.x, m_staffTransform.localPosition.y, m_staffTransform.localPosition.z);
        m_collider2D.offset = new Vector2(-m_collider2D.offset.x, m_collider2D.offset.y);
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    #endregion
}
