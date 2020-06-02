using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawn : MonoBehaviour
{
    [SerializeField] private GameObject EvilFireBall;
    [SerializeField] private GameObject EvilLightning;
    [SerializeField] private GameObject EvilExplosion;
    public EvilSpawnManager outEvil;

    void Start()
    {
        StartCoroutine(DestroySelfSpawnEvil(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));
    }

    private IEnumerator DestroySelfSpawnEvil(float time)
    {
        yield return new WaitForSeconds(time);
        List<GameObject> availableEvils = new List<GameObject>();
        if (PlayerPrefs.GetInt("EvilFireball") == 1)
        {
            availableEvils.Add(EvilFireBall);
        }
        if (PlayerPrefs.GetInt("EvilLightning") == 1)
        {
            availableEvils.Add(EvilLightning);
        }
        if (PlayerPrefs.GetInt("EvilExplosion") == 1)
        {
            availableEvils.Add(EvilExplosion);
        }

        if (availableEvils.Count > 0)
        {
            GameObject evil = Instantiate(availableEvils[Random.Range(0, availableEvils.Count)], transform.position, Quaternion.identity);
            outEvil.currentEvil = evil;
        }

        Destroy(gameObject);
    }
}
