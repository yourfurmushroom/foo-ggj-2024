using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float moveLerp = .5f;
    float xDirectional;
    [SerializeField]
    float moveSpeed;

    // Update is called once per frame


    void OnUpdate(float time)
    {
        xDirectional = Input.GetAxis("Horizontal");

        var calculatedBodyPosition = CalBodyPosition(this.transform.position, this.transform.right, xDirectional,time);
        this.transform.position = Vector3.Lerp(this.transform.position, calculatedBodyPosition, moveLerp);


    }

    private Vector3 CalBodyPosition(Vector3 position, Vector3 right, float input,float time)
    {
        return position + (right * input * moveSpeed * time);
    }
}
