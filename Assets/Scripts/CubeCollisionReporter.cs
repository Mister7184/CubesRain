using System;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeCollisionReporter : MonoBehaviour
{
    private Cube _cube;

    public Action<Cube> PlatformTouched;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Platform>() == null)
            return;

        PlatformTouched?.Invoke(_cube);
    }
}
