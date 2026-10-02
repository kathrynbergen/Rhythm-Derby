using UnityEngine;

public class PitcherSpriteAnimator : MonoBehaviour
{
    public SpriteRenderer PitcherSpriteRenderer;
    public Sprite WindupSprite;
    public Sprite IdleSprite;
    public Sprite ThrowingSprite;
    

    public void ChangeToWindupSprite()
    {
        PitcherSpriteRenderer.sprite = WindupSprite;
    }
    
    public void ChangeToIdleSprite()
    {
        PitcherSpriteRenderer.sprite = IdleSprite;
    }
    
    public void ChangeToThrowingSprite()
    {
        PitcherSpriteRenderer.sprite = ThrowingSprite;
    }
}
