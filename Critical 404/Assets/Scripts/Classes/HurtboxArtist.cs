using System.Collections;
using UnityEngine;

public abstract class HurtboxArtist
{
    protected HitboxManager hbm;
    protected GameObject hurtboxObject;
    protected GameObject hitboxObject;

    private bool spawnHitboxesThisImage = true;
    private bool stopThisRoutine = false;

    public HurtboxArtist(HitboxManager hbm, GameObject hurtboxObj, GameObject hitboxObj)
    {
        this.hbm = hbm;
        this.hurtboxObject = hurtboxObj;
        this.hitboxObject = hitboxObj;
    }

    public abstract IEnumerator DrawIdle(bool facingRight);
    public abstract IEnumerator DrawCrouch(bool facingRight);
    public abstract IEnumerator DrawMoveForward(bool facingRight);
    public abstract IEnumerator DrawMoveBackward(bool facingRight);
    public abstract IEnumerator DrawJump(bool facingRight);
    public abstract IEnumerator DrawJumpRise(bool facingRight);
    public abstract IEnumerator DrawJumpFall(bool facingRight);

    public abstract IEnumerator DrawStandingBlock(bool facingRight);
    public abstract IEnumerator DrawCrouchingBlock(bool facingRight);
    public abstract IEnumerator DrawJumpingBlock(bool facingRight);

    public abstract IEnumerator DrawSLP(bool facingRight);
    public abstract IEnumerator DrawSHP(bool facingRight);
    public abstract IEnumerator DrawSLK(bool facingRight);
    public abstract IEnumerator DrawSHK(bool facingRight);

    public abstract IEnumerator DrawCLP(bool facingRight);
    public abstract IEnumerator DrawCHP(bool facingRight);
    public abstract IEnumerator DrawCLK(bool facingRight);
    public abstract IEnumerator DrawCHK(bool facingRight);

    public abstract IEnumerator DrawJLP(bool facingRight);
    public abstract IEnumerator DrawJHP(bool facingRight);
    public abstract IEnumerator DrawJLK(bool facingRight);
    public abstract IEnumerator DrawJHK(bool facingRight);

    public abstract IEnumerator DrawHitstun(bool facingRight);

    protected IEnumerator DrawHurtboxAnimation(HurtboxAnimation anim, bool facingRight)
    {
        int flipMultiplier = facingRight ? 1 : -1;
        for (int i = 0; i < anim.frames.Length; i++)
        {
            if (!spawnHitboxesThisImage) spawnHitboxesThisImage = true; // reset once previous image is done
            if (stopThisRoutine)
            {
                stopThisRoutine = false;
                yield break;
            }
            // Draw each hitbox per frame
            HurtboxFrame frame = anim.frames[i];
            foreach (Hurtbox hurtbox in frame.hurtboxes)
            {
                if (hurtbox.GetType() == typeof(Hitbox) && spawnHitboxesThisImage)
                    hbm.CreateHitbox(hitboxObject, (Hitbox)hurtbox, flipMultiplier, anim.frameDurations[i]);
                else
                    hbm.CreateHurtbox(hurtboxObject, hurtbox, flipMultiplier, anim.frameDurations[i]);
            }
            yield return new WaitForSeconds(anim.frameDurations[i] / 60f);
            StopDrawingAll();
        }
    }

    public void PreventHitboxesThisImage()
    {
        spawnHitboxesThisImage = false;
        hbm.ClearHitboxes();
    }

    public void StopCurrentRoutine()
    {
        stopThisRoutine = true;
    }

    public void StopDrawingAll()
    {
        hbm.ClearAll(hurtboxObject);
        hbm.ClearAll(hitboxObject);
    }
}