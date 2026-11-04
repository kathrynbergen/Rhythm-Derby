using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    [SerializeField] protected Sprite IdleSprite;
    
    public void ChangeToIdleSprite()
    {
        SpriteRenderer.sprite = IdleSprite;
    }

    public void SwapSides()
    {
        // change position of sprites 
        transform.position = new Vector3(-1 * transform.position.x, 0);
        SpriteRenderer.flipX = !SpriteRenderer.flipX; // flips the way they're facing
    }
}
