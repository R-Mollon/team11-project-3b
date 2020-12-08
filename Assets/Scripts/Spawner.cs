using UnityEngine;

public class Spawner : MonoBehaviour
{

    float _nextSpawnTime;

    [SerializeField] float _delay = 2f;
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform[] _spawnPoints;

    void Update()
    {
        if (ShouldSpawn())
            Spawn();
    }
    void Spawn()
    {
        _nextSpawnTime = Time.time + _delay;

        int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        var spawnPoint = _spawnPoints[randomIndex];
        Instantiate(_prefab, spawnPoint.position, spawnPoint.rotation);
    }
    bool ShouldSpawn()
    {
        return Time.time >= _nextSpawnTime;
    }
}
