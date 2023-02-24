using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtboxArtist : HurtboxArtist
{

    private HitboxManager hbm;
    private GameObject hurtboxObject;

    public PlayerHurtboxArtist(HitboxManager hbm, GameObject hbo)
    {
        this.hbm = hbm;
        this.hurtboxObject = hbo;
    }

    public override IEnumerator DrawIdle()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawCrouch()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawMoveForward()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawMoveBackward()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawJumpRise()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawJumpFall()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawSLP()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawSHP()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawSLK()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawSHK()
    {
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

}
