using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _startCount = 20;

    private Queue<Cube> _freeCubes = new Queue<Cube>();

    private void Awake()
    {
        for (int i = 0; i < _startCount; i++)
        {
            Cube cube = Instantiate(_cubePrefab);

            cube.gameObject.SetActive(false);

            _freeCubes.Enqueue(cube);
        }
    }

    public Cube Get() 
    {
        Cube pooledCube = _freeCubes.Dequeue();

        pooledCube.gameObject.SetActive(true);

        return pooledCube;
    }

    public void Release(Cube cube) 
    {
        cube.gameObject.SetActive(false);

        _freeCubes.Enqueue(cube);
    }
}
