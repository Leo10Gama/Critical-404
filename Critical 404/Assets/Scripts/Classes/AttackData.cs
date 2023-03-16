using UnityEngine;

public class AttackData
{

    public int damage;
    public int hitstun;
    public int blockstun;

    public Vector2 knockback;   // should be pointing right (positive x)
    public BlockState hitsAt;   // what block-type can block it

    public AttackData(int damage, int hitstun, int blockstun, Vector2 knockback, BlockState hitsAt)
    {
        this.damage = damage;
        this.hitstun = hitstun;
        this.blockstun = blockstun;
        this.knockback = knockback;
        this.hitsAt = hitsAt;
    }

    public AttackData(int damage, int hitstun, int blockstun, Vector2 knockback):
        this(damage, hitstun, blockstun, knockback, BlockState.mid)
    {
    }

    public AttackData(int damage, int hitstun, int blockstun):
        this(damage, hitstun, blockstun, Vector2.zero, BlockState.mid)
    {
    }

}
