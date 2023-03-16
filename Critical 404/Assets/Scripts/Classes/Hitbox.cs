using UnityEngine;

/// <summary>
/// This is a class to represent a single Hitbox on a character in
/// the game. It has an offset (x, y) and scaling (x, y) to instruct
/// other functions as to where on the screen it is, like a Hurtbox,
/// though it also has values for damage to deal, as well as how
/// many frames of hitstun to lock the enemy in if they collide.
/// <summary/>
public class Hitbox : Hurtbox
{

    public AttackData attackData;

    public int activeImage;

    public Hitbox(Vector2 offset, Vector2 scale, AttackData attackData, int activeImage)
        : base(offset, scale)
    {
        this.attackData = attackData;
        this.activeImage = activeImage;
    }

    public Hitbox(Vector2 offset, Vector2 scale, AttackData attackData)
        : this(offset, scale, attackData, 0)
    {
    }

}