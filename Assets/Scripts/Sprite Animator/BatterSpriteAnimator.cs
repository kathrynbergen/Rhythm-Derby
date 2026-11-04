using UnityEngine;

public class BatterSpriteAnimator : SpriteAnimator
{
    [SerializeField] private Sprite doneSwingingSprite;
    [SerializeField] private Sprite swingingSprite;
    
    public void ChangeToSwingingSprite()
    {
        SpriteRenderer.sprite = swingingSprite;
    }
    
    public void ChangeToDoneSwingingSprite()
    {
        SpriteRenderer.sprite = doneSwingingSprite;
    }
}
