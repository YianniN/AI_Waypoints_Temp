using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAI : MonoBehaviour
{

    [SerializeField] private float speed = 2.5f;
    [SerializeField] private GameObject goal;
    [SerializeField] private GameObject goal2;
    [SerializeField] private bool goalIsFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, goal.transform.position);
        float distance2 = Vector2.Distance(transform.position, goal2.transform.position);

        if (goalIsFirst == true)
        {
            if (distance > 0.01f)
            {
                Vector2 direction = (goal.transform.position - transform.position).normalized;
                Vector2 aiPosition = transform.position;
                aiPosition += (direction * speed * Time.deltaTime);
                transform.position = aiPosition;

            }
            else
            {
                goalIsFirst = false;
            }
                  
        }
        else
        {
            if (distance2 > 0.01f)
            {
                Vector2 direction2 = (goal2.transform.position - transform.position).normalized;
                Vector2 aiPosition = transform.position;
                aiPosition += (direction2 * speed * Time.deltaTime);
                transform.position = aiPosition;
            }
            else
            {
                goalIsFirst = true;
            }
        }
    }
}
