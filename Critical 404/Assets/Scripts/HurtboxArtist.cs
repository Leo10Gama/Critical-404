using System.Collections;

public abstract class HurtboxArtist
{
    public abstract IEnumerator DrawIdle();
    public abstract IEnumerator DrawCrouch();
    public abstract IEnumerator DrawMoveForward();
    public abstract IEnumerator DrawMoveBackward();
    public abstract IEnumerator DrawJumpRise();
    public abstract IEnumerator DrawJumpFall();
    public abstract IEnumerator DrawSLP();
    public abstract IEnumerator DrawSHP();
    public abstract IEnumerator DrawSLK();
    public abstract IEnumerator DrawSHK();
}