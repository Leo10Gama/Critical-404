using UnityEngine;

/// <summary>
/// This is a class to represent a collection of HurtboxFrames. That
/// is, in a given animation, there will consist of a series of
/// frames for which to draw hurtboxes, as well as manage the timing
/// in between those frames.
/// <summary/>
public class HurtboxAnimation
{

    public HurtboxFrame[] frames;
    public int[] frameDurations;

    public HurtboxAnimation(HurtboxFrame[] frames, int[] frameDurations)
    {
        this.frames = frames;
        this.frameDurations = new int[frames.Length];
        for (int i = 0; i < frames.Length; i++) 
        {   // if not enough values provided, default to 1
            if (i >= frameDurations.Length) this.frameDurations[i] = 1;   
            else this.frameDurations[i] = frameDurations[i];
        }
    }

    public HurtboxAnimation(HurtboxFrame[] frames) : this(frames, new int[0])
    {
    }

    public HurtboxAnimation(HurtboxFrame frame) : this(new HurtboxFrame[] {frame})
    {
    }

}
