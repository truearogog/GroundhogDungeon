using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    #region Variables

    #region Components
    private Rigidbody2D m_rigidbody2D;
    private Animator m_animator;
    #endregion

    #region Private Variables
    [SerializeField] private Transform m_staffTransform;
    private Camera m_mainCamera;
    private Vector3 m_mousePos;
    #endregion

    #region Public Variables
    #endregion

    #endregion

    #region BuiltIn Methods

    void Awake()
    {
        m_mainCamera = Camera.main;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        m_mousePos = GetMouseWorldPos();
        UpdateAnimator();
    }

    #endregion

    #region Custom Methods

    void UpdateAnimator()
    {
        m_animator.SetFloat("moveSpeed", m_rigidbody2D.velocity.magnitude);

        Vector2 sign = m_mousePos - m_staffTransform.position;

        m_animator.SetFloat("ySign", sign.y);

        if ((sign.x > 0 && m_rigidbody2D.velocity.x < 0) || (sign.x < 0 && m_rigidbody2D.velocity.x > 0))
            m_animator.SetFloat("xSign", -1f);
        else
            m_animator.SetFloat("xSign", 1f);
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    #endregion
}
