using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAutoDestroy : MonoBehaviour
{
    [SerializeField] private float m_destroyTime;

    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(m_destroyTime);
        Destroy(gameObject);
    }
}
