using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] CubePool _pool;
    [SerializeField] private float _spawnPositionX = 4f;
    [SerializeField] private float _spawnPositionZ = 4f;
    [SerializeField] private float _spawnPositionY = 5f;

    public Cube Spawn() 
    {
        Cube cube = _pool.Get();

        float randomPositionX = Random.Range(-_spawnPositionX, _spawnPositionX);
        float randomPositionZ = Random.Range(-_spawnPositionZ, _spawnPositionZ);

        cube.transform.position = new Vector3(randomPositionX, _spawnPositionY, randomPositionZ);

        return cube;
    }
}