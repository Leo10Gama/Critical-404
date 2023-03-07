using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtboxArtist : HurtboxArtist
{

    private HitboxManager hbm;
    private GameObject hurtboxObject;
    private GameObject hitboxObject;

    private bool spawnHitboxesThisImage = true;

    // ~~~~~ Fun values to tweak ~~~~~
    private const int SLP_DAMAGE = 22;
    private const int SLP_HITSTUN = 13;
    private const int SLP_BLOCKSTUN = 6;
    private const int SHP_DAMAGE = 48;
    private const int SHP_HITSTUN = 18;
    private const int SHP_BLOCKSTUN = 9;
    private const int SLK_DAMAGE = 27;
    private const int SLK_HITSTUN = 15;
    private const int SLK_BLOCKSTUN = 7;
    private const int SHK_DAMAGE = 55;
    private const int SHK_HITSTUN = 21;
    private const int SHK_BLOCKSTUN = 10;
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    // ########## MOVEMENT POSES ##########
    // === STANDING IDLE ===
    private readonly HurtboxAnimation IDLE_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.06859541f, 0.7585843f),
            new Vector2(0.8143892f, 0.838599f)
        ),
        new Hurtbox(    // body
            new Vector2(-0.1049104f, -0.05649029f),
            new Vector2(0.241416f, 0.7901788f)
        ),
        new Hurtbox(    // back arm
            new Vector2(-0.4196424f, 0.008070052f),
            new Vector2(0.3866768f, 0.6610581f)
        ),
        new Hurtbox(    // front arm
            new Vector2(0.3349068f, 0.01614013f),
            new Vector2(0.636848f, 0.3866765f)
        ),
        new Hurtbox(    // legs
            new Vector2(-0.04842019f, -0.9441953f),
            new Vector2(0.6610579f, 0.6610579f)
        )
    }));
    // === CROUCHING IDLE ===
    private readonly HurtboxAnimation CROUCH_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(0.02824545f, 0.1210507f),
            new Vector2(0.6852684f, 0.7094784f)
        ),
        new Hurtbox(    // upper body
            new Vector2(-0.09280515f, -0.4236773f),
            new Vector2(1.02421f, 0.3947467f)
        ),
        new Hurtbox(    // lower body
            new Vector2(-0.1452603f, -0.9764754f),
            new Vector2(0.9193001f, 0.5964978f)
        )
    }));
    // === MOVING FORWARD ===
    private readonly HurtboxAnimation FORWARD_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(0.3026271f, 0.6899888f),
            new Vector2(0.8305302f, 0.8143889f)
        ),
        new Hurtbox(    // arms
            new Vector2(0.1896462f, 0.07666548f),
            new Vector2(1.395433f, 0.3624663f)
        ),
        new Hurtbox(    // torso and front leg
            new Vector2(0.05649042f, -0.7182339f),
            new Vector2(0.3221169f, 1.11298f)
        ),
        new Hurtbox(    // back leg
            new Vector2(-0.4922724f, -0.6859536f),
            new Vector2(0.6933393f, 0.3059762f)
        )
    }));
    // === MOVING BACKWARD ===
    private readonly HurtboxAnimation BACKWARD_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.3510463f, 0.6859539f),
            new Vector2(0.9112306f, 0.9354396f)
        ),
        new Hurtbox(    // arms
            new Vector2(-0.2259607f, -0.02017507f),
            new Vector2(1.112982f, 0.459307f)
        ),
        new Hurtbox(    // torso
            new Vector2(-0.1614001f, -0.4599925f),
            new Vector2(0.3543973f, 0.3705365f)
        ),
        new Hurtbox(    // legs
            new Vector2(-0.1735051f, -0.9482303f),
            new Vector2(0.7659698f, 0.620708f)
        ),
    }));
    // === JUMP RISING ===
    private readonly HurtboxAnimation RISING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // top half
            new Vector2(0.08070135f, 0.6415685f),
            new Vector2(1.000001f, 1.02421f)
        ),
        new Hurtbox(    // bottom half
            new Vector2(-0.1735051f, -0.536658f),
            new Vector2(0.7821097f, 1.250172f)
        ),
    }));
    // === JUMP FALLING ===
    private readonly HurtboxAnimation FALLING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // upper body
            new Vector2(-0.04438424f, 0.6940238f),
            new Vector2(1.169472f, 0.8063192f)
        ),
        new Hurtbox(    // torso
            new Vector2(0.05649114f, -0.1008756f),
            new Vector2(0.2575574f, 0.5238677f)
        ),
        new Hurtbox(    // legs
            new Vector2(0.2461371f, -0.7182341f),
            new Vector2(0.5884295f, 0.6449184f)
        )
    }));
    // === STANDING BLOCK ===
    private readonly HurtboxAnimation BLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame( new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.09479785f, 0.8531797f),
            new Vector2(0.8815026f, 0.8341038f)
        ),
        new Hurtbox(    // arms
            new Vector2(0.08887291f, 0.4858385f),
            new Vector2(1.035549f, 0.7867051f)
        ),
        new Hurtbox(    // body
            new Vector2(-0.01184988f, -0.242919f),
            new Vector2(0.2416182f, 0.6800576f)
        ),
        new Hurtbox(    // legs
            new Vector2(-0.05332398f, -0.9361274f),
            new Vector2(0.7511563f, 0.6682078f)
        )
    }));
    // === HIT ===
    private readonly HurtboxAnimation HIT_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.280998f, 0.796161f),
            new Vector2(0.8876014f, 0.7939347f)
        ),
        new Hurtbox(    // arms
            new Vector2(-0.3184643f, 0.2060652f),
            new Vector2(1.299732f, 0.3256046f)
        ),
        new Hurtbox(    // body
            new Vector2(-0.3933973f, -0.2622648f),
            new Vector2(0.4005384f, 0.7752014f)
        ),
        new Hurtbox(    // legs
            new Vector2(-0.1217659f, -0.9553933f),
            new Vector2(0.7939363f, 0.6253358f)
        )
    }));
    // ########## ATTACKS ##########
    // === STANDING LIGHT PUNCH ===
    private readonly HurtboxAnimation SLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LP frame 0 =====
                new Hurtbox(    // head
                    new Vector2(-0.3429761f, 0.6294634f),
                    new Vector2(1.201753f, 0.9031599f)
                ),
                new Hurtbox(    // chest
                    new Vector2(-0.3913963f, -0.05245522f),
                    new Vector2(0.6368489f, 0.233346f)
                ),
                new Hurtbox(    // back hand
                    new Vector2(-0.7666535f, -0.367187f),
                    new Vector2(0.3059769f, 0.427027f)
                ),
                new Hurtbox(    // gut
                    new Vector2(-0.1049097f, -0.3631519f),
                    new Vector2(0.2414165f, 0.435097f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.06859446f, -0.9199851f),
                    new Vector2(0.7982492f, 0.7094784f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LP frame 1 =====
                new Hurtbox(    // head
                    new Vector2(-0.07666445f, 0.6940239f),
                    new Vector2(0.8466692f, 0.8708795f)
                ),
                new Hurtbox(    // punching arm
                    new Vector2(0.520519f, 0.1775411f),
                    new Vector2(0.7659688f, 0.2252761f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.3510458f, -0.0968405f),
                    new Vector2(0.4108863f, 0.7094789f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.01613879f, -0.1977161f),
                    new Vector2(0.2091355f, 0.91123f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.048419f, -0.9361252f),
                    new Vector2(0.7094784f, 0.6771985f)
                ),
                new Hitbox(     // arm
                    new Vector2(0.5999374f, 0.1714107f),
                    new Vector2(1.039557f, 0.4725825f),
                    SLP_DAMAGE, SLP_HITSTUN, SLP_BLOCKSTUN,
                    1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LP frame 2 =====
                new Hurtbox(    // head
                    new Vector2(-0.07666445f, 0.6940239f),
                    new Vector2(0.8466692f, 0.8708795f)
                ),
                new Hurtbox(    // punching arm
                    new Vector2(0.520519f, 0.1775411f),
                    new Vector2(0.7659688f, 0.2252761f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.3510458f, -0.0968405f),
                    new Vector2(0.4108863f, 0.7094789f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.01613879f, -0.1977161f),
                    new Vector2(0.2091355f, 0.91123f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.048419f, -0.9361252f),
                    new Vector2(0.7094784f, 0.6771985f)
                )
            }),
        },
        new int[]
        {
            4,
            6-4,
            15-6
        }
    );
    // === STANDING HEAVY PUNCH ===
    private readonly HurtboxAnimation SHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 0 =====
                new Hurtbox(    // head
                    new Vector2(-0.3429761f, 0.6294634f),
                    new Vector2(1.201753f, 0.9031599f)
                ),
                new Hurtbox(    // chest
                    new Vector2(-0.3913963f, -0.05245522f),
                    new Vector2(0.6368489f, 0.233346f)
                ),
                new Hurtbox(    // back hand
                    new Vector2(-0.7666535f, -0.367187f),
                    new Vector2(0.3059769f, 0.427027f)
                ),
                new Hurtbox(    // gut
                    new Vector2(-0.1049097f, -0.3631519f),
                    new Vector2(0.2414165f, 0.435097f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.06859446f, -0.9199851f),
                    new Vector2(0.7982492f, 0.7094784f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 1 =====
                new Hurtbox(    // head
                    new Vector2(-0.07666445f, 0.6940239f),
                    new Vector2(0.8466692f, 0.8708795f)
                ),
                new Hurtbox(    // punching arm
                    new Vector2(0.520519f, 0.1775411f),
                    new Vector2(0.7659688f, 0.2252761f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.3510458f, -0.0968405f),
                    new Vector2(0.4108863f, 0.7094789f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.01613879f, -0.1977161f),
                    new Vector2(0.2091355f, 0.91123f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.048419f, -0.9361252f),
                    new Vector2(0.7094784f, 0.6771985f)
                ),
                new Hitbox(     // arm
                    new Vector2(0.5999374f, 0.1714107f),
                    new Vector2(1.039557f, 0.4725825f),
                    SHP_DAMAGE, SHP_HITSTUN, SHP_BLOCKSTUN,
                    1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 2 =====
                new Hurtbox(    // head
                    new Vector2(-0.07666445f, 0.6940239f),
                    new Vector2(0.8466692f, 0.8708795f)
                ),
                new Hurtbox(    // punching arm
                    new Vector2(0.520519f, 0.1775411f),
                    new Vector2(0.7659688f, 0.2252761f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.3510458f, -0.0968405f),
                    new Vector2(0.4108863f, 0.7094789f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.01613879f, -0.1977161f),
                    new Vector2(0.2091355f, 0.91123f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.048419f, -0.9361252f),
                    new Vector2(0.7094784f, 0.6771985f)
                )
            })
        },
        new int[]
        {
            8,
            11-8,
            19-10
        }
    );
    // === STANDING LIGHT KICK ===
    private readonly HurtboxAnimation SLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 0 =====
                new Hurtbox(    // head
                    new Vector2(-0.0080688f, 0.6213933f),
                    new Vector2(0.9031596f, 0.8547394f)
                ),
                new Hurtbox(    // back arm and body
                    new Vector2(-0.3187654f, -0.08877065f),
                    new Vector2(0.636848f, 0.5157977f)
                ),
                new Hurtbox(    // front arm
                    new Vector2(0.4317486f, -0.1250859f),
                    new Vector2(0.7336888f, 0.2333462f)
                ),
                new Hurtbox(    // back leg
                    new Vector2(-0.6012168f, -0.4438526f),
                    new Vector2(0.7982492f, 0.2736964f)
                ),
                new Hurtbox(    // front leg
                    new Vector2(-0.2420998f, -0.9119152f),
                    new Vector2(0.209136f, 0.7094788f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 1 =====
                new Hurtbox(    // head
                    new Vector2(-0.008068562f, 0.7101637f),
                    new Vector2(0.7578993f, 0.8224596f)
                ),
                new Hurtbox(    // front arm
                    new Vector2(0.3671887f, -2.682209e-07f),
                    new Vector2(0.620708f, 0.4512376f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.3510456f, -0.1008758f),
                    new Vector2(0.3947468f, 0.6045684f)
                ),
                new Hurtbox(    // body and standing leg
                    new Vector2(-0.03631377f, -0.5124481f),
                    new Vector2(0.2172055f, 1.508413f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.6375351f, -0.6940241f),
                    new Vector2(0.98386f, 0.2414165f)
                ),
                new Hitbox(     // leg
                    new Vector2(0.7120137f, -0.7186065f),
                    new Vector2(1.184596f, 0.5121388f),
                    SLK_DAMAGE, SLK_HITSTUN, SLK_BLOCKSTUN,
                    1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 2 =====
                new Hurtbox(    // head
                    new Vector2(-0.008068562f, 0.7101637f),
                    new Vector2(0.7578993f, 0.8224596f)
                ),
                new Hurtbox(    // front arm
                    new Vector2(0.3671887f, -2.682209e-07f),
                    new Vector2(0.620708f, 0.4512376f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.3510456f, -0.1008758f),
                    new Vector2(0.3947468f, 0.6045684f)
                ),
                new Hurtbox(    // body and standing leg
                    new Vector2(-0.03631377f, -0.5124481f),
                    new Vector2(0.2172055f, 1.508413f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.6375351f, -0.6940241f),
                    new Vector2(0.98386f, 0.2414165f)
                )
            })
        },
        new int[] 
        {
            5,
            7-5,
            14-7
        }
    );
    // === STANDING HEAVY KICK ===
    private readonly HurtboxAnimation SHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 0 =====
                new Hurtbox(    // head
                    new Vector2(-0.0080688f, 0.6213933f),
                    new Vector2(0.9031596f, 0.8547394f)
                ),
                new Hurtbox(    // back arm and body
                    new Vector2(-0.3187654f, -0.08877065f),
                    new Vector2(0.636848f, 0.5157977f)
                ),
                new Hurtbox(    // front arm
                    new Vector2(0.4317486f, -0.1250859f),
                    new Vector2(0.7336888f, 0.2333462f)
                ),
                new Hurtbox(    // back leg
                    new Vector2(-0.6012168f, -0.4438526f),
                    new Vector2(0.7982492f, 0.2736964f)
                ),
                new Hurtbox(    // front leg
                    new Vector2(-0.2420998f, -0.9119152f),
                    new Vector2(0.209136f, 0.7094788f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 1 =====
                new Hurtbox(    // head
                    new Vector2(-0.008068562f, 0.7101637f),
                    new Vector2(0.7578993f, 0.8224596f)
                ),
                new Hurtbox(    // front arm
                    new Vector2(0.3671887f, -2.682209e-07f),
                    new Vector2(0.620708f, 0.4512376f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.3510456f, -0.1008758f),
                    new Vector2(0.3947468f, 0.6045684f)
                ),
                new Hurtbox(    // body and standing leg
                    new Vector2(-0.03631377f, -0.5124481f),
                    new Vector2(0.2172055f, 1.508413f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.6375351f, -0.6940241f),
                    new Vector2(0.98386f, 0.2414165f)
                ),
                new Hitbox(     // leg
                    new Vector2(0.7120137f, -0.7186065f),
                    new Vector2(1.184596f, 0.5121388f),
                    SHK_DAMAGE, SHK_HITSTUN, SHK_BLOCKSTUN,
                    1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 2 =====
                new Hurtbox(    // head
                    new Vector2(-0.008068562f, 0.7101637f),
                    new Vector2(0.7578993f, 0.8224596f)
                ),
                new Hurtbox(    // front arm
                    new Vector2(0.3671887f, -2.682209e-07f),
                    new Vector2(0.620708f, 0.4512376f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.3510456f, -0.1008758f),
                    new Vector2(0.3947468f, 0.6045684f)
                ),
                new Hurtbox(    // body and standing leg
                    new Vector2(-0.03631377f, -0.5124481f),
                    new Vector2(0.2172055f, 1.508413f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.6375351f, -0.6940241f),
                    new Vector2(0.98386f, 0.2414165f)
                )
            })
        },
        new int[] 
        {
            8,
            11-8,
            21-11
        }
    );

    public PlayerHurtboxArtist(HitboxManager hbm, GameObject hurtboxObj, GameObject hitboxObj)
    {
        this.hbm = hbm;
        this.hurtboxObject = hurtboxObj;
        this.hitboxObject = hitboxObj;
    }

    public override IEnumerator DrawIdle(bool facingRight)
    {
        yield return DrawHurtboxAnimation(IDLE_FRAMES, facingRight);
    }

    public override IEnumerator DrawCrouch(bool facingRight)
    {
        yield return DrawHurtboxAnimation(CROUCH_FRAMES, facingRight);
    }

    public override IEnumerator DrawMoveForward(bool facingRight)
    {
        yield return DrawHurtboxAnimation(FORWARD_FRAMES, facingRight);
    }

    public override IEnumerator DrawMoveBackward(bool facingRight)
    {
        yield return DrawHurtboxAnimation(BACKWARD_FRAMES, facingRight);
    }

    public override IEnumerator DrawJumpRise(bool facingRight)
    {
        yield return DrawHurtboxAnimation(RISING_FRAMES, facingRight);
    }

    public override IEnumerator DrawJumpFall(bool facingRight)
    {
        yield return DrawHurtboxAnimation(FALLING_FRAMES, facingRight);
    }

    public override IEnumerator DrawStandingBlock(bool facingRight)
    {
        yield return DrawHurtboxAnimation(BLOCKING_FRAMES, facingRight);
    }

    public override IEnumerator DrawSLP(bool facingRight)
    {
        yield return DrawHurtboxAnimation(SLP_FRAMES, facingRight);
    }

    public override IEnumerator DrawSHP(bool facingRight)
    {
        yield return DrawHurtboxAnimation(SHP_FRAMES, facingRight);
    }

    public override IEnumerator DrawSLK(bool facingRight)
    {
        yield return DrawHurtboxAnimation(SLK_FRAMES, facingRight);
    }

    public override IEnumerator DrawSHK(bool facingRight)
    {
        yield return DrawHurtboxAnimation(SHK_FRAMES, facingRight);
    }

    public override IEnumerator DrawHitstun(bool facingRight)
    {
        yield return DrawHurtboxAnimation(HIT_FRAMES, facingRight);
    }

    public override void StopDrawingAll()
    {
        hbm.ClearAll(hurtboxObject);
        hbm.ClearAll(hitboxObject);
    }

    private IEnumerator DrawHurtboxAnimation(HurtboxAnimation anim, bool facingRight)
    {
        int flipMultiplier = facingRight ? 1 : -1;
        for (int i = 0; i < anim.frames.Length; i++)
        {
            if (!spawnHitboxesThisImage) spawnHitboxesThisImage = true; // reset once previous image is done
            HurtboxFrame frame = anim.frames[i];
            foreach (Hurtbox hurtbox in frame.hurtboxes)
            {
                if (hurtbox.GetType() == typeof(Hitbox) && spawnHitboxesThisImage)
                    hbm.CreateHitbox(hitboxObject, (Hitbox)hurtbox, flipMultiplier, anim.frameDurations[i]);
                else
                    hbm.CreateHurtbox(hurtboxObject, hurtbox, flipMultiplier, anim.frameDurations[i]);
            }
            yield return new WaitForSeconds(anim.frameDurations[i] / 60f);
        }
    }

    public void PreventHitboxesThisImage()
    {
        spawnHitboxesThisImage = false;
    }

}
