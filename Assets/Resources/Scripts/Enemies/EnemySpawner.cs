using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Variables

    #region Components

    #endregion

    #region Public Variables
    
    #endregion

    #region Private Variables

    #region Serializable Variables
    [SerializeField] private Vector2 m_topLeftMoveBorder;
    [SerializeField] private Vector2 m_bottomRightMoveBorder;
    [Space]
    [SerializeField] private float m_minimumDistanceToPlayer;
    [Space]
    [SerializeField] private float m_spawnRadius;
    [Space]
    [SerializeField] private List<GameObject> m_enemyPrefabs;
    [Space]
    [SerializeField] private float m_spawnDelay;
    [SerializeField] private float m_spawnSpeed;
    [SerializeField] private float m_minimumSpawnSpeed;
    [SerializeField] private float m_spawnCount;
    #endregion

    #region Non-Serializable Variables
    private GameObject m_player;
    private float m_currentSpawnSpeed;
    private float m_spawnTime = 0;
    private float m_currentSpawnCount;
    private EnemySpawnManager enemySpawnManager;
    #endregion

    #endregion

    #endregion

    #region BuiltIn Methods

    void Start()
    {
        m_spawnTime = m_spawnDelay;
        m_currentSpawnSpeed = m_spawnSpeed;
        m_currentSpawnCount = m_spawnCount;
        m_player = GameObject.FindGameObjectWithTag("Player");
        enemySpawnManager = GameObject.FindGameObjectWithTag("EnemySpawnManager").GetComponent<EnemySpawnManager>();
    }

    void FixedUpdate()
    {
        if (m_spawnTime > 0)
        {
            m_spawnTime -= Time.deltaTime;
        }
        else
        {
            if (m_player)
            {
                Move();
                SpawnEnemy(Mathf.FloorToInt(m_currentSpawnCount));
                ModifySpawnVariables();
            }
        }
    }

    #endregion

    #region Custom Methods

    void SpawnEnemy(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            if (enemySpawnManager.currentEnemyCount < enemySpawnManager.maxEnemyCount && enemySpawnManager.canSpawn)
            {
                GameObject enemy = Instantiate(m_enemyPrefabs[Random.Range(0, m_enemyPrefabs.Count)], transform.position + (Vector3)(Random.insideUnitCircle * m_spawnRadius), Quaternion.identity);
                enemy.GetComponent<EnemyGetDamage>().enemySpawnManager = enemySpawnManager;
                enemySpawnManager.currentEnemyCount += 1;
            }
        }
        m_spawnTime = m_currentSpawnSpeed;
    }

    void Move()
    {
        do
        {
            transform.position = new Vector3(Random.Range(m_topLeftMoveBorder.x, m_bottomRightMoveBorder.x), Random.Range(m_topLeftMoveBorder.y, m_bottomRightMoveBorder.y), transform.position.z);
        }
        while ((transform.position - m_player.transform.position).magnitude < m_minimumDistanceToPlayer);
    }

    void ModifySpawnVariables()
    { 
        
    }

    #endregion
}
