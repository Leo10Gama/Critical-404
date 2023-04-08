using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilaHurtboxArtist : HurtboxArtist
{
    // ~~~~~ Fun values to tweak ~~~~~
    /* ATTACK DATA TAKES THESE PARAMS:
     * Damage, Hitstun, Blockstun, Hitstop, Knockback, (BlockState)
     */
    private static readonly AttackData SLP_DATA = new AttackData(
        22, 10, 4, 3, new Vector2(1, 0)
    );
    private static readonly AttackData SHP_DATA = new AttackData(
        48, 16, 6, 10, new Vector2(5, 0)
    );
    private static readonly AttackData SLK_DATA = new AttackData(
        27, 8, 6, 5, new Vector2(2, 1)
    );
    private static readonly AttackData SHK_DATA = new AttackData(
        55, 22, 7, 10, new Vector2(4, 20)
    );
    private static readonly AttackData CLP_DATA = new AttackData(
        18, 8, 4, 5, new Vector2(1, 0)
    );
    private static readonly AttackData CHP_DATA = new AttackData(
        35, 18, 5, 9, new Vector2(4, 1)
    );
    private static readonly AttackData CLK_DATA = new AttackData(
        25, 9, 7, 4, new Vector2(1, 4), BlockState.low
    );
    private static readonly AttackData CHK_DATA = new AttackData(
        39, 20, 8, 10, new Vector2(-3, 4), BlockState.low
    );
    private static readonly AttackData JLP_DATA = new AttackData(
        19, 9, 3, 6, new Vector2(2, 1), BlockState.high
    );
    private static readonly AttackData JHP_DATA = new AttackData(
        32, 17, 4, 10, new Vector2(4, 0), BlockState.high
    );
    private static readonly AttackData JLK_DATA = new AttackData(
        22, 9, 5, 7, new Vector2(3, -1)
    );
    private static readonly AttackData JHK_DATA = new AttackData(
        58, 20, 6, 10, new Vector2(13, 7)
    );
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    // ########## MOVEMENT POSES ##########
    // ~~~ IDLE ~~~
    private static readonly HurtboxFrame _IDLE = new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.012954f, 0.9542692f),
            new Vector2(0.7150149f, 0.8186454f)
        ),
        new Hurtbox(    // body
            new Vector2(0.07340527f, 0.08204123f),
            new Vector2(0.6459274f, 0.9740921f)
        ),
        new Hurtbox(    // arm in front
            new Vector2(0.5872428f, -0.1986262f),
            new Vector2(0.4127574f, 0.8272814f)
        ),
        new Hurtbox(    // arm behind back
            new Vector2(-0.4577036f, -0.06476939f),
            new Vector2(0.5336599f, 0.8877329f)
        ),
        new Hurtbox(    // legs
            new Vector2(0.1122668f, -1.014721f),
            new Vector2(0.9481845f, 1.457703f)
        )
    });
    // === STANDING IDLE ===
    private readonly HurtboxAnimation IDLE_FRAMES = new HurtboxAnimation(_IDLE);
    // === CROUCHING IDLE ===
    private readonly HurtboxAnimation CROUCH_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(0.04458857f, 0.3009722f),
            new Vector2(0.7919207f, 0.8885288f)
        ),
        new Hurtbox(    // body
            new Vector2(-0.05573535f, -0.5907971f),
            new Vector2(0.8885293f, 0.9925687f)
        ),
        new Hurtbox(    // raised leg
            new Vector2(0.1932168f, -1.203888f),
            new Vector2(1.178354f, 1.10404f)
        ),
        new Hurtbox(    // back leg
            new Vector2(-0.5239141f, -1.612616f),
            new Vector2(0.9033923f, 0.286585f)
        )
    }));
    // ~~~ MOVING ~~~
    private static readonly HurtboxFrame[] _WALK = new HurtboxFrame[] {
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/walk_1.png
            new Hurtbox(    // body
                new Vector2(0.02229428f, -0.1746383f),
                new Vector2(0.7770591f, 3.236855f)
            ),
            new Hurtbox(    // arms
                new Vector2(-0.04087257f, -0.1597754f),
                new Vector2(1.453318f, 0.8142148f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/walk_2.png
            new Hurtbox(    // head and body
                new Vector2(0.007431507f, 0.3864332f),
                new Vector2(0.7027454f, 1.995809f)
            ),
            new Hurtbox(    // arms
                new Vector2(0.06688285f, -0.08174568f),
                new Vector2(1.222944f, 1.089177f)
            ),
            new Hurtbox(    // legs
                new Vector2(0.08917737f, -1.137006f),
                new Vector2(0.702745f, 1.222942f)
            ),
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/walk_3.png
            new Hurtbox(    // body
                new Vector2(-0.04087234f, -0.1783543f),
                new Vector2(0.8142161f, 3.21456f)
            ),
            new Hurtbox(    // leg
                new Vector2(-0.4904728f, -1.226183f),
                new Vector2(0.5838423f, 0.5838408f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/walk_4.png
            new Hurtbox(    // body
                new Vector2(-0.003715515f, -0.2192767f),
                new Vector2(0.7993536f, 3.132914f)
            ),
            new Hurtbox(    // leg
                new Vector2(-0.5313458f, -1.296831f),
                new Vector2(0.6209993f, 0.665686f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/walk_5.png
            new Hurtbox(    // body
                new Vector2(0.003715754f, 0.3455107f),
                new Vector2(0.9033933f, 2.092516f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.3269823f, -1.036731f),
                new Vector2(0.5095286f, 1.379101f)
            ),
            new Hurtbox(    // back leg
                new Vector2(-0.4979043f, -1.066457f),
                new Vector2(0.8810987f, 1.052119f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/walk_6.png
            new Hurtbox(    // body
                new Vector2(0.01114726f, -0.1784036f),
                new Vector2(0.739902f, 3.21466f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.4198749f, -0.7840636f),
                new Vector2(0.3683314f, 1.141296f)
            ),
            new Hurtbox(    // back arm
                new Vector2(-0.4533157f, 0.0556858f),
                new Vector2(0.5541162f, 0.992668f)
            )
        }),
    };
    // === MOVING FORWARD ===
    private readonly HurtboxAnimation FORWARD_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _WALK[3],   // === forward frame 0 ===
            _WALK[2],   // === forward frame 1 ===
            _WALK[1],   // === forward frame 2 ===
            _WALK[0],   // === forward frame 3 ===
            _WALK[5],   // === forward frame 4 ===
            _WALK[4]    // === forward frame 5 ===
        },
        new int[]
        {
            8,
            11-8,
            18-11,
            24-18,
            28-24,
            30-28
        }
    );
    // === MOVING BACKWARD ===
    private readonly HurtboxAnimation BACKWARD_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _WALK[0],   // === backward frame 0 ===
            _WALK[1],   // === backward frame 1 ===
            _WALK[2],   // === backward frame 2 ===
            _WALK[3],   // === backward frame 3 ===
            _WALK[4],   // === backward frame 4 ===
            _WALK[5]    // === backward frame 5 ===
        },
        new int[]
        {
            4,
            12-4,
            18-12,
            25-18,
            30-25,
            33-30
        }
    );
    // ~~~ AIR IDLES ~~~
    private static readonly HurtboxFrame[] _JUMP = new HurtboxFrame[] {
        _IDLE,                              // Mila/Jumping/Jump/Jump_1.png
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/Jump/Jump_2.png
            new Hurtbox(    // head and body
                new Vector2(0.05191422f, 0.3828684f),
                new Vector2(0.7663851f, 1.869565f)
            ),
            new Hurtbox(    // arms
                new Vector2(0.03893566f, -0.220636f),
                new Vector2(1.207657f, 0.8702141f)
            ),
            new Hurtbox(    // legs
                new Vector2(0.09084988f, -1.083712f),
                new Vector2(1.077871f, 1.402336f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/Jump/Jump_3.png
            new Hurtbox(    // head
                new Vector2(0.3958468f, 0.707333f),
                new Vector2(0.8053207f, 0.8831923f)
            ),
            new Hurtbox(    // body
                new Vector2(-0.05840373f, -0.3049971f),
                new Vector2(0.8831925f, 1.506165f)
            ),
            new Hurtbox(    // legs
                new Vector2(-0.3763793f, -1.401688f),
                new Vector2(1.259572f, 0.7923422f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/Jump/Jump_4.png
            new Hurtbox(    // head
                new Vector2(0.4023361f, 0.4672289f),
                new Vector2(0.8442564f, 0.8961708f)
            ),
            new Hurtbox(    // body and arms
                new Vector2(0.02595735f, -0.3763793f),
                new Vector2(0.7144704f, 1.259571f)
            ),
            new Hurtbox(    // torso and front leg
                new Vector2(-0.4477613f, -0.6619083f),
                new Vector2(0.8053203f, 1.311486f)
            ),
            new Hurtbox(    // back calf
                new Vector2(-0.992862f, -0.525633f),
                new Vector2(0.4159627f, 0.8831921f)
            )
        })
    };
    // === JUMP ===
    private readonly HurtboxAnimation JUMP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _JUMP[0],   // === jump frame 0 ===
            _JUMP[1],   // === jump frame 1 ===
            _JUMP[2],   // === jump frame 2 ===
            _JUMP[3]    // === jump frame 3 ===
        },
        new int[]
        {
            2,
            6-2,
            10-6,
            12-10
        }
    );
    // === JUMP RISING ===
    private readonly HurtboxAnimation RISING_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _JUMP[3]    // === rising frame 0
        },
        new int[]
        {
            1
        }
    );
    // === JUMP FALLING ===
    private readonly HurtboxAnimation FALLING_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _JUMP[3]    // === falling frame 0 ===
        },
        new int[]
        {
            1
        }
    );
    // === STANDING BLOCK ===
    private readonly HurtboxAnimation SBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(0.240104f, 0.8306295f),
            new Vector2(0.8053203f, 0.8702133f)
        ),
        new Hurtbox(    // body and back arm
            new Vector2(-0.116807f, 0.1232966f),
            new Vector2(0.8702135f, 0.8831917f)
        ),
        new Hurtbox(    // forward arm
            new Vector2(0.4282935f, -0.3634004f),
            new Vector2(0.5327706f, 0.5068127f)
        ),
        new Hurtbox(    // legs
            new Vector2(0.01297832f, -0.9604154f),
            new Vector2(0.9480853f, 1.648929f)
        )
    }));
    // === CROUCHING BLOCK ===
    private readonly HurtboxAnimation CBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(0.09733915f, 0.2271255f),
            new Vector2(0.7793636f, 0.8572346f)
        ),
        new Hurtbox(    // body and arms
            new Vector2(-0.2530825f, -0.4477611f),
            new Vector2(0.9091496f, 0.7793631f)
        ),
        new Hurtbox(    // front leg and back thigh
            new Vector2(0.08436084f, -1.258923f),
            new Vector2(1.324464f, 1.051914f)
        ),
        new Hurtbox(    // back calf
            new Vector2(-0.6489296f, -1.628813f),
            new Vector2(0.9221287f, 0.3121343f)
        )
    }));
    // === JUMPING BLOCK ===
    private readonly HurtboxAnimation JBLOCKING_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(0.421804f, 0.4802081f),
            new Vector2(0.8312778f, 0.8961707f)
        ),
        new Hurtbox(    // body and arms
            new Vector2(0.05191422f, -0.3763787f),
            new Vector2(0.6625566f, 1.311485f)
        ),
        new Hurtbox(    // torso and leg
            new Vector2(-0.4542508f, -0.6554183f),
            new Vector2(0.740428f, 1.298507f)
        ),
        new Hurtbox(    // back calf
            new Vector2(-0.9993515f, -0.538611f),
            new Vector2(0.4289417f, 0.8831918f)
        ),
    }));
    // === HIT ===
    private readonly HurtboxAnimation HIT_FRAMES = new HurtboxAnimation(new HurtboxFrame(new Hurtbox[] {
        new Hurtbox(    // head
            new Vector2(-0.2595718f, 0.8890336f),
            new Vector2(0.8442569f, 0.9091492f)
        ),
        new Hurtbox(    // body and back arm
            new Vector2(-0.5840366f, 0.2660612f),
            new Vector2(0.870214f, 0.7014918f)
        ),
        new Hurtbox(    // body and upper legs
            new Vector2(-0.4703619f, -0.5748868f),
            new Vector2(1.188145f, 2.275203f)
        ),
        new Hurtbox(    // torso and back arm
            new Vector2(-0.2465935f, -0.3698896f),
            new Vector2(1f, 0.7534061f)
        ),
        new Hurtbox(    // legs
            new Vector2(0.1297855f, -1.122648f),
            new Vector2(1.155744f, 1.324464f)
        ),
    }));
    // ########## ATTACKS ##########
    // === STANDING LIGHT PUNCH ===
    private static readonly HurtboxFrame[] _SLP = new HurtboxFrame[] {
        new HurtboxFrame(new Hurtbox[] {    // Standing/s.LP/SLP1.png
            new Hurtbox(    // head and body
                new Vector2(-0.01727223f, 0.3281648f),
                new Vector2(0.6718345f, 1.777232f)
            ),
            new Hurtbox(    // punching fist
                new Vector2(-0.3411191f, 0.1727183f),
                new Vector2(1.043179f, 0.3609414f)
            ),
            new Hurtbox(    // upper legs
                new Vector2(0.09499478f, -0.6995098f),
                new Vector2(1.051815f, 0.9481846f)
            ),
            new Hurtbox(    // back calf
                new Vector2(-0.4965656f, -1.368794f),
                new Vector2(0.732286f, 0.7322865f)
            ),
            new Hurtbox(    // front calf
                new Vector2(0.3929343f, -1.381747f),
                new Vector2(0.4386644f, 0.7927379f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Standing/s.LP/SLP2.png
            new Hurtbox(    // head and body
                new Vector2(-0.01727223f, 0.3281648f),
                new Vector2(0.6718345f, 1.777232f)
            ),
            new Hurtbox(    // punching fist
                new Vector2(-0.2590778f, 0.1770362f),
                new Vector2(1.172718f, 0.3868497f)
            ),
            new Hurtbox(    // upper legs
                new Vector2(0.09499478f, -0.6995098f),
                new Vector2(1.051815f, 0.9481846f)
            ),
            new Hurtbox(    // back calf
                new Vector2(-0.4965656f, -1.368794f),
                new Vector2(0.732286f, 0.7322865f)
            ),
            new Hurtbox(    // front calf
                new Vector2(0.3929343f, -1.381747f),
                new Vector2(0.4386644f, 0.7927379f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Standing/s.LP/SLP3.png
            new Hurtbox(    // head and body
                new Vector2(-0.01727223f, 0.3281648f),
                new Vector2(0.6718345f, 1.777232f)
            ),
            new Hurtbox(    // punching fist
                new Vector2(0.4965649f, 0.2115799f),
                new Vector2(1.42316f, 0.3177623f)
            ),
            new Hurtbox(    // upper legs
                new Vector2(0.09499478f, -0.6995098f),
                new Vector2(1.051815f, 0.9481846f)
            ),
            new Hurtbox(    // back calf
                new Vector2(-0.4965656f, -1.368794f),
                new Vector2(0.732286f, 0.7322865f)
            ),
            new Hurtbox(    // front calf
                new Vector2(0.3929343f, -1.381747f),
                new Vector2(0.4386644f, 0.7927379f)
            )
        })
    };
    private readonly HurtboxAnimation SLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            _SLP[0],                            // ===== s.LP frame 0 =====
            _SLP[1],                            // ===== s.LP frame 1 =====
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LP frame 2 =====
                _SLP[2].hurtboxes[0],
                _SLP[2].hurtboxes[1],
                _SLP[2].hurtboxes[2],
                _SLP[2].hurtboxes[3],
                _SLP[2].hurtboxes[4],
                new Hitbox(
                    new Vector2(0.7513244f, 0.194308f),
                    new Vector2(1.224533f, 0.5941118f),
                    SLP_DATA, 2
                )
            }),
            _SLP[2],                            // ==== s.LP frame 3 ====
            _SLP[1],                            // ==== s.LP frame 4 ====
            _SLP[0]                             // ==== s.LP frame 5 ====
        },
        new int[]
        {
            4,
            6-4,
            8-6,
            16-6,
            21-16,
            24-21
        }
    );
    // === STANDING HEAVY PUNCH ===
    private readonly HurtboxAnimation SHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            _SLP[0],                            // ===== s.HP frame 0 =====
            _SLP[1],                            // ===== s.HP frame 1 =====
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HP frame 2 =====
                _SLP[2].hurtboxes[0],
                _SLP[2].hurtboxes[1],
                _SLP[2].hurtboxes[2],
                _SLP[2].hurtboxes[3],
                _SLP[2].hurtboxes[4],
                new Hitbox(
                    new Vector2(0.493186f, 0.103829f),
                    new Vector2(1.674887f, 0.7663846f),
                    SHP_DATA, 2
                )
            }),
            _SLP[2],                            // ===== s.HP frame 3 =====
            _SLP[1],                            // ===== s.HP frame 4 =====
            _SLP[0],                            // ===== s.HP frame 5 =====
        },
        new int[]
        {
            9,
            12-9,
            18-12,
            28-18,
            35-28,
            39-35
        }
    );
    // === STANDING LIGHT KICK ===
    private static readonly HurtboxFrame[] _SLK = new HurtboxFrame[] {
        _IDLE,                              // Mila/Standing/s.LK/SLK1.png
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/s.LK/SLK2.png
            _IDLE.hurtboxes[0],
            _IDLE.hurtboxes[1],
            _IDLE.hurtboxes[2],
            _IDLE.hurtboxes[3],
            new Hurtbox(    // back leg
                new Vector2(-0.142765f, -0.9085009f),
                new Vector2(0.5846853f, 1.7268f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.4088252f, -0.8760543f),
                new Vector2(0.5197921f, 1.661907f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/s.LK/SLK3.png
            _IDLE.hurtboxes[0],
            _IDLE.hurtboxes[1],
            _IDLE.hurtboxes[2],
            _IDLE.hurtboxes[3],
            new Hurtbox(    // back leg
                new Vector2(-0.142765f, -0.9085009f),
                new Vector2(0.5846853f, 1.7268f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.5905254f, -0.7462684f),
                new Vector2(0.7793641f, 1.376378f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/s.LK/SLK4.png
            _IDLE.hurtboxes[0],
            _IDLE.hurtboxes[1],
            _IDLE.hurtboxes[2],
            _IDLE.hurtboxes[3],
            new Hurtbox(    // back leg
                new Vector2(-0.142765f, -0.9085009f),
                new Vector2(0.5846853f, 1.7268f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.6748862f, -0.5645681f),
                new Vector2(0.9740429f, 1.090849f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Standing/s.LK/SLK5.png
            _IDLE.hurtboxes[0],
            _IDLE.hurtboxes[1],
            _IDLE.hurtboxes[2],
            _IDLE.hurtboxes[3],
            new Hurtbox(    // back leg
                new Vector2(-0.142765f, -0.9085009f),
                new Vector2(0.5846853f, 1.7268f)
            ),
            new Hurtbox(    // front leg
                new Vector2(1.018819f, -0.3634f),
                new Vector2(1.661908f, 0.6106412f)
            )
        }),
    };
    private readonly HurtboxAnimation SLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _SLK[1],                            // ===== s.LK frame 0 =====
            _SLK[2],                            // ===== s.LK frame 1 =====
            _SLK[3],                            // ===== s.LK frame 2 =====
            new HurtboxFrame(new Hurtbox[] {    // ===== s.LK frame 3 =====
                _SLK[4].hurtboxes[0],
                _SLK[4].hurtboxes[1],
                _SLK[4].hurtboxes[2],
                _SLK[4].hurtboxes[3],
                _SLK[4].hurtboxes[4],
                _SLK[4].hurtboxes[5],
                new Hitbox(
                    new Vector2(1.070733f, -0.7657363f),
                    new Vector2(1.765737f, 1.259571f),
                    SLK_DATA, 3
                )
            }),
            _SLK[4],                            // ===== s.LK frame 4 =====
            _SLK[3],                            // ===== s.LK frame 5 =====
            _SLK[2],                            // ===== s.LK frame 6 =====
            _SLK[1],                            // ===== s.LK frame 7 =====
            _SLK[0],                            // ===== s.LK frame 8 =====
        },
        new int[] 
        {
            2,
            4-2,
            8-4,
            10-8,
            13-10,
            16-13,
            20-16,
            22-20,
            23-22
        }
    );
    // === STANDING HEAVY KICK ===
    private readonly HurtboxAnimation SHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[] 
        {
            _SLK[0],                            // ===== s.HK frame 0 =====
            _SLK[2],                            // ===== s.HK frame 1 =====
            _SLK[3],                            // ===== s.HK frame 2 =====
            new HurtboxFrame(new Hurtbox[] {    // ===== s.HK frame 3 =====
                _SLK[4].hurtboxes[0],
                _SLK[4].hurtboxes[1],
                _SLK[4].hurtboxes[2],
                _SLK[4].hurtboxes[3],
                _SLK[4].hurtboxes[4],
                _SLK[4].hurtboxes[5],
                new Hitbox(
                    new Vector2(1.083712f, -0.7916934f),
                    new Vector2(1.843608f, 1.519142f),
                    SHK_DATA, 3
                )
            }),
            _SLK[4],                            // ===== s.HK frame 4 =====
            _SLK[3],                            // ===== s.HK frame 5 =====
            _SLK[2],                            // ===== s.HK frame 6 =====
            _SLK[1],                            // ===== s.HK frame 7 =====
            _SLK[0]                             // ===== s.HK frame 8 =====
        },
        new int[] 
        {
            7,
            9-7,
            14-9,
            17-14,
            27-17,
            31-27,
            35-31,
            41-35,
            45-41
        }
    );
    // === CROUCHING LIGHT PUNCH ===
    private static readonly HurtboxFrame[] _CLP = new HurtboxFrame[] {
        new HurtboxFrame(new Hurtbox[] {    // Mila/Crouching/c.LP/CLP1.png
            new Hurtbox(    // head and body
                new Vector2(0.0064888f, -0.5126538f),
                new Vector2(0.8572359f, 2.492537f)
            ),
            new Hurtbox(    // punching arm
                new Vector2(-0.123297f, -0.4218037f),
                new Vector2(1.168722f, 0.5976625f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.5710576f, -1.252433f),
                new Vector2(0.4808564f, 1.064892f)
            ),
            new Hurtbox(    // back leg
                new Vector2(-0.5256331f, -1.615834f),
                new Vector2(0.9610639f, 0.3380909f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Crouching/c.LP/CLP2.png
            new Hurtbox(    // head and body
                new Vector2(0.0064888f, -0.5126538f),
                new Vector2(0.8572359f, 2.492537f)
            ),
            new Hurtbox(    // punching arm
                new Vector2(0.3049967f, -0.5905252f),
                new Vector2(1.168722f, 0.5976629f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.5710576f, -1.252433f),
                new Vector2(0.4808564f, 1.064892f)
            ),
            new Hurtbox(    // back leg
                new Vector2(-0.5256331f, -1.615834f),
                new Vector2(0.9610639f, 0.3380909f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Crouching/c.LP/CLP3.png
            new Hurtbox(    // head and body
                new Vector2(0.0064888f, -0.5126538f),
                new Vector2(0.8572359f, 2.492537f)
            ),
            new Hurtbox(    // punching arm
                new Vector2(0.6294615f, -0.2141461f),
                new Vector2(1.194679f, 0.753406f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.5710576f, -1.252433f),
                new Vector2(0.4808564f, 1.064892f)
            ),
            new Hurtbox(    // back leg
                new Vector2(-0.5256331f, -1.615834f),
                new Vector2(0.9610639f, 0.3380909f)
            )
        })
    };
    private readonly HurtboxAnimation CLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _CLP[0],                            // ==== c.LP frame 0 ====
            _CLP[1],                            // ==== c.LP frame 1 ====
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 2 ====
                _CLP[2].hurtboxes[0],
                _CLP[2].hurtboxes[1],
                _CLP[2].hurtboxes[2],
                _CLP[2].hurtboxes[3],
                new Hitbox(
                    new Vector2(0.6099935f, -0.324464f),
                    new Vector2(1.467229f, 1.051914f),
                    CLP_DATA, 2
                )
            }),
            _CLP[2],                            // ==== c.LP frame 3 ====
            _CLP[1],                            // ==== c.LP frame 4 ====
            _CLP[0]                             // ==== c.LP frame 5 ====
        },
        new int[]
        {
            6,
            8-6,
            10-8,
            14-10,
            20-14,
            24-20
        }
    );
    // === CROUCHING HEAVY PUNCH ===
    private readonly HurtboxAnimation CHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _CLP[0],                            // ==== c.HP frame 0 ====
            _CLP[1],                            // ==== c.HP frame 1 ====
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 2 ====
                _CLP[2].hurtboxes[0],
                _CLP[2].hurtboxes[1],
                _CLP[2].hurtboxes[2],
                _CLP[2].hurtboxes[3],
                new Hitbox(
                    new Vector2(0.5061648f, -0.324464f),
                    new Vector2(1.441271f, 1.285528f),
                    CHP_DATA, 2
                )
            }),
            _CLP[2],                            // ==== c.HP frame 3 ====
            _CLP[1],                            // ==== c.HP frame 4 ====
            _CLP[0]                             // ==== c.HP frame 5 ====
        },
        new int[]
        {
            10,
            12-10,
            15-12,
            26-15,
            32-26,
            35-32
        }
    );
    // === CROUCHING LIGHT KICK ===
    private static readonly HurtboxFrame[] _CLK = new HurtboxFrame[] {
        new HurtboxFrame(new Hurtbox[] {    // Mila/Crouching/c.LK/CLK1.png
            new Hurtbox(    // head and body
                new Vector2(-0.1622329f, -0.499675f),
                new Vector2(0.8312778f, 2.518494f)
            ),
            new Hurtbox(    // back calf
                new Vector2(-0.5191443f, -1.615834f),
                new Vector2(0.9221282f, 0.3380909f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.4672284f, -1.226476f),
                new Vector2(0.8182993f, 1.116806f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Crouching/c.LK/CLK2.png
            new Hurtbox(    // head and body
                new Vector2(-0.1622329f, -0.499675f),
                new Vector2(0.8312778f, 2.518494f)
            ),
            new Hurtbox(    // back calf
                new Vector2(-0.5191443f, -1.615834f),
                new Vector2(0.9221282f, 0.3380909f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.5710571f, -1.194029f),
                new Vector2(1.025957f, 0.9740419f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Crouching/c.LK/CLK3.png
            new Hurtbox(    // head and body
                new Vector2(-0.1622329f, -0.499675f),
                new Vector2(0.8312778f, 2.518494f)
            ),
            new Hurtbox(    // back calf
                new Vector2(-0.5191443f, -1.615834f),
                new Vector2(0.9221282f, 0.3380909f)
            ),
            new Hurtbox(    // front leg
                new Vector2(0.791693f, -0.9604149f),
                new Vector2(1.467228f, 0.4808556f)
            )
        })
    };
    private readonly HurtboxAnimation CLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _CLK[0],                            // ==== c.LK frame 0 ====
            _CLK[1],                            // ==== c.LK frame 1 ====
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 2 ====
                _CLK[2].hurtboxes[0],
                _CLK[2].hurtboxes[1],
                _CLK[2].hurtboxes[2],
                new Hitbox(
                    new Vector2(0.8695643f, -1.207008f),
                    new Vector2(1.622971f, 1.103828f),
                    CLK_DATA, 2
                )
            }),
            _CLK[2],                            // ==== c.LK frame 3 ====
            _CLK[1],                            // ==== c.LK frame 4 ====
            _CLK[0]                             // ==== c.LK frame 5 ====
        },
        new int[]
        {
            4,
            7-4,
            9-7,
            12-9,
            17-12,
            20-17
        }
    );
    // === CROUCHING HEAVY KICK ===
    private readonly HurtboxAnimation CHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _CLK[0],                            // ==== c.HK frame 0 ====
            _CLK[1],                            // ==== c.HK frame 1 ====
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 2 ====
                _CLK[2].hurtboxes[0],
                _CLK[2].hurtboxes[1],
                _CLK[2].hurtboxes[2],
                new Hitbox(
                    new Vector2(0.8565857f, -1.168072f),
                    new Vector2(1.597014f, 1.181699f),
                    CHK_DATA, 2
                )
            }),
            _CLK[2],                            // ==== c.HK frame 3 ====
            _CLK[1],                            // ==== c.HK frame 4 ====
            _CLK[0]                             // ==== c.HK frame 5 ====
        },
        new int[]
        {
            10,
            12-10,
            16-12,
            25-16,
            30-25,
            32-30
        }
    );
    // === JUMPING LIGHT PUNCH ===
    private static readonly HurtboxFrame[] _JLP = new HurtboxFrame[] {
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/j.LP/JLP1.png
            new Hurtbox(    // head and body
                new Vector2(0.1622312f, 0.4347835f),
                new Vector2(0.8831916f, 1.480207f)
            ),
            new Hurtbox(    // hands
                new Vector2(0.5321212f, 1.122649f),
                new Vector2(0.740427f, 0.9870205f)
            ),
            new Hurtbox(    // legs and torso
                new Vector2(-0.6229734f, -0.6554178f),
                new Vector2(1.181699f, 1.298507f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/j.LP/JLP2.png
            new Hurtbox(    // head and body
                new Vector2(0.1752095f, 0.3893585f),
                new Vector2(0.9091482f, 1.467229f)
            ),
            new Hurtbox(    // hands and arms
                new Vector2(0.8241391f, 0.4477622f),
                new Vector2(1.35042f, 0.7014917f)
            ),
            new Hurtbox(    // legs and torso
                new Vector2(-0.5191448f, -0.6878644f),
                new Vector2(1.207656f, 1.285528f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/j.LP/JLP3.png
            new Hurtbox(    // head and body
                new Vector2(0.1557417f, 0.3763799f),
                new Vector2(0.8961697f, 1.3634f)
            ),
            new Hurtbox(    // hands and arms
                new Vector2(0.7981818f, 0.05191523f),
                new Vector2(1.402335f, 0.5846843f)
            ),
            new Hurtbox(    // legs and torso
                new Vector2(-0.3049984f, -0.6359499f),
                new Vector2(1.402335f, 1.1817f)
            )
        })
    };
    private readonly HurtboxAnimation JLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _JLP[0],                            // ==== j.LP frame 0 ====
            _JLP[1],                            // ==== j.LP frame 1 ====
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 2 ====
                _JLP[2].hurtboxes[0],
                _JLP[2].hurtboxes[1],
                _JLP[2].hurtboxes[2],
                new Hitbox(
                    new Vector2(0.7916923f, 0.4153157f),
                    new Vector2(1.467228f, 1.415314f),
                    JLP_DATA, 2
                )
            }),
            _JLP[2],                            // ==== j.LP frame 3 ====
            _JLP[1]                             // ==== j.LP frame 4 ====
        },
        new int[]
        {
            4,
            7-4,
            9-7,
            14-9,
            19-14
        }
    );
    // === JUMPING LIGHT PUNCH ===
    private readonly HurtboxAnimation JHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _JLP[0],                            // ==== j.HP frame 0 ====
            _JLP[1],                            // ==== j.HP frame 1 ====
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HP frame 2 ====
                _JLP[2].hurtboxes[0],
                _JLP[2].hurtboxes[1],
                _JLP[2].hurtboxes[2],
                new Hitbox(
                    new Vector2(0.8371174f, 0.5580802f),
                    new Vector2(1.558078f, 1.882543f),
                    JHP_DATA, 2
                )
            }),
            _JLP[2],                            // ==== j.HP frame 3 ====
            _JLP[1]                             // ==== j.HP frame 4 ====
        },
        new int[]
        {
            6,
            8-6,
            11-8,
            28-11,
            30-28
        }
    );
    // === JUMPING LIGHT KICK ===
    private static readonly HurtboxFrame[] _JLK = new HurtboxFrame[] {
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/j.LK/JLK1.png
            new Hurtbox(    // head
                new Vector2(0.428292f, 0.4672301f),
                new Vector2(0.870213f, 0.9221276f)
            ),
            new Hurtbox(    // arms and body
                new Vector2(0.05191278f, -0.4218031f),
                new Vector2(0.6885128f, 1.350421f)
            ),
            new Hurtbox(    // legs and torso
                new Vector2(-0.6748881f, -0.6489285f),
                new Vector2(1.051913f, 1.285528f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/j.LK/JLK2.png
            new Hurtbox(    // head
                new Vector2(0.428292f, 0.4672301f),
                new Vector2(0.870213f, 0.9221276f)
            ),
            new Hurtbox(    // arms and body
                new Vector2(-0.03244799f, -0.4218031f),
                new Vector2(0.8567215f, 1.350421f)
            ),
            new Hurtbox(    // not-kicking leg
                new Vector2(-0.3114874f, -0.8241393f),
                new Vector2(0.5582137f, 0.9606425f)
            ),
            new Hurtbox(    // kicking leg
                new Vector2(-0.8436098f, -0.3179743f),
                new Vector2(0.7399139f, 0.9346851f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/j.LK/JLK3.png
            new Hurtbox(    // head
                new Vector2(0.4218025f, 0.4802089f),
                new Vector2(0.7788496f, 0.8697922f)
            ),
            new Hurtbox(    // arms and body
                new Vector2(0.006487608f, -0.3633993f),
                new Vector2(0.8307643f, 1.25915f)
            ),
            new Hurtbox(    // not-kicking leg
                new Vector2(-0.3114874f, -0.9604144f),
                new Vector2(0.5322566f, 0.688092f)
            ),
            new Hurtbox(    // kicking leg
                new Vector2(-0.9020131f, -0.5580783f),
                new Vector2(1.090336f, 0.9995779f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/j.LK/JLK4.png
            new Hurtbox(    // head
                new Vector2(0.4023349f, 0.5061659f),
                new Vector2(0.8437428f, 0.869792f)
            ),
            new Hurtbox(    // arms and body
                new Vector2(0.02595592f, -0.330953f),
                new Vector2(0.8697f, 1.246171f)
            ),
            new Hurtbox(    // hips and thighs
                new Vector2(-0.3309553f, -0.6813749f),
                new Vector2(0.6490636f, 1.272128f)
            ),
            new Hurtbox(    // kicking calf
                new Vector2(-0.7203131f, -1.336794f),
                new Vector2(0.5971489f, 0.740006f)
            )
        }),
        new HurtboxFrame(new Hurtbox[] {    // Mila/Jumping/j.LK/JLK5.png
            new Hurtbox(    // head
                new Vector2(0.3958457f, 0.5191444f),
                new Vector2(0.752892f, 0.8697916f)
            ),
            new Hurtbox(    // arms and body
                new Vector2(0.01297736f, -0.2985067f),
                new Vector2(1.0514f, 1.15532f)
            ),
            new Hurtbox(    // back knee
                new Vector2(-0.1362765f, -0.9993504f),
                new Vector2(0.49332f, 0.6880915f)
            ),
            new Hurtbox(    // kicking calf
                new Vector2(0.1427634f, -1.537962f),
                new Vector2(0.4803414f, 0.5972413f)
            )
        }),
    };
    private readonly HurtboxAnimation JLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _JLK[0],                            // ==== j.LK frame 0 ====
            _JLK[1],                            // ==== j.LK frame 1 ====
            _JLK[2],                            // ==== j.LK frame 2 ====
            _JLK[3],                            // ==== j.LK frame 3 ====
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LK frame 4 ====
                _JLK[4].hurtboxes[0],
                _JLK[4].hurtboxes[1],
                _JLK[4].hurtboxes[2],
                _JLK[4].hurtboxes[3],
                new Hitbox(
                    new Vector2(-0.2882583f, -1.497089f),
                    new Vector2(1.446425f, 0.7681639f),
                    JLK_DATA, 4
                )
            }),
            _JLK[4],                            // ==== j.LK frame 5 ====
            _JLK[3],                            // ==== j.LK frame 6 ====
            _JLK[2],                            // ==== j.LK frame 7 ====
            _JLK[1],                            // ==== j.LK frame 8 ====
            _JLK[0],                            // ==== j.LK frame 9 ====
        },
        new int[]
        {
            2,
            6-2,
            10-6,
            12-10,
            15-12,
            19-15,
            23-19,
            28-23,
            32-28,
            34-32
        }
    );
    // === JUMPING HEAVY KICK ===
    private readonly HurtboxAnimation JHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            _JLK[0],                            // ==== j.HK frame 0 ====
            _JLK[1],                            // ==== j.HK frame 1 ====
            _JLK[2],                            // ==== j.HK frame 2 ====
            _JLK[3],                            // ==== j.HK frame 3 ====
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 4 ====
                _JLK[4].hurtboxes[0],
                _JLK[4].hurtboxes[1],
                _JLK[4].hurtboxes[2],
                _JLK[4].hurtboxes[3],
                new Hitbox(
                    new Vector2(-0.21766f, -1.359608f),
                    new Vector2(1.647073f, 1.147166f),
                    JHK_DATA, 4
                )
            }),
            _JLK[4],                            // ==== j.HK frame 5 ====
            _JLK[3],                            // ==== j.HK frame 6 ====
            _JLK[2],                            // ==== j.HK frame 7 ====
            _JLK[1],                            // ==== j.HK frame 8 ====
            _JLK[0],                            // ==== j.HK frame 9 ====
        },
        new int[]
        {
            2,
            9-2,
            12-9,
            14-12,
            20-14,
            28-20,
            32-28,
            35-32,
            38-35,
            40-38
        }
    );

    public MilaHurtboxArtist(HitboxManager hbm, GameObject hurtboxObj, GameObject hitboxObj)
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
        return DrawHurtboxAnimation(JUMP_FRAMES, facingRight);
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
