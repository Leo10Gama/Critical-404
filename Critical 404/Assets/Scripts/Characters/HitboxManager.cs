using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{

    private List<BoxCollider2D> activeHurtboxes = new List<BoxCollider2D>();
    private List<BoxCollider2D> activeHitboxes = new List<BoxCollider2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     *  Create a hurtbox that belongs to a given GameObject at given 
     *  coordinates and with a given scale.
     */
    public void CreateHurtbox(GameObject parent, Vector2 coords, Vector2 scale, int lifespan)
    {
        BoxCollider2D col = parent.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.offset = coords;
        col.size = scale;
        activeHurtboxes.Add(col);
        StartCoroutine(DeleteColliderAfterLifespan(col, lifespan));
    }

    /**
     *  Create a hurtbox that belongs to a given GameObject given
     *  it as a Hurtbox object.
     */
    public void CreateHurtbox(GameObject parent, Hurtbox hurtbox, int flipMultiplier, int lifespan)
    {
        BoxCollider2D col = parent.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.offset = new Vector2(hurtbox.offset.x * flipMultiplier, hurtbox.offset.y);
        col.size = hurtbox.scale;
        activeHurtboxes.Add(col);
        StartCoroutine(DeleteColliderAfterLifespan(col, lifespan));
    }

    /**
     *  Create a hitbox that belongs to a given GameObject given
     *  it as a Hitbox object.
     */
    public void CreateHitbox(GameObject parent, Hitbox hitbox, int flipMultiplier, int lifespan)
    {
        GameObject hitboxObject = new GameObject();
        hitboxObject.transform.parent = parent.transform;
        BoxCollider2D col = hitboxObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.offset = new Vector2(
            parent.transform.position.x + (hitbox.offset.x * flipMultiplier),
            parent.transform.position.y + hitbox.offset.y
        );
        col.size = hitbox.scale;
        activeHitboxes.Add(col);
        HitboxComponent hbc = hitboxObject.AddComponent<HitboxComponent>();
        hbc.hitbox = hitbox;
        StartCoroutine(DeleteGameObjectAfterLifespan(hitboxObject, lifespan));
    }

    IEnumerator DeleteColliderAfterLifespan(BoxCollider2D collider, int lifespan)
    {
        yield return new WaitForSeconds(lifespan / 60f);
        Destroy(collider);
    }

    public void ClearColliders()
    {
        foreach (BoxCollider2D c in activeHurtboxes)
        {
            Destroy(c);
        }
        activeHurtboxes = new List<BoxCollider2D>();
        ClearHitboxes();
    }

    public void ClearHitboxes()
    {
        foreach (BoxCollider2D c in activeHitboxes)
        {
            Destroy(c);
        }
        activeHitboxes = new List<BoxCollider2D>();
    }

    IEnumerator DeleteGameObjectAfterLifespan(GameObject obj, int lifespan)
    {
        yield return new WaitForSeconds(lifespan / 60f);
        Destroy(obj);
    }

    /**
     *  Clear all hitboxes and hurtboxes from a given parent GameObject.
     */
    public void ClearAll(GameObject parent)
    {
        foreach (BoxCollider2D hitbox in parent.transform.GetComponents<BoxCollider2D>())
        {
            Destroy(hitbox);
        }
    }
}
