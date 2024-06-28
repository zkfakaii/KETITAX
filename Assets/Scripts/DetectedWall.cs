using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DetectedWall : MonoBehaviour
{

    public TilemapCollider2D tile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Empujable")) 
            {
            tile.isTrigger = false;
        }
        else

            {
            tile.isTrigger = true;
        }
    }
}
