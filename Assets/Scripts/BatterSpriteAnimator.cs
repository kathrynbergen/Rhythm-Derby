using UnityEngine;

public class BatterSpriteAnimator : MonoBehaviour
{
    public SpriteRenderer BatterSpriteRenderer;
    public Sprite IdleSprite;
    public Sprite DoneSwingingSprite;
    public Sprite SwingingSprite;
    

    public void ChangeToIdleSprite()
    {
        BatterSpriteRenderer.sprite = IdleSprite;
    }
    
    public void ChangeToSwingingSprite()
    {
        BatterSpriteRenderer.sprite = SwingingSprite;
    }
    
    public void ChangeToDoneSwingingSprite()
    {
        BatterSpriteRenderer.sprite = DoneSwingingSprite;
    }
    
    
}
