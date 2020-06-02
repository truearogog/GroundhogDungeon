using UnityEngine;

public class LayerMaskAutoDestroy : MonoBehaviour
{
    [SerializeField] private GameObject m_destroyObject;
    [SerializeField] private LayerMask m_destroyMask;
    [SerializeField] private string m_destroySound;
    private AudioManager m_audioManager;

    void Start()
    {
        if (!m_destroySound.Equals(""))
        {
            m_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (m_destroyMask == (m_destroyMask | (1 << col.gameObject.layer)))
        {
            if (m_audioManager)
                m_audioManager.Play("SFX", m_destroySound);
            Destroy(m_destroyObject);
        }
    }
}
