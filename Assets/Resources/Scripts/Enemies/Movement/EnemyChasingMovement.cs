using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingMovement : EnemyMovement
{
    #region Variables

    #region Private Variables

    #region Serializable
    [SerializeField] private float m_followMinDistance;
    #endregion

    #region Non-Serializable
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    #endregion

    #region Custom Methods

    protected override void Move()
    {
        m_rigidbody2D.velocity = (toPlayer).normalized * ((toPlayer.magnitude > m_followMinDistance) ? 1 : 0) * m_moveSpeed * Time.deltaTime;
    }

    #endregion
}
