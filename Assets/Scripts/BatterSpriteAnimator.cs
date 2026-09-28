using UnityEngine;

public class BatterSpriteAnimator : MonoBehaviour
{
    public SpriteRenderer BatterSpriteRenderer;
    public Sprite IdleSprite;
    public Sprite SwingSprite;

    public void ChangeToIdleSprite()
    {
        BatterSpriteRenderer.sprite = IdleSprite;
    }
    
    public void ChangeToSwingSprite()
    {
        BatterSpriteRenderer.sprite = SwingSprite;
    }
    
    
}
