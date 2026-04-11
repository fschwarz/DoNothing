using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PointerSpriteSwapper : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite hoverSprite;
    public Sprite tapSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Grab the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite just in case it wasn't set in the editor
        if (hoverSprite != null)
        {
            spriteRenderer.sprite = hoverSprite;
        }
    }

    void Update()
    {
        // 0 represents the Left Mouse Button
        if (Input.GetMouseButtonDown(0))
        {
            // Changed to clicked sprite the exact frame the mouse goes down
            spriteRenderer.sprite = tapSprite;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Revert to default sprite the exact frame the mouse is released
            spriteRenderer.sprite = hoverSprite;
        }
    }
}