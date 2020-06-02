using UnityEngine;

public class EvilSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject evilSpawn;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDelay;
    public GameObject currentEvil = null;
    private float spawnTime;
    private EnemySpawnManager enemySpawnManager;

    void Start()
    {
        enemySpawnManager = GameObject.FindGameObjectWithTag("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        spawnTime = spawnDelay;
        if (PlayerPrefs.GetInt("EvilExplosion") + PlayerPrefs.GetInt("EvilLightning") + PlayerPrefs.GetInt("EvilFireball") == 0)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (spawnTime > 0)
        {
            spawnTime -= Time.deltaTime;
        }
        
        if (spawnTime <= 0)
        {
            SpawnEvil();
        }

        if (currentEvil)
        {
            enemySpawnManager.canSpawn = false;
        }
        else
        {
            enemySpawnManager.canSpawn = true;
        }
    }

    void SpawnEvil()
    {
        if (currentEvil != null)
        {
            spawnTime = spawnInterval;
            return;
        }
        EvilSpawn currentEvilSpawn = Instantiate(evilSpawn, transform.position, Quaternion.identity).GetComponent<EvilSpawn>();
        currentEvilSpawn.outEvil = this;
        spawnTime = spawnInterval;
    }

    public void ResetSpawn()
    {
        spawnTime = spawnInterval;
    }
}
