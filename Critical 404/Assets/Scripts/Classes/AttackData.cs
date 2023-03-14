using System.Collections;
using System.Collections.Generic;

public class AttackData
{

    public int damage;
    public int hitstun;
    public int blockstun;

    public AttackData(int damage, int hitstun, int blockstun)
    {
        this.damage = damage;
        this.hitstun = hitstun;
        this.blockstun = blockstun;
    }

}
