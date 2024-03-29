using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direccion = Vector2.zero;
    private List<Transform> partesSnake;
    [SerializeField] private Transform prefabParte;
    private void Start()
    {
        partesSnake = new List<Transform>();
        partesSnake.Add(gameObject.transform);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            direccion = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            direccion = Vector2.down;
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            direccion = Vector2.left;
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            direccion = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        for(int i = partesSnake.Count - 1;i > 0;i--)
        {
            partesSnake[i].position = partesSnake[i - 1].position;
        }

        gameObject.transform.position = new Vector3(
            Mathf.Round(gameObject.transform.position.x) + direccion.x,
            Mathf.Round(gameObject.transform.position.y) + direccion.y
            , 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Comida"))
        {
            Crecer();
        } else if (collision.CompareTag("Veneno")) 
        {
            if(partesSnake.Count > 1)
            {
                Decrecer();
            }
        } else if (collision.CompareTag("Obstaculo"))
        {
            Morir();
        }
    }

    private void Crecer()
    {
        Transform parte = Instantiate(this.prefabParte);
        parte.position = partesSnake[partesSnake.Count - 1].position;
        partesSnake.Add(parte);
    }

    private void Decrecer()
    {
        Transform parte = partesSnake.Last();
        partesSnake.Remove(parte);
        Destroy(parte.gameObject);
    }
    private void Morir()
    {
        for (int i = 1; i < partesSnake.Count;i++)
        {
            Destroy(partesSnake[i].gameObject);
        }
        partesSnake.Clear();
        partesSnake.Add(gameObject.transform);
        direccion = Vector3.zero;
        gameObject.transform.position = Vector3.zero;
    }
}
