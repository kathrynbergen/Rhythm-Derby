using System;
using UnityEngine;

public class BallAnimator : MonoBehaviour
{
    public SpriteRenderer BallSpriteRenderer;

    public void Start()
    {
        BallSpriteRenderer.enabled = false;
    }

    public void Vanish()
    {
        BallSpriteRenderer.enabled = false;
    }
    
    public void Appear()
    {
        BallSpriteRenderer.enabled = true;
    }
}
