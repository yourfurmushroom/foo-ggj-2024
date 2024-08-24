using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    float xDirectional;
    [SerializeField]
    float yDirectional;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float movementDistance;
    // Update is called once per frame
    void Update()
    {
        yDirectional = Input.GetAxis("Vertical");
        xDirectional = Input.GetAxis("Horizontal");

        var move=(this.transform.up*yDirectional+this.transform.right*xDirectional)*movementSpeed*Time.deltaTime;

        this.transform.position += move;

    }
}
