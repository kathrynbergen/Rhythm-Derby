using UnityEngine;

public class PitcherSpriteAnimator : SpriteAnimator
{
    [SerializeField] private Sprite windupSprite;
    [SerializeField] private Sprite throwingSprite;

    public void ChangeToWindupSprite()
    {
        SpriteRenderer.sprite = windupSprite;
    }
    
    public void ChangeToThrowingSprite()
    {
        SpriteRenderer.sprite = throwingSprite;
    }
}
