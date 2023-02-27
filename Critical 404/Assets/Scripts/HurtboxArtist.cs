using System.Collections;

public abstract class HurtboxArtist
{
    public abstract IEnumerator DrawIdle(bool facingLeft);
    public abstract IEnumerator DrawCrouch(bool facingLeft);
    public abstract IEnumerator DrawMoveForward(bool facingLeft);
    public abstract IEnumerator DrawMoveBackward(bool facingLeft);
    public abstract IEnumerator DrawJumpRise(bool facingLeft);
    public abstract IEnumerator DrawJumpFall(bool facingLeft);
    public abstract IEnumerator DrawSLP(bool facingLeft);
    public abstract IEnumerator DrawSHP(bool facingLeft);
    public abstract IEnumerator DrawSLK(bool facingLeft);
    public abstract IEnumerator DrawSHK(bool facingLeft);
}