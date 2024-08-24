using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    KeyCode menuKey;
    [SerializeField] private float moveLerp = .5f;
    [SerializeField]
    float xDirectional;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float movementDistance;
    // Update is called once per frame
    void Update()
    {
        xDirectional = Input.GetAxis("Horizontal");

        var calculatedBodyPosition = CalBodyPosition(this.transform.position, this.transform.right, xDirectional);
        this.transform.position = Vector3.Lerp(this.transform.position, calculatedBodyPosition, moveLerp);


        if(Input.GetKeyDown(menuKey))
        {
            //call menu
        }

    }

    private Vector3 CalBodyPosition(Vector3 position, Vector3 right, float input)
    {
        return position + (right * input * moveSpeed * Time.deltaTime);
    }
}
