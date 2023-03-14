using UnityEngine;

public class AttackData
{

    public int damage;
    public int hitstun;
    public int blockstun;

    public Vector2 knockback;   // should be pointing right (positive x)

    public AttackData(int damage, int hitstun, int blockstun, Vector2 knockback)
    {
        this.damage = damage;
        this.hitstun = hitstun;
        this.blockstun = blockstun;
        this.knockback = knockback;
    }

    public AttackData(int damage, int hitstun, int blockstun):
        this(damage, hitstun, blockstun, Vector2.zero)
    {
    }

}
