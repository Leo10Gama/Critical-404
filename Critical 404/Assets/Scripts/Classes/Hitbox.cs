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

    public int damage;
    public int hitstun;
    public int blockstun;
    public Vector2 knockback;

    public int activeImage;

    public Hitbox(Vector2 offset, Vector2 scale, int damage, int hitstun, int blockstun, Vector2 knockback, int activeImage)
        : base(offset, scale)
    {
        this.damage = damage;
        this.hitstun = hitstun;
        this.blockstun = blockstun;
        this.knockback = knockback;
        this.activeImage = activeImage;
    }

    public Hitbox(Vector2 offset, Vector2 scale, int damage, int hitstun, int blockstun, Vector2 knockback) 
        : this(offset, scale, damage, hitstun, blockstun, knockback, 0)
    {
    }

    public Hitbox(Vector2 offset, Vector2 scale, int damage, int hitstun, int blockstun) 
        : this(offset, scale, damage, hitstun, blockstun, Vector2.zero, 0)
    {
    }

    public Hitbox(Vector2 offset, Vector2 scale, AttackData attackData)
        : this(offset, scale, attackData.damage, attackData.hitstun, attackData.blockstun, attackData.knockback, 0)
    {
    }

    public Hitbox(Vector2 offset, Vector2 scale, AttackData attackData, int activeImage)
        : this(offset, scale, attackData.damage, attackData.hitstun, attackData.blockstun, attackData.knockback, activeImage)
    {
    }

}