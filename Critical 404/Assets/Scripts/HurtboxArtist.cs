using System.Collections;

public abstract class HurtboxArtist
{
    public abstract IEnumerator DrawIdle(bool facingRight);
    public abstract IEnumerator DrawCrouch(bool facingRight);
    public abstract IEnumerator DrawMoveForward(bool facingRight);
    public abstract IEnumerator DrawMoveBackward(bool facingRight);
    public abstract IEnumerator DrawJumpRise(bool facingRight);
    public abstract IEnumerator DrawJumpFall(bool facingRight);
    public abstract IEnumerator DrawSLP(bool facingRight);
    public abstract IEnumerator DrawSHP(bool facingRight);
    public abstract IEnumerator DrawSLK(bool facingRight);
    public abstract IEnumerator DrawSHK(bool facingRight);
}