using System.Collections;

public abstract class HurtboxArtist
{
    public abstract IEnumerator DrawIdle(bool facingRight);
    public abstract IEnumerator DrawCrouch(bool facingRight);
    public abstract IEnumerator DrawMoveForward(bool facingRight);
    public abstract IEnumerator DrawMoveBackward(bool facingRight);
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

    public abstract void StopDrawingAll();
}