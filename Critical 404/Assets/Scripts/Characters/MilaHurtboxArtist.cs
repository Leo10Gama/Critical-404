using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilaHurtboxArtist : HurtboxArtist
{

    /**
     *  DELETE WHEN DONE BUT, CHECKLIST FOR ME TO REMEMBER WHICH
     *  ANIMATION HITBOXES I ALREADY MADE

     standing idle
     crouching idle
     walk forward
     walk backward
     jump
     rise (waiting on second frame)
     fall (waiting on second frame)
     standing block
     crouching block
     jumping block
     hit
     s.LP
     s.HP
     s.LK
     s.HK

     */

    // ~~~~~ Fun values to tweak ~~~~~
    /* ATTACK DATA TAKES THESE PARAMS:
     * Damage, Hitstun, Blockstun, Knockback
     */
    private static readonly AttackData SLP_DATA = new AttackData(
        22, 10, 4, new Vector2(1, 0)
    );
    private static readonly AttackData SHP_DATA = new AttackData(
        48, 16, 6, new Vector2(5, 0)
    );
    private static readonly AttackData SLK_DATA = new AttackData(
        27, 8, 6, new Vector2(2, 1)
    );
    private static readonly AttackData SHK_DATA = new AttackData(
        55, 22, 7, new Vector2(4, 20)
    );
    private static readonly AttackData CLP_DATA = new AttackData(
        18, 8, 4, new Vector2(1, 0)
    );
    private static readonly AttackData CHP_DATA = new AttackData(
        35, 18, 5, new Vector2(4, 1)
    );
    private static readonly AttackData CLK_DATA = new AttackData(
        25, 9, 7, new Vector2(1, 4), BlockState.low
    );
    private static readonly AttackData[] CHK_DATA = 
    {
        new AttackData(
            39, 20, 8, new Vector2(-3, 4), BlockState.low
        ),
        new AttackData(
            56, 20, 8, new Vector2(2, 17), BlockState.low
        )
    };
    private static readonly AttackData JLP_DATA = new AttackData(
        19, 9, 3, new Vector2(2, 1), BlockState.high
    );
    private static readonly AttackData JHP_DATA = new AttackData(
        32, 17, 4, new Vector2(4, 0), BlockState.high
    );
    private static readonly AttackData JLK_DATA = new AttackData(
        22, 9, 5, new Vector2(3, -1)
    );
    private static readonly AttackData[] JHK_DATA = 
    {
        new AttackData(
            12, 10, 6, new Vector2(-1, 1)
        ),
        new AttackData(
            12, 10, 6, new Vector2(-1, 1)
        ),
        new AttackData(
            12, 10, 6, new Vector2(-1, 1)
        ),
        new AttackData(
            12, 10, 6, new Vector2(-1, 1)
        ),
        new AttackData(
            12, 10, 6, new Vector2(-1, 1)
        ),
        new AttackData(
            58, 20, 6, new Vector2(13, 7)
        )
    };
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
    private readonly HurtboxAnimation CLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 0 ====
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
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.2684984f, -0.07671382f),
                    new Vector2(2.227421f, 0.6164312f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                ),
                new Hitbox(     // arm
                    new Vector2(0.776727f, -0.1054815f),
                    new Vector2(1.364391f, 0.7123236f),
                    CLP_DATA, 1
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.2684984f, -0.07671382f),
                    new Vector2(2.227421f, 0.6164312f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LP frame 3 ====
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
            })
        },
        new int[]
        {
            5,
            7-5,
            15-7,
            23-15
        }
    );
    // === CROUCHING HEAVY PUNCH ===
    private readonly HurtboxAnimation CHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 0 ====
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
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.1821952f, 0.01917841f),
                    new Vector2(2.054814f, 0.7315018f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.3739796f, -0.1821952f),
                    new Vector2(2.054814f, 0.7123233f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                ),
                new Hitbox(     // arms
                    new Vector2(0.7383699f, -0.172606f),
                    new Vector2(1.517818f, 0.8082156f),
                    CHP_DATA, 2
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 3 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.3739796f, -0.1821952f),
                    new Vector2(2.054814f, 0.7123233f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 4 ====
                new Hurtbox(    // head
                    new Vector2(-0.03135753f, 0.3553846f),
                    new Vector2(0.7700458f, 0.7491404f)
                ),
                new Hurtbox(    // upper body and outward arm
                    new Vector2(0.1821952f, 0.01917841f),
                    new Vector2(2.054814f, 0.7315018f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.2195024f, -0.8257464f),
                    new Vector2(1.271766f, 1.898914f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HP frame 5 ====
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
            })
        },
        new int[]
        {
            2,
            10-2,
            13-10,
            19-13,
            26-19,
            29-26
        }
    );
    // === CROUCHING LIGHT KICK ===
    private readonly HurtboxAnimation CLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 0 ====
                new Hurtbox(    // head and front half
                    new Vector2(0.009588718f, -0.5178181f),
                    new Vector2(0.8273945f, 2.572633f)
                ),
                new Hurtbox(    // back half
                    new Vector2(-0.843852f, -0.8054948f),
                    new Vector2(1.076715f, 1.997279f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 1 ====
                new Hurtbox(    // head and body
                    new Vector2(0.04794574f, -0.3452121f),
                    new Vector2(0.9041085f, 2.227421f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.7671382f, -0.728781f),
                    new Vector2(1.038357f, 1.460283f)
                ),
                new Hurtbox(    // "tail"
                    new Vector2(0.3548007f, -1.524686f),
                    new Vector2(0.9424648f, 0.5205392f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 2 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                ),
                new Hitbox(     // stinger
                    new Vector2(1.160295f, -1.505508f),
                    new Vector2(1.057535f, 0.5972531f),
                    CLK_DATA, 2
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 3 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 4 ====
                new Hurtbox(    // head and body
                    new Vector2(0.04794574f, -0.3452121f),
                    new Vector2(0.9041085f, 2.227421f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.7671382f, -0.728781f),
                    new Vector2(1.038357f, 1.460283f)
                ),
                new Hurtbox(    // "tail"
                    new Vector2(0.3548007f, -1.524686f),
                    new Vector2(0.9424648f, 0.5205392f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.LK frame 5 ====
                new Hurtbox(    // head and front half
                    new Vector2(0.009588718f, -0.5178181f),
                    new Vector2(0.8273945f, 2.572633f)
                ),
                new Hurtbox(    // back half
                    new Vector2(-0.843852f, -0.8054948f),
                    new Vector2(1.076715f, 1.997279f)
                )
            })
        },
        new int[]
        {
            5,
            7-5,
            10-7,
            14-10,
            18-14,
            22-18
        }
    );
    // === CROUCHING HEAVY KICK ===
    private readonly HurtboxAnimation CHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 0 ====
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
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 1 ====
                new Hurtbox(    // head and front half
                    new Vector2(0.009588718f, -0.5178181f),
                    new Vector2(0.8273945f, 2.572633f)
                ),
                new Hurtbox(    // back half
                    new Vector2(-0.843852f, -0.8054948f),
                    new Vector2(1.076715f, 1.997279f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 2 ====
                new Hurtbox(    // head and body
                    new Vector2(0.04794574f, -0.3452121f),
                    new Vector2(0.9041085f, 2.227421f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.7671382f, -0.728781f),
                    new Vector2(1.038357f, 1.460283f)
                ),
                new Hurtbox(    // "tail"
                    new Vector2(0.3548007f, -1.524686f),
                    new Vector2(0.9424648f, 0.5205392f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 3 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                ),
                new Hitbox(     // stinger
                    new Vector2(1.160295f, -1.505508f),
                    new Vector2(1.057535f, 0.5972531f),
                    CHK_DATA[0], 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 4 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 5 ====
                new Hurtbox(    // upper body and arms
                    new Vector2(-0.4595587f, -0.7251158f),
                    new Vector2(1.198349f, 2.048934f)
                ),
                new Hurtbox(    // base sting
                    new Vector2(0.3478112f, -1.24336f),
                    new Vector2(0.8928566f, 0.9906247f)
                ),
                new Hurtbox(    // sting tip
                    new Vector2(1.00789f, -1.488844f),
                    new Vector2(0.6200957f, 0.3905525f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 6 ====
                new Hurtbox(    // upper body
                    new Vector2(-0.6832221f, -1.019697f),
                    new Vector2(1.383824f, 1.416131f)
                ),
                new Hurtbox(    // lower "body"
                    new Vector2(0.4896464f, -1.39065f),
                    new Vector2(1.045602f, 0.9578936f)
                ),
                new Hurtbox(    // stinger tip
                    new Vector2(1.25883f, -1.112435f),
                    new Vector2(0.5109921f, 0.7287751f)
                ),
                new Hitbox(     // stinger
                    new Vector2(0.9915252f, -1.145167f),
                    new Vector2(1.220168f, 1.361578f),
                    CHK_DATA[1], 6
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 7 ====
                new Hurtbox(    // upper body
                    new Vector2(-0.6832221f, -1.019697f),
                    new Vector2(1.383824f, 1.416131f)
                ),
                new Hurtbox(    // lower "body"
                    new Vector2(0.4896464f, -1.39065f),
                    new Vector2(1.045602f, 0.9578936f)
                ),
                new Hurtbox(    // stinger tip
                    new Vector2(1.25883f, -1.112435f),
                    new Vector2(0.5109921f, 0.7287751f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 8 ====
                new Hurtbox(    // upper body and arms
                    new Vector2(-0.4595587f, -0.7251158f),
                    new Vector2(1.198349f, 2.048934f)
                ),
                new Hurtbox(    // base sting
                    new Vector2(0.3478112f, -1.24336f),
                    new Vector2(0.8928566f, 0.9906247f)
                ),
                new Hurtbox(    // sting tip
                    new Vector2(1.00789f, -1.488844f),
                    new Vector2(0.6200957f, 0.3905525f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 9 ====
                new Hurtbox(    // head and body
                    new Vector2(0.2013731f, -0.5657641f),
                    new Vector2(0.9424648f, 2.323313f)
                ),
                new Hurtbox(    // back arm
                    new Vector2(-0.5274079f, -0.949333f),
                    new Vector2(0.7506804f, 1.556175f)
                ),
                new Hurtbox(    // bottom stinger piece idk lol
                    new Vector2(0.7671373f, -1.304134f),
                    new Vector2(1.191784f, 0.8465729f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 10 ====
                new Hurtbox(    // head and body
                    new Vector2(0.04794574f, -0.3452121f),
                    new Vector2(0.9041085f, 2.227421f)
                ),
                new Hurtbox(    // arms
                    new Vector2(-0.7671382f, -0.728781f),
                    new Vector2(1.038357f, 1.460283f)
                ),
                new Hurtbox(    // "tail"
                    new Vector2(0.3548007f, -1.524686f),
                    new Vector2(0.9424648f, 0.5205392f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 11 ====
                new Hurtbox(    // head and front half
                    new Vector2(0.009588718f, -0.5178181f),
                    new Vector2(0.8273945f, 2.572633f)
                ),
                new Hurtbox(    // back half
                    new Vector2(-0.843852f, -0.8054948f),
                    new Vector2(1.076715f, 1.997279f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== c.HK frame 12 ====
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
            })
        },
        new int[]
        {
            2,
            9-2,
            14-9,
            17-14,
            24-17,
            26-24,
            28-26,
            32-28,
            34-32,
            38-34,
            42-38,
            46-42,
            47-46
        }
    );
    // === JUMPING LIGHT PUNCH ===
    private readonly HurtboxAnimation JLP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 0 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body and arms
                    new Vector2(0.1633818f, -0.2498774f),
                    new Vector2(1.518976f, 0.9231145f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 3 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body and arms
                    new Vector2(0.1633818f, -0.2498774f),
                    new Vector2(1.518976f, 0.9231145f)
                ),
                new Hitbox(     // punching arms
                    new Vector2(0.4324808f, -0.4997549f),
                    new Vector2(1.365205f, 0.8077864f),
                    JLP_DATA, 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 4 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body and arms
                    new Vector2(0.1633818f, -0.2498774f),
                    new Vector2(1.518976f, 0.9231145f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 5 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 6 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            })
        },
        new int[]
        {
            2,
            7-2,
            8-7,
            10-8,
            13-10,
            16-13,
            18-16,
            19-18
        }
    );
    // === JUMPING LIGHT PUNCH ===
    private readonly HurtboxAnimation JHP_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 0 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                ),
                new Hurtbox(    // here comes a giant fist
                    new Vector2(-0.1249382f, -0.4613122f),
                    new Vector2(0.5963511f, 0.7309009f)
                ),
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HP frame 3 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                ),
                new Hurtbox(    // here comes a giant fist
                    new Vector2(-0.009610176f, -0.5093656f),
                    new Vector2(0.6732368f, 0.8270075f)
                ),
                new Hitbox(     // the fist in question
                    new Vector2(0.1153286f, -0.5478082f),
                    new Vector2(1.038443f, 0.9807786f),
                    JHP_DATA, 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HP frame 4 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                ),
                new Hurtbox(    // here comes a giant fist
                    new Vector2(-0.009610176f, -0.5093656f),
                    new Vector2(0.6732368f, 0.8270075f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 5 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                ),
                new Hurtbox(    // here comes a giant fist
                    new Vector2(-0.1249382f, -0.4613122f),
                    new Vector2(0.5963511f, 0.7309009f)
                ),
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 6 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 7 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
        },
        new int[]
        {
            4,
            11-4,
            12-11,
            16-12,
            21-16,
            26-21,
            35-26,
            37-35
        }
    );
    // === JUMPING LIGHT KICK ===
    private readonly HurtboxAnimation JLK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 0 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 2 ====
                new Hurtbox(    // head
                    new Vector2(0.2690992f, 0.1537711f),
                    new Vector2(0.7693443f, 0.7693437f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.221046f, -0.1633812f),
                    new Vector2(1.288321f, 0.7116798f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LK frame 3 ====
                new Hurtbox(    // head
                    new Vector2(0.4228699f, 0.1537711f),
                    new Vector2(0.7693439f, 0.7693437f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.2979312f, -0.1922132f),
                    new Vector2(1.326763f, 0.6540155f)
                ),
                new Hitbox(     // stabbers
                    new Vector2(0.413259f, -0.5958617f),
                    new Vector2(1.134549f, 0.8462291f),
                    JLK_DATA, 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LK frame 4 ====
                new Hurtbox(    // head
                    new Vector2(0.4228699f, 0.1537711f),
                    new Vector2(0.7693439f, 0.7693437f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.2979312f, -0.1922132f),
                    new Vector2(1.326763f, 0.6540155f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 5 ====
                new Hurtbox(    // head
                    new Vector2(0.2690992f, 0.1537711f),
                    new Vector2(0.7693443f, 0.7693437f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.221046f, -0.1633812f),
                    new Vector2(1.288321f, 0.7116798f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.LP frame 6 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            })
        },
        new int[]
        {
            2,
            7-2,
            11-7,
            17-11,
            20-17,
            27-20,
            30-27
        }
    );
    // === JUMPING HEAVY KICK ===
    private readonly HurtboxAnimation JHK_FRAMES = new HurtboxAnimation(
        new HurtboxFrame[]
        {
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 0 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 1 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 2 ====
                new Hurtbox(    // all
                    new Vector2(0.07688522f, 0.04805352f),
                    new Vector2(1.115328f, 1.019221f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 3 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[0], 3
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 4 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[1], 4
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 5 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[2], 5
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 6 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[3], 6
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 7 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[4], 7
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 8 ====
                new Hurtbox(    // all
                    new Vector2(0.2306561f, -0.03844261f),
                    new Vector2(1.192213f, 1.307542f)
                ),
                new Hitbox(     // drill
                    new Vector2(0.5189764f, -0.4036482f),
                    new Vector2(1.115327f, 1.038443f),
                    JHK_DATA[5], 8
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 9 ====
                new Hurtbox(    // all
                    new Vector2(0.07688522f, 0.04805352f),
                    new Vector2(1.115328f, 1.019221f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 10 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(0.02883244f, -0.1345493f),
                    new Vector2(1.249877f, 0.6924584f)
                )
            }),
            new HurtboxFrame(new Hurtbox[] {    // ==== j.HK frame 11 ====
                new Hurtbox(    // head
                    new Vector2(0.06727505f, 0.2114349f),
                    new Vector2(0.7501221f, 0.7693438f)
                ),
                new Hurtbox(    // body
                    new Vector2(-0.04805326f, -0.06727464f),
                    new Vector2(1.595861f, 0.7116797f)
                )
            }),
        },
        new int[]
        {
            2,
            10-2,
            15-10,
            19-15,
            23-19,
            27-23,
            31-27,
            35-31,
            39-35,
            44-39,
            48-44,
            50-48
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
