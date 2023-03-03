using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    private GameObject p1;
    private GameObject p2;
    private PlayerMovement p1script;
    private PlayerMovement p2script;

    private bool[] registeringHit = {false, false};
    private GameObject turningPoint = null;

    private GameObject hitboxManager;

    void Awake()
    {
        hitboxManager = transform.Find("HitboxManager").gameObject;

        p1 = Instantiate(player1, new Vector3(-3f, 0f, 0f), Quaternion.identity);
        p2 = Instantiate(player2, new Vector3(3f, 0f, 0f), Quaternion.identity);
        p1script = p1.GetComponent<PlayerMovement>();
        p2script = p2.GetComponent<PlayerMovement>();
        p1script.SetFightManager(this.gameObject);
        p2script.SetFightManager(this.gameObject);
        p1script.playerId = 1;
        p2script.playerId = 2;
        turningPoint = transform.Find("TurningPoint").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float newPos = 0f;
        float p1x = p1.transform.position.x;
        float p2x = p2.transform.position.x;
        if (p1x > p2x)
        {
            newPos = ((p1x - p2x) / 2f) + p2x;
        }
        else
        {
            newPos = ((p2x - p1x) / 2f) + p1x;
        }
        p1script.SetTurningPoint(newPos);
        p2script.SetTurningPoint(newPos);
        turningPoint.transform.position = new Vector3(newPos, 0f, 0f);
    }

    /**
     *  Perform all necessary actions for when a player is hit. This function
     *  takes in as parameter the ID of the player who landed the attack, as
     *  well as the Hitbox that collided.
     */
    public void LandedHit(int attackedId, Hitbox hitbox)
    {
        // Blocker
        if (attackedId != 1 && attackedId != 2)
            throw new Exception(String.Format("Unknown interaction: Attacked player's ID set to '{0}'!", attackedId));

        int attackerId = attackedId == 1 ? 2 : 1;

        // Assign players from their scripts
        PlayerMovement attackingPlayer;
        PlayerMovement hitPlayer;
        if (attackerId == 1)
        {
            attackingPlayer = p1script;
            hitPlayer = p2script;
        }
        else
        {
            attackingPlayer = p2script;
            hitPlayer = p1script;
        }

        // Clear the attacking player's hitboxes (prevent double-hits)
        attackingPlayer.ClearHitboxesThisImage();
        // Set hit player into hitstun and apply damage
        hitPlayer.hp -= hitbox.damage;
        hitPlayer.hitstun = hitbox.hitstun;
        // Screenshake and hitstop effects
        // TODO
        // Particle effects
        // TODO
        Debug.Log("Hit!");
    }

    public HitboxManager GetHitboxManager()
    {
        return hitboxManager.GetComponent<HitboxManager>();
    }
}
