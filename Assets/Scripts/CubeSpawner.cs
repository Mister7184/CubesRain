using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] CubePool _pool;
    [SerializeField] private float _spawnPositionX = 4f;
    [SerializeField] private float _spawnPositionZ = 4f;
    [SerializeField] private float _spawnPositionY = 5f;

    private float _spawnInterval = 1f;
    private WaitForSeconds _spawnDelay;
    private bool _isWork = true;

    private void Start()
    {
        _spawnDelay = new WaitForSeconds(_spawnInterval);

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isWork)
        {
            Spawn();

            yield return _spawnDelay;
        }
    }

    public void Spawn() 
    {
        Cube cube = _pool.Get();

        cube.Deactivate();

        float randomPositionX = Random.Range(-_spawnPositionX, _spawnPositionX);
        float randomPositionZ = Random.Range(-_spawnPositionZ, _spawnPositionZ);

        cube.transform.position = new Vector3(randomPositionX, _spawnPositionY, randomPositionZ);

        cube.LifeTimeEnded -= OnCubeLifeTimeEnded;
        cube.LifeTimeEnded += OnCubeLifeTimeEnded;
    }

    private void OnCubeLifeTimeEnded(Cube cube) 
    {
        cube.LifeTimeEnded -= OnCubeLifeTimeEnded;

        _pool.Release(cube);
    }
}