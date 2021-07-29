using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayTestScript : MonoBehaviour
{
    [SerializeField]
    public string[] testStringArray;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(testStringArray[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
