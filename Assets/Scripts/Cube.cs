using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _isActivated;
    private Renderer _renderer;
    private Color _defaultColor = Color.white;
    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;

    public Action<Cube> LifeTimeEnded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActivated)
            return;

        if (collision.gameObject.GetComponent<Platform>() == null)
            return;

        Activate();
    }

    public void Deactivate() 
    {
        _isActivated = false;

        SetColor(_defaultColor);
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

        StartCoroutine(ReturnCubeAfterTime(lifeTime));
    }

    private IEnumerator ReturnCubeAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        LifeTimeEnded?.Invoke(this);
    }
}