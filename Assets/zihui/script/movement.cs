using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    KeyCode menuKey;

    [SerializeField]
    float xDirectional;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float movementDistance;
    // Update is called once per frame
    void Update()
    {
        xDirectional = Input.GetAxis("Horizontal");

        var move=(this.transform.right*xDirectional)*movementSpeed*Time.deltaTime;

        this.transform.position += move;

        if(Input.GetKeyDown(menuKey))
        {
            //call menu
        }

    }
}
