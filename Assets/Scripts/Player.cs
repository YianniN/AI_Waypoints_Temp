using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    // Update is called once per frame
    void Update()
    {
        Vector3 delta = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            delta.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            delta.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            delta.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            delta.x += 1;
        }
        transform.position += delta.normalized * speed * Time.deltaTime;
    }
}
