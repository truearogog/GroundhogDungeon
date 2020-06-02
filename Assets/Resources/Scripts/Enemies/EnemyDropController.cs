using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropController : MonoBehaviour
{
    [SerializeField] private GameObject[] dropArray;
    [SerializeField] private int dropChance;

    public void Drop()
    {
        if (Random.Range(0, 99) < dropChance)
        {
            Instantiate(dropArray[Random.Range(0, dropArray.Length)], transform.position, Quaternion.identity);
        }
    }
}
