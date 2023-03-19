using System.Collections;
using UnityEngine;

public enum BufferableInput
{
    slp,    // 0
    slk,    // 1
    shp,    // 2
    shk,    // 3
    clp,    // 4
    clk,    // 5
    chp,    // 6
    chk,    // 7
    jump    // 8
}

/// Class representing a bufferable input, that exists with a given
/// lifespan. This lifespan represents the number of frames that a
/// given input can be buffered for.
public class AttackInput
{
    public BufferableInput input;
    public string attackButtonName;
    public IEnumerator regularRoutine;
    public IEnumerator jumpingRoutine;
    private int lifespan = 3;   // frame buffer

    public AttackInput(BufferableInput input, string attackButtonName, IEnumerator regularRoutine, IEnumerator jumpingRoutine)
    {
        this.input = input;
        this.attackButtonName = attackButtonName;
        this.regularRoutine = regularRoutine;
        this.jumpingRoutine = jumpingRoutine;
    }

    public AttackInput(BufferableInput input)
        : this(input, "", null, null)
    {
    }

    public bool IsAlive()
    {
        return lifespan > 0;
    }

    public IEnumerator PassTime()
    {
        while (lifespan > 0)
        {
            yield return new WaitForSeconds(1f / 60f);
            lifespan--;
        }
    }
}