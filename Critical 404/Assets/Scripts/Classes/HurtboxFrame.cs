using UnityEngine;

/// <summary>
/// This is a class to represent a collection of hurtboxes on a given
/// frame of animation. It makes use of the Hurtbox class to maintain
/// the collection and store them properly.
/// <summary/>
public class HurtboxFrame
{

    public Hurtbox[] hurtboxes;

    public HurtboxFrame(Hurtbox[] hurtboxes)
    {
        this.hurtboxes = new Hurtbox[hurtboxes.Length];
        for (int i = 0; i < hurtboxes.Length; i++) 
        {
            this.hurtboxes[i] = hurtboxes[i];
        }
    }

}