using System.Collections;
using UnityEngine;

public class Handler : MonoBehaviour
{
    [SerializeField] CubePool _pool;
    [SerializeField] CubeSpawner _spawner;

    private CubeCollisionReporter _collisionReporter;
    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;
    private float _spawnInterval = 1f;
    private bool _isWork = true;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine() 
    {
        while (_isWork) 
        {
            Cube cube = _spawner.Spawn();

            CubeCollisionReporter collisionReporter = cube.GetComponent<CubeCollisionReporter>();

            collisionReporter.PlatfomTouched -= OnCubeTouched;
            collisionReporter.PlatfomTouched += OnCubeTouched;

            cube.SetColor(cube.DefaultColor);

            cube.Deactivate();

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void OnCubeTouched(Cube cube) 
    {
        if (cube.IsActivated)
            return;

        cube.Activate();

        cube.SetColor(Random.ColorHSV());

        float lifeTime = Random.Range(_minLifeTime, _maxLifeTime);

        StartCoroutine(ReturnCubeAfterTime(cube, lifeTime));
    }

    private IEnumerator ReturnCubeAfterTime(Cube cube, float delay) 
    {
        yield return new WaitForSeconds(delay);

        _pool.Release(cube);
    }
}
