using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoyo : MonoBehaviour
{
    // Referencia al GameObject que representa el bache tapado
   

   
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Empujable"))
        {
            // Desactivar el bache visible
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

    }

}






