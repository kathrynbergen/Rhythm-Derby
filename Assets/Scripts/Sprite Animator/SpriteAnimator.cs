using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    [SerializeField] protected Transform spriteTransform;
    [SerializeField] protected Sprite IdleSprite;
    
    public void ChangeToIdleSprite()
    {
        SpriteRenderer.sprite = IdleSprite;
    }

    public void SwapSides()
    {
        // change position of batter and pitcher sprites 
        if (transform.position.x < 0) 
        {
            transform.position = new Vector3(6f, 0);
        }
        else
        {
            transform.position = new Vector3(-6f, 0);
        }
        SpriteRenderer.flipX = !SpriteRenderer.flipX; // flips the way they're facing
    }
}
