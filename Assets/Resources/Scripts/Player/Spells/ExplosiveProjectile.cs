using System.Collections;
using UnityEngine;

public class ExplosiveProjectile : CustomProjectile
{
    [SerializeField] private GameObject m_areaDamagePrefab;

    public override void Init(Transform parent)
    {
        GameObject areaDamage = Instantiate(m_areaDamagePrefab, parent.position, parent.rotation);
        Camera.main.GetComponent<CameraController>().Shake();
        StartCoroutine(RemoveTrigger());
    }

    private IEnumerator RemoveTrigger()
    {
        yield return new WaitForSeconds(Time.maximumDeltaTime*2);
        if (m_areaDamagePrefab.GetComponent<Collider2D>())
        {
            m_areaDamagePrefab.GetComponent<Collider2D>().enabled = false;
        }
    }
}
