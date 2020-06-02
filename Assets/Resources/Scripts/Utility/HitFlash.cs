using System.Collections;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private Color m_flashColor;
    private bool isFlashing = false;
    private Color m_spriteColor;
    private SpriteRenderer m_spriteRenderer;

    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Flash(float time)
    {
        if (!isFlashing)
        {
            isFlashing = true;
            m_spriteColor = m_spriteRenderer.color;
            m_spriteRenderer.color = m_flashColor;
            StartCoroutine(SetMaterialBack(time));
        }
    }

    IEnumerator SetMaterialBack(float time)
    {
        yield return new WaitForSeconds(time);
        m_spriteRenderer.color = m_spriteColor;
        isFlashing = false;
    }
}
