using UnityEngine;

public class AttackData
{
    private const int DEFAULT_HITSTOP = 3;

    public int damage;
    public int hitstun;
    public int blockstun;
    public int hitstop;

    public Vector2 knockback;   // should be pointing right (positive x)
    public BlockState hitsAt;   // what block-type can block it

    public AttackData(int damage, int hitstun, int blockstun, int hitstop, Vector2 knockback, BlockState hitsAt)
    {
        this.damage = damage;
        this.hitstun = hitstun;
        this.blockstun = blockstun;
        this.hitstop = hitstop;
        this.knockback = knockback;
        this.hitsAt = hitsAt;
    }

    public AttackData(int damage, int hitstun, int blockstun, Vector2 knockback, BlockState hitsAt):
        this(damage, hitstun, blockstun, DEFAULT_HITSTOP, knockback, hitsAt)
    {
    }

    public AttackData(int damage, int hitstun, int blockstun, int hitstop, Vector2 knockback):
        this(damage, hitstun, blockstun, hitstop, knockback, BlockState.mid)
    {
    }

    public AttackData(int damage, int hitstun, int blockstun, Vector2 knockback):
        this(damage, hitstun, blockstun, DEFAULT_HITSTOP, knockback, BlockState.mid)
    {
    }

    public AttackData(int damage, int hitstun, int blockstun):
        this(damage, hitstun, blockstun, DEFAULT_HITSTOP, Vector2.zero, BlockState.mid)
    {
    }

}
