using UnityEngine;
using System.Collections;

public class AnimationAutoDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 0f;

    void Start()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
