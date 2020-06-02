using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Variables

    #region Components
    private Rigidbody2D m_rigidbody2D;
    #endregion

    #region Private Variables
    [SerializeField] private float m_initialMoveSpeed;
    private Vector2 xyinput;
    #endregion

    #region Public Variables
    public float m_currentMoveSpeed;
    #endregion

    #endregion

    #region BuiltIn Methods

    void Start()
    {
        m_currentMoveSpeed = m_initialMoveSpeed;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region Custom Methods

    Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void Move()
    {
        xyinput = GetInput();
        m_rigidbody2D.velocity = xyinput * m_currentMoveSpeed * Time.deltaTime;
    }

    #endregion
}
