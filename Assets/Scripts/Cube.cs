using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool _isActivated;
    private Renderer _renderer;
    private Color _defaultColor = Color.white;

    public Color DefaultColor => _defaultColor;
    public bool IsActivated => _isActivated;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Activate() 
    {
        _isActivated = true;
    }

    public void Deactivate() 
    {
        _isActivated = false;

    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
