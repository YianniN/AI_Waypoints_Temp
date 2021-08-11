using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAI : MonoBehaviour
{

    [SerializeField] private float speed = 2.5f;
    [SerializeField] private GameObject[] goals;
    private int goalIndex = 0;

    public bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        { return; }

        float distance = Vector2.Distance(transform.position, goals[goalIndex].transform.position);
        if (distance > 0.01f)
        {
            Vector2 direction = (goals[goalIndex].transform.position - transform.position).normalized;
            Vector2 aiPosition = transform.position;
            aiPosition += (direction * speed * Time.deltaTime);
            transform.position = aiPosition;

        }
        else
        {
            goalIndex += 1;
            if (goalIndex >= goals.Length)
            {
                goalIndex = 0;
            }
        }

    }
}
