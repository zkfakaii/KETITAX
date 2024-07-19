using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empujable : MonoBehaviour
{
    public LayerMask layerObstaculos;


    [HideInInspector] public float lerpTime;

    public bool PuedeMoverse(Vector3 _direccion, float _lerpTime)
    {
        bool r = false;

        lerpTime = _lerpTime;
        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, _direccion, 1, layerObstaculos);
        
        if(hit2d.collider!= null)
        {
            if(hit2d.collider.CompareTag("caja"))
            {
                if(hit2d.collider.GetComponent<Empujable>().PuedeMoverse(_direccion, _lerpTime))
                {
                    StartCoroutine(Moverse(_direccion));
                    r = true;
                }
            }
            else
            {
                r = false;
            }
        }
        else
        {
            StartCoroutine(Moverse(_direccion));
            r = true;
        }

        return r;



    }



    IEnumerator Moverse(Vector3 _direccion)
    {

        Vector3 desde = transform.position;
        Vector3 hacia = desde + _direccion;
        float t = 0;

        while (t < lerpTime)
        {

            t += Time.deltaTime;
            float porcentaje = t / lerpTime;
            transform.position = Vector3.Lerp(desde, hacia, porcentaje);
            yield return null;
        }

    }
}
