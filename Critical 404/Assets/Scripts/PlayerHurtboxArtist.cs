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

    // hello and welcome to ✨magic number hell✨

    public override IEnumerator DrawIdle(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(  // head
            hurtboxObject,
            new Vector2(-0.06859541f * flipMultiplier, 0.7585843f),
            new Vector2(0.8143892f, 0.838599f),
            1
        );
        hbm.CreateHurtbox(  // body
            hurtboxObject,
            new Vector2(-0.1049104f * flipMultiplier, -0.05649029f),
            new Vector2(0.241416f, 0.7901788f),
            1
        );
        hbm.CreateHurtbox(  // back arm
            hurtboxObject,
            new Vector2(-0.4196424f * flipMultiplier, 0.008070052f),
            new Vector2(0.3866768f, 0.6610581f),
            1
        );
        hbm.CreateHurtbox(  // front arm
            hurtboxObject,
            new Vector2(0.3349068f * flipMultiplier, 0.01614013f),
            new Vector2(0.636848f, 0.3866765f),
            1
        );
        hbm.CreateHurtbox(  // legs
            hurtboxObject,
            new Vector2(-0.04842019f * flipMultiplier, -0.9441953f),
            new Vector2(0.6610579f, 0.6610579f),
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawCrouch(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(  // head
            hurtboxObject,
            new Vector2(0.02824545f * flipMultiplier, 0.1210507f),
            new Vector2(0.6852684f, 0.7094784f), 
            1
        );
        hbm.CreateHurtbox(  // upper body
            hurtboxObject,
            new Vector2(-0.09280515f * flipMultiplier, -0.4236773f),
            new Vector2(1.02421f, 0.3947467f), 
            1
        );
        hbm.CreateHurtbox(  // lower body
            hurtboxObject,
            new Vector2(-0.1452603f * flipMultiplier, -0.9764754f),
            new Vector2(0.9193001f, 0.5964978f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawMoveForward(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(  // head
            hurtboxObject,
            new Vector2(0.3026271f * flipMultiplier, 0.6899888f),
            new Vector2(0.8305302f, 0.8143889f), 
            1
        );
        hbm.CreateHurtbox(  // arms
            hurtboxObject,
            new Vector2(0.1896462f * flipMultiplier, 0.07666548f),
            new Vector2(1.395433f, 0.3624663f), 
            1
        );
        hbm.CreateHurtbox(  // torso and front leg
            hurtboxObject,
            new Vector2(0.05649042f * flipMultiplier, -0.7182339f),
            new Vector2(0.3221169f, 1.11298f), 
            1
        );
        hbm.CreateHurtbox(  // back leg
            hurtboxObject,
            new Vector2(-0.4922724f * flipMultiplier, -0.6859536f),
            new Vector2(0.6933393f, 0.3059762f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawMoveBackward(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(  // head
            hurtboxObject,
            new Vector2(-0.3510463f * flipMultiplier, 0.6859539f),
            new Vector2(0.9112306f, 0.9354396f), 
            1
        );
        hbm.CreateHurtbox(  // arms
            hurtboxObject,
            new Vector2(-0.2259607f * flipMultiplier, -0.02017507f),
            new Vector2(1.112982f, 0.459307f), 
            1
        );
        hbm.CreateHurtbox(  // torso
            hurtboxObject,
            new Vector2(-0.1614001f * flipMultiplier, -0.4599925f),
            new Vector2(0.3543973f, 0.3705365f), 
            1
        );
        hbm.CreateHurtbox(  // legs
            hurtboxObject,
            new Vector2(-0.1735051f * flipMultiplier, -0.9482303f),
            new Vector2(0.7659698f, 0.620708f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawJumpRise(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(  // top half
            hurtboxObject,
            new Vector2(0.08070135f * flipMultiplier, 0.6415685f),
            new Vector2(1.000001f, 1.02421f), 
            1
        );
        hbm.CreateHurtbox(  // bottom half
            hurtboxObject,
            new Vector2(-0.1735051f * flipMultiplier, -0.536658f),
            new Vector2(0.7821097f, 1.250172f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawJumpFall(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(  // upper body
            hurtboxObject,
            new Vector2(-0.04438424f * flipMultiplier, 0.6940238f),
            new Vector2(1.169472f, 0.8063192f), 
            1
        );
        hbm.CreateHurtbox(  // torso
            hurtboxObject,
            new Vector2(0.05649114f * flipMultiplier, -0.1008756f),
            new Vector2(0.2575574f, 0.5238677f), 
            1
        );
        hbm.CreateHurtbox(  // legs
            hurtboxObject,
            new Vector2(0.2461371f * flipMultiplier, -0.7182341f),
            new Vector2(0.5884295f, 0.6449184f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawSLP(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawSHP(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawSLK(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

    public override IEnumerator DrawSHK(bool facingLeft)
    {
        int flipMultiplier = facingLeft ? -1 : 1;
        hbm.CreateHurtbox(
            hurtboxObject,
            new Vector2(0f, 0f),
            new Vector2(1f, 1f), 
            1
        );
        yield return new WaitForSeconds(1f / 60f);
    }

}
