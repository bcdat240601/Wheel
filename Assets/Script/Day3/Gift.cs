using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : SetupBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        GetSpriteGift();
    }

    protected virtual void GetSpriteGift()
    {
        if (spriteRenderer != null) return;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Debug.Log("Reset " + nameof(spriteRenderer) + " in " + GetType().Name);
    }
    public virtual void SpriteGiftBlack()
    {
        spriteRenderer.color = Color.black;
    }
    public virtual void ResetGift()
    {
        spriteRenderer.color = Color.white;
    }
}
