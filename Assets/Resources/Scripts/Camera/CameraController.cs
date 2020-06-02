using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Shake()
    {
        m_animator.SetTrigger("shake");
    }
}
