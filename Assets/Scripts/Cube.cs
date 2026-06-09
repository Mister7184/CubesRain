using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool _isActivated;
    private Renderer _renderer;
    private Color _defaultColor = Color.white;
    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;
    private Coroutine _lifeRoutine;

    public Action<Cube> LifeTimeEnded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActivated)
            return;

        if (collision.gameObject.TryGetComponent<Platform>(out _) == false)
            return;

        Activate();
    }

    public void Deactivate() 
    {
        _isActivated = false;

        SetColor(_defaultColor);

        if (_lifeRoutine != null) 
        {
            StopCoroutine(_lifeRoutine);
            _lifeRoutine = null;
        }
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void Activate() 
    {
        _isActivated = true;

        SetColor(UnityEngine.Random.ColorHSV());

        float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

        _lifeRoutine = StartCoroutine(ReturnCubeAfterTime(lifeTime));
    }

    private IEnumerator ReturnCubeAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        LifeTimeEnded?.Invoke(this);
    }
}