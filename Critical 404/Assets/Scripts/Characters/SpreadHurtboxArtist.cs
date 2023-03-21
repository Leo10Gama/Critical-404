using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadHurtboxArtist : HurtboxArtist
{
    // ~~~~~ Fun values to tweak ~~~~~
    /* ATTACK DATA TAKES THESE PARAMS:
     * Damage, Hitstun, Blockstun, Knockback
     */
    private static readonly AttackData SLP_DATA = new AttackData(
        22, 6, 4, new Vector2(1, 0)
    );
    private static readonly AttackData SHP_DATA = new AttackData(
        48, 9, 6, new Vector2(5, 0)
    );
    private static readonly AttackData SLK_DATA = new AttackData(
        27, 8, 6, new Vector2(2, 1)
    );
    private static readonly AttackData SHK_DATA = new AttackData(
        55, 10, 7, new Vector2(3, 20)
    );
    private static readonly AttackData CLP_DATA = new AttackData(
        20, 6, 4, new Vector2(1, 0)
    );
    private static readonly AttackData CHP_DATA = new AttackData(
        36, 8, 5, new Vector2(3, 0)
    );
    private static readonly AttackData CLK_DATA = new AttackData(
        25, 9, 7, new Vector2(1, 4), BlockState.low
    );
    private static readonly AttackData CHK_DATA = new AttackData(
        52, 11, 8, new Vector2(7, 5), BlockState.low
    );
    private static readonly AttackData JLP_DATA = new AttackData(
        11, 6, 3, new Vector2(1, -1), BlockState.high
    );
    private static readonly AttackData JHP_DATA = new AttackData(
        29, 8, 4, new Vector2(3, -3), BlockState.high
    );
    private static readonly AttackData JLK_DATA = new AttackData(
        18, 7, 5, new Vector2(3, 0)
    );
    private static readonly AttackData JHK_DATA = new AttackData(
        43, 10, 6, new Vector2(10, 5)
    );
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    // ########## MOVEMENT POSES ##########
    // === STANDING IDLE ===
    private readonly HurtboxAnimation IDLE_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.2090497f, 1.118416f),
            new Vector2(0.6655207f, 0.6864252f)
        ),
        new Hurtbox(    // upper body and outward arm
            new Vector2(0.1045249f, 0.5435291f),
            new Vector2(1.878009f, 0.7073303f)
        ),
        new Hurtbox(    // body and legs
            new Vector2(-0.4703619f, -0.5748868f),
            new Vector2(1.188145f, 2.275203f)
        )
    }));
    // === CROUCHING IDLE ===
    private readonly HurtboxAnimation CROUCH_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.03135753f, 0.3553846f),
            new Vector2(0.7700458f, 0.7491404f)
        ),
        new Hurtbox(    // outward arm
            new Vector2(0.6898639f, 0.01045266f),
            new Vector2(0.7909513f, 0.8118553f)
        ),
        new Hurtbox(    // body
            new Vector2(-0.2195024f, -0.8257464f),
            new Vector2(1.271766f, 1.898914f)
        )
    }));
    // === MOVING FORWARD ===
    private readonly HurtboxAnimation FORWARD_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 0 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2090497f, -0.9302712f),
                    new Vector2(1.75258f, 1.355385f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 1 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.522624f, -0.9093663f),
                    new Vector2(1.041811f, 1.648054f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.3031225f, 0.4390043f),
                    new Vector2(0.7282362f, 0.4564707f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 2 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.3971941f, -0.9511764f),
                    new Vector2(1.45991f, 1.606244f)
                ),
                new Hurtbox(    // backward arm
                    new Vector2(-0.7003162f, 0.3449318f),
                    new Vector2(0.8118558f, 0.8954752f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 3 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2717645f, -0.9302714f),
                    new Vector2(1.668959f, 1.689864f)
                ),
                new Hurtbox(    // backward arm
                    new Vector2(-0.7003162f, 0.3449318f),
                    new Vector2(0.8118558f, 0.8954752f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5017195f, 0.4180992f),
                    new Vector2(1.459909f, 0.4564707f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 4 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.5330765f, -0.9511764f),
                    new Vector2(0.8536649f, 1.606244f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === forward frame 5 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.3449316f, -0.8884615f),
                    new Vector2(1.564434f, 1.731674f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            })
        },
        new int[]
        {
            6,
            11-6,
            16-11,
            23-16,
            27-13,
            29-27
        }
    );
    // === MOVING BACKWARD ===
    private readonly HurtboxAnimation BACKWARD_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 0 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2090497f, -0.9302712f),
                    new Vector2(1.75258f, 1.355385f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 1 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.5330765f, -0.9511764f),
                    new Vector2(0.8536649f, 1.606244f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 2 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.2717645f, -0.9302714f),
                    new Vector2(1.668959f, 1.689864f)
                ),
                new Hurtbox(    // backward arm
                    new Vector2(-0.7003162f, 0.3449318f),
                    new Vector2(0.8118558f, 0.8954752f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5017195f, 0.4180992f),
                    new Vector2(1.459909f, 0.4564707f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 3 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.3971941f, -0.9511764f),
                    new Vector2(1.45991f, 1.606244f)
                ),
                new Hurtbox(    // backward arm
                    new Vector2(-0.7003162f, 0.3449318f),
                    new Vector2(0.8118558f, 0.8954752f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 4 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.522624f, -0.9093663f),
                    new Vector2(1.041811f, 1.648054f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.3031225f, 0.4390043f),
                    new Vector2(0.7282362f, 0.4564707f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // === backward frame 5 ===
                new Hurtbox(    // head and body
                    new Vector2(-0.1149776f, 0.6689591f),
                    new Vector2(0.7282357f, 1.919819f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.3449316f, -0.8884615f),
                    new Vector2(1.564434f, 1.731674f)
                ),
                new Hurtbox(    // outward arm
                    new Vector2(0.5226245f, 0.627149f),
                    new Vector2(1.041811f, 0.5819006f)
                )
            })
        },
        new int[]
        {
            6,
            11-6,
            16-11,
            23-16,
            27-13,
            29-27
        }
    );
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
    private readonly HurtboxAnimation SBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
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
    // === CROUCHING BLOCK ===
    private readonly HurtboxAnimation CBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // upper body
            new Vector2(-0.1619604f, 0.04143177f),
            new Vector2(0.7212768f, 0.9171364f)
        ),
        new Hurtbox(    // mid body
            new Vector2(-0.2787225f, -0.5649785f),
            new Vector2(0.2165627f, 0.6384134f)
        ),
        new Hurtbox(    // legs
            new Vector2(-0.3502867f, -1.020728f),
            new Vector2(0.8719382f, 0.4952856f)
        )
    }));
    // === JUMPING BLOCK ===
    private readonly HurtboxAnimation JBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.1016963f, 0.6968071f),
            new Vector2(0.6610131f, 0.7514092f)
        ),
        new Hurtbox(    // arms
            new Vector2(-0.007532835f, 0.3088551f),
            new Vector2(0.7740092f, 0.563083f)
        ),
        new Hurtbox(    // back leg
            new Vector2(-0.1092288f, -0.4519826f),
            new Vector2(0.5555511f, 0.8493387f)
        ),
        new Hurtbox(    // front leg
            new Vector2(0.2109253f, -0.6704411f),
            new Vector2(0.5329518f, 0.4124218f)
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
                    SLP_DATA, 1
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
                    SHP_DATA, 1
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
            19-11
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
                    SLK_DATA, 1
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
                    SHK_DATA, 1
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
    // === CROUCHING LIGHT PUNCH ===
    private readonly HurtboxAnimation CLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 0 ====
                new Hurtbox(    // head and windup arm
                    new Vector2(-0.003477097f, 0.09388435f),
                    new Vector2(0.770505f, 0.8122313f)
                ),
                new Hurtbox(    // torso and back arm
                    new Vector2(-0.3094707f, -0.5146254f),
                    new Vector2(0.6453257f, 0.5410095f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.09388423f, -0.9910016f),
                    new Vector2(0.8539577f, 0.5479641f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.04520369f, 0.04520363f),
                    new Vector2(0.6731434f, 0.8539574f)
                ),
                new Hurtbox(    // arms
                    new Vector2(0.02781749f, -0.3129478f),
                    new Vector2(1.375537f, 0.1932896f)
                ),
                new Hurtbox(    // torso
                    new Vector2(-0.1634285f, -0.598078f),
                    new Vector2(0.297606f, 0.3880128f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.1564741f, -1.011865f),
                    new Vector2(0.770505f, 0.5201466f)
                ),
                new Hitbox(     // arm
                    new Vector2(0.4868078f, -0.2816531f),
                    new Vector2(0.8748207f, 0.3393322f),
                    CLP_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(0.04520369f, 0.04520363f),
                    new Vector2(0.6731434f, 0.8539574f)
                ),
                new Hurtbox(    // arms
                    new Vector2(0.02781749f, -0.3129478f),
                    new Vector2(1.375537f, 0.1932896f)
                ),
                new Hurtbox(    // torso
                    new Vector2(-0.1634285f, -0.598078f),
                    new Vector2(0.297606f, 0.3880128f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.1564741f, -1.011865f),
                    new Vector2(0.770505f, 0.5201466f)
                )
            })
        },
        new int[]
        {
            4,
            6-4,
            13-6
        }
    );
    // === CROUCHING HEAVY PUNCH ===
    private readonly HurtboxAnimation CHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 0 ====
                new Hurtbox(    // head and windup arm
                    new Vector2(-0.003477097f, 0.09388435f),
                    new Vector2(0.770505f, 0.8122313f)
                ),
                new Hurtbox(    // torso and back arm
                    new Vector2(-0.3094707f, -0.5146254f),
                    new Vector2(0.6453257f, 0.5410095f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.09388423f, -0.9910016f),
                    new Vector2(0.8539577f, 0.5479641f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.04520369f, 0.04520363f),
                    new Vector2(0.6731434f, 0.8539574f)
                ),
                new Hurtbox(    // arms
                    new Vector2(0.02781749f, -0.3129478f),
                    new Vector2(1.375537f, 0.1932896f)
                ),
                new Hurtbox(    // torso
                    new Vector2(-0.1634285f, -0.598078f),
                    new Vector2(0.297606f, 0.3880128f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.1564741f, -1.011865f),
                    new Vector2(0.770505f, 0.5201466f)
                ),
                new Hitbox(     // arm
                    new Vector2(0.4868078f, -0.2816531f),
                    new Vector2(0.8748207f, 0.3393322f),
                    CHP_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(0.04520369f, 0.04520363f),
                    new Vector2(0.6731434f, 0.8539574f)
                ),
                new Hurtbox(    // arms
                    new Vector2(0.02781749f, -0.3129478f),
                    new Vector2(1.375537f, 0.1932896f)
                ),
                new Hurtbox(    // torso
                    new Vector2(-0.1634285f, -0.598078f),
                    new Vector2(0.297606f, 0.3880128f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.1564741f, -1.011865f),
                    new Vector2(0.770505f, 0.5201466f)
                )
            })
        },
        new int[]
        {
            6,
            10-6,
            16-10
        }
    );
    // === CROUCHING LIGHT KICK ===
    private readonly HurtboxAnimation CLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 0 ====
                new Hurtbox(    // entire body lmao
                    new Vector2(-0.01506615f, -0.4143177f),
                    new Vector2(0.7890739f, 1.677974f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 1 ====
                new Hurtbox(    // head
                    new Vector2(-0.1770265f, -0.09416309f),
                    new Vector2(0.8116736f, 0.6308804f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.3050883f, -0.5009477f),
                    new Vector2(1.067797f, 0.3747567f)
                ),
                new Hurtbox(    // back leg
                    new Vector2(-0.2636564f, -0.9868293f),
                    new Vector2(0.5932155f, 0.5329505f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.3201547f, -1.013195f),
                    new Vector2(0.6760788f, 0.2391618f)
                ),
                new Hitbox(     // kicking leg
                    new Vector2(0.331454f, -0.9755297f),
                    new Vector2(0.9397349f, 0.495286f),
                    CLK_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 2 ====
                new Hurtbox(    // head
                    new Vector2(-0.1770265f, -0.09416309f),
                    new Vector2(0.8116736f, 0.6308804f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.3050883f, -0.5009477f),
                    new Vector2(1.067797f, 0.3747567f)
                ),
                new Hurtbox(    // back leg
                    new Vector2(-0.2636564f, -0.9868293f),
                    new Vector2(0.5932155f, 0.5329505f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.3201547f, -1.013195f),
                    new Vector2(0.6760788f, 0.2391618f)
                )
            })
        },
        new int[]
        {
            4,
            6-4,
            15-6
        }
    );
    // === CROUCHING HEAVY KICK ===
    private readonly HurtboxAnimation CHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 0 ====
                new Hurtbox(    // entire body lmao
                    new Vector2(-0.01506615f, -0.4143177f),
                    new Vector2(0.7890739f, 1.677974f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 1 ====
                new Hurtbox(    // head
                    new Vector2(-0.1770265f, -0.09416309f),
                    new Vector2(0.8116736f, 0.6308804f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.3050883f, -0.5009477f),
                    new Vector2(1.067797f, 0.3747567f)
                ),
                new Hurtbox(    // back leg
                    new Vector2(-0.2636564f, -0.9868293f),
                    new Vector2(0.5932155f, 0.5329505f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.3201547f, -1.013195f),
                    new Vector2(0.6760788f, 0.2391618f)
                ),
                new Hitbox(     // kicking leg
                    new Vector2(0.331454f, -0.9755297f),
                    new Vector2(0.9397349f, 0.495286f),
                    CHK_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 2 ====
                new Hurtbox(    // head
                    new Vector2(-0.1770265f, -0.09416309f),
                    new Vector2(0.8116736f, 0.6308804f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.3050883f, -0.5009477f),
                    new Vector2(1.067797f, 0.3747567f)
                ),
                new Hurtbox(    // back leg
                    new Vector2(-0.2636564f, -0.9868293f),
                    new Vector2(0.5932155f, 0.5329505f)
                ),
                new Hurtbox(    // kicking leg
                    new Vector2(0.3201547f, -1.013195f),
                    new Vector2(0.6760788f, 0.2391618f)
                )
            })
        },
        new int[]
        {
            7,
            10-7,
            19-10
        }
    );
    // === JUMPING LIGHT PUNCH ===
    private readonly HurtboxAnimation JLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 0 ====
                new Hurtbox(    // head and back arm
                    new Vector2(-0.2297583f, 0.6252429f),
                    new Vector2(1.188326f, 0.9397354f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2259917f, -0.07909697f),
                    new Vector2(0.5329514f, 0.5706159f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.04519868f, -0.6704412f),
                    new Vector2(0.5329509f, 0.7438762f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(-0.1845598f, 0.6290095f),
                    new Vector2(1.233524f, 0.9171365f)
                ),
                new Hurtbox(    // body and punching arm
                    new Vector2(0.252357f, -0.03013209f),
                    new Vector2(0.9623346f, 0.4274884f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.1770267f, -0.6817408f),
                    new Vector2(0.7363434f, 0.7363433f)
                ),
                new Hitbox(     // punching arm
                    new Vector2(0.4444499f, -0.0715639f),
                    new Vector2(0.8945379f, 0.6308806f),
                    JLP_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(-0.1845598f, 0.6290095f),
                    new Vector2(1.233524f, 0.9171365f)
                ),
                new Hurtbox(    // body and punching arm
                    new Vector2(0.252357f, -0.03013209f),
                    new Vector2(0.9623346f, 0.4274884f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.1770267f, -0.6817408f),
                    new Vector2(0.7363434f, 0.7363433f)
                )
            })
        },
        new int[]
        {
            6,
            10-6,
            16-10
        }
    );
    // === JUMPING LIGHT PUNCH ===
    private readonly HurtboxAnimation JHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HP frame 0 ====
                new Hurtbox(    // head and back arm
                    new Vector2(-0.2297583f, 0.6252429f),
                    new Vector2(1.188326f, 0.9397354f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2259917f, -0.07909697f),
                    new Vector2(0.5329514f, 0.5706159f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.04519868f, -0.6704412f),
                    new Vector2(0.5329509f, 0.7438762f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(-0.1845598f, 0.6290095f),
                    new Vector2(1.233524f, 0.9171365f)
                ),
                new Hurtbox(    // body and punching arm
                    new Vector2(0.252357f, -0.03013209f),
                    new Vector2(0.9623346f, 0.4274884f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.1770267f, -0.6817408f),
                    new Vector2(0.7363434f, 0.7363433f)
                ),
                new Hitbox(     // punching arm
                    new Vector2(0.4444499f, -0.0715639f),
                    new Vector2(0.8945379f, 0.6308806f),
                    JHP_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(-0.1845598f, 0.6290095f),
                    new Vector2(1.233524f, 0.9171365f)
                ),
                new Hurtbox(    // body and punching arm
                    new Vector2(0.252357f, -0.03013209f),
                    new Vector2(0.9623346f, 0.4274884f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.1770267f, -0.6817408f),
                    new Vector2(0.7363434f, 0.7363433f)
                )
            })
        },
        new int[]
        {
            9,
            14-9,
            21-14
        }
    );
    // === JUMPING LIGHT KICK ===
    private readonly HurtboxAnimation JLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LK frame 0 ====
                new Hurtbox(    // head
                    new Vector2(-0.0451982f, 0.6214765f),
                    new Vector2(0.9096036f, 0.871938f)
                ),
                new Hurtbox(    // arms
                    new Vector2(0.003766537f, 0.1883263f),
                    new Vector2(1.44445f, 0.2617611f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.003766537f, -0.09792954f),
                    new Vector2(0.2391624f, 0.4425542f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.01883292f, -0.5838112f),
                    new Vector2(0.7062116f, 0.5856822f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LK frame 1 ====
                new Hurtbox(    // head and arms
                    new Vector2(-0.4067848f, 0.546146f),
                    new Vector2(1.150662f, 0.9924667f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2335246f, -0.1468944f),
                    new Vector2(0.3822913f, 0.5856821f)
                ),
                new Hurtbox(    // raised leg
                    new Vector2(0.06026435f, -0.3088549f),
                    new Vector2(0.3973575f, 0.7137439f)
                ),
                new Hurtbox(    // stretched leg
                    new Vector2(0.5838113f, -0.2975553f),
                    new Vector2(0.9472704f, 0.2090298f)
                ),
                new Hitbox(     // kicking leg
                    new Vector2(0.4896481f, -0.2711897f),
                    new Vector2(1.346522f, 0.3672238f),
                    JLK_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LK frame 2 ====
                new Hurtbox(    // head and arms
                    new Vector2(-0.4067848f, 0.546146f),
                    new Vector2(1.150662f, 0.9924667f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2335246f, -0.1468944f),
                    new Vector2(0.3822913f, 0.5856821f)
                ),
                new Hurtbox(    // raised leg
                    new Vector2(0.06026435f, -0.3088549f),
                    new Vector2(0.3973575f, 0.7137439f)
                ),
                new Hurtbox(    // stretched leg
                    new Vector2(0.5838113f, -0.2975553f),
                    new Vector2(0.9472704f, 0.2090298f)
                )
            })
        },
        new int[]
        {
            7,
            12-7,
            18-12
        }
    );
    // === JUMPING HEAVY KICK ===
    private readonly HurtboxAnimation JHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 0 ====
                new Hurtbox(    // head
                    new Vector2(-0.0451982f, 0.6214765f),
                    new Vector2(0.9096036f, 0.871938f)
                ),
                new Hurtbox(    // arms
                    new Vector2(0.003766537f, 0.1883263f),
                    new Vector2(1.44445f, 0.2617611f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.003766537f, -0.09792954f),
                    new Vector2(0.2391624f, 0.4425542f)
                ),
                new Hurtbox(    // legs
                    new Vector2(-0.01883292f, -0.5838112f),
                    new Vector2(0.7062116f, 0.5856822f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 1 ====
                new Hurtbox(    // head and arms
                    new Vector2(-0.4067848f, 0.546146f),
                    new Vector2(1.150662f, 0.9924667f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2335246f, -0.1468944f),
                    new Vector2(0.3822913f, 0.5856821f)
                ),
                new Hurtbox(    // raised leg
                    new Vector2(0.06026435f, -0.3088549f),
                    new Vector2(0.3973575f, 0.7137439f)
                ),
                new Hurtbox(    // stretched leg
                    new Vector2(0.5838113f, -0.2975553f),
                    new Vector2(0.9472704f, 0.2090298f)
                ),
                new Hitbox(     // kicking leg
                    new Vector2(0.4896481f, -0.2711897f),
                    new Vector2(1.346522f, 0.3672238f),
                    JHK_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 2 ====
                new Hurtbox(    // head and arms
                    new Vector2(-0.4067848f, 0.546146f),
                    new Vector2(1.150662f, 0.9924667f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2335246f, -0.1468944f),
                    new Vector2(0.3822913f, 0.5856821f)
                ),
                new Hurtbox(    // raised leg
                    new Vector2(0.06026435f, -0.3088549f),
                    new Vector2(0.3973575f, 0.7137439f)
                ),
                new Hurtbox(    // stretched leg
                    new Vector2(0.5838113f, -0.2975553f),
                    new Vector2(0.9472704f, 0.2090298f)
                )
            })
        },
        new int[]
        {
            11,
            18-11,
            26-18
        }
    );

    public SpreadHurtboxArtist(HitboxManager hbm, GameObject hurtboxObj, GameObject hitboxObj)
        : base(hbm, hurtboxObj, hitboxObj)
    {
    }

    public override IEnumerator DrawIdle(bool facingRight)
    {
        return DrawHurtboxAnimation(IDLE_FRAMES, facingRight);
    }

    public override IEnumerator DrawCrouch(bool facingRight)
    {
        return DrawHurtboxAnimation(CROUCH_FRAMES, facingRight);
    }

    public override IEnumerator DrawMoveForward(bool facingRight)
    {
        return DrawHurtboxAnimation(FORWARD_FRAMES, facingRight);
    }

    public override IEnumerator DrawMoveBackward(bool facingRight)
    {
        return DrawHurtboxAnimation(BACKWARD_FRAMES, facingRight);
    }

    public override IEnumerator DrawJump(bool facingRight)
    {
        return DrawHurtboxAnimation(RISING_FRAMES, facingRight);
    }

    public override IEnumerator DrawJumpRise(bool facingRight)
    {
        return DrawHurtboxAnimation(RISING_FRAMES, facingRight);
    }

    public override IEnumerator DrawJumpFall(bool facingRight)
    {
        return DrawHurtboxAnimation(FALLING_FRAMES, facingRight);
    }

    public override IEnumerator DrawStandingBlock(bool facingRight)
    {
        return DrawHurtboxAnimation(SBLOCKING_FRAMES, facingRight);
    }

    public override IEnumerator DrawCrouchingBlock(bool facingRight)
    {
        return DrawHurtboxAnimation(CBLOCKING_FRAMES, facingRight);
    }

    public override IEnumerator DrawJumpingBlock(bool facingRight)
    {
        return DrawHurtboxAnimation(JBLOCKING_FRAMES, facingRight);
    }

    public override IEnumerator DrawSLP(bool facingRight)
    {
        return DrawHurtboxAnimation(SLP_FRAMES, facingRight);
    }

    public override IEnumerator DrawSHP(bool facingRight)
    {
        return DrawHurtboxAnimation(SHP_FRAMES, facingRight);
    }

    public override IEnumerator DrawSLK(bool facingRight)
    {
        return DrawHurtboxAnimation(SLK_FRAMES, facingRight);
    }

    public override IEnumerator DrawSHK(bool facingRight)
    {
        return DrawHurtboxAnimation(SHK_FRAMES, facingRight);
    }

    public override IEnumerator DrawCLP(bool facingRight)
    {
        return DrawHurtboxAnimation(CLP_FRAMES, facingRight);
    }

    public override IEnumerator DrawCHP(bool facingRight)
    {
        return DrawHurtboxAnimation(CHP_FRAMES, facingRight);
    }

    public override IEnumerator DrawCLK(bool facingRight)
    {
        return DrawHurtboxAnimation(CLK_FRAMES, facingRight);
    }

    public override IEnumerator DrawCHK(bool facingRight)
    {
        return DrawHurtboxAnimation(CHK_FRAMES, facingRight);
    }

    public override IEnumerator DrawJLP(bool facingRight)
    {
        return DrawHurtboxAnimation(JLP_FRAMES, facingRight);
    }

    public override IEnumerator DrawJHP(bool facingRight)
    {
        return DrawHurtboxAnimation(JHP_FRAMES, facingRight);
    }

    public override IEnumerator DrawJLK(bool facingRight)
    {
        return DrawHurtboxAnimation(JLK_FRAMES, facingRight);
    }

    public override IEnumerator DrawJHK(bool facingRight)
    {
        return DrawHurtboxAnimation(JHK_FRAMES, facingRight);
    }

    public override IEnumerator DrawHitstun(bool facingRight)
    {
        return DrawHurtboxAnimation(HIT_FRAMES, facingRight);
    }

}
