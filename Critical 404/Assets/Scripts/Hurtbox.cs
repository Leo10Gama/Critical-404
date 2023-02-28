using UnityEngine;

/// <summary>
/// This is a class to represent a single Hurtbox on a character in
/// the game. It has an offset (x, y) and scaling (x, y) to instruct
/// other functions as to where on the screen it is.
/// <summary/>
public class Hurtbox
{

    public Vector2 offset;
    public Vector2 scale;

    public Hurtbox(Vector2 offset, Vector2 scale)
    {
        this.offset = offset;
        this.scale = scale;
    }

}