using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
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
        StartCoroutine(DeleteColliderAfterLifespan(col, lifespan));
    }

    IEnumerator DeleteColliderAfterLifespan(BoxCollider2D collider, int lifespan)
    {
        yield return new WaitForSeconds(lifespan / 60f);
        Destroy(collider);
    }
}
