using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawnReset : MonoBehaviour
{
    private EvilSpawnManager evilSpawnManager;

    void Start()
    {
        evilSpawnManager = GameObject.FindGameObjectWithTag("EvilSpawnManager").GetComponent<EvilSpawnManager>();
    }

    public void ResetSpawn()
    {
        evilSpawnManager.ResetSpawn();
    }
}
