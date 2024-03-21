using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veneno : MonoBehaviour
{
    private void Start()
    {
        Cambio();
    }

    public void Cambio()
    {
        
        InvokeRepeating(nameof(PosicionAleatoria), 0, 5);
    }

    private void PosicionAleatoria()
    {
        float x = Random.Range(-23, 24);
        float y = Random.Range(-11, 12);
        gameObject.transform.position = new Vector3(
            Mathf.Round(x),
            Mathf.Round(y),
            0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PosicionAleatoria();
        }
    }
}
