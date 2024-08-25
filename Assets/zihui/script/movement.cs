using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField]
    KeyCode menuKey;
    [SerializeField] private float moveLerp = .5f;
    float xDirectional;
    [SerializeField]
    float moveSpeed;
    public bool activeFlag = true;
    public Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeFlag)
        {
            rb.isKinematic = false;
            boxCollider2D.enabled = false;
            return;
        }
        xDirectional = Input.GetAxis("Horizontal");

        var calculatedBodyPosition = CalBodyPosition(this.transform.position, this.transform.right, xDirectional);
        this.transform.position = Vector3.Lerp(this.transform.position, calculatedBodyPosition, moveLerp);


        if (Input.GetKeyDown(menuKey))
        {
            //call menu
        }

    }

    private Vector3 CalBodyPosition(Vector3 position, Vector3 right, float input)
    {
        return position + (right * input * moveSpeed * Time.deltaTime);
    }
}
