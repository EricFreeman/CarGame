using UnityEngine;

public class ToggleRenderOffScreen : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        _spriteRenderer.enabled = Vector3.Distance(Camera.main.transform.position, transform.position) <= 70;
    }
}