using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Action<string> onHit;
    public SpeedAttribute speedAttribute;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //初始化
    public virtual void Init(SpeedAttribute speed)
    {
        this.speedAttribute = speed;
    }

    public virtual void Move()
    {
    }
    public virtual void ItemCustomAction()
    {
    }
    public virtual void ItemTriggerEnter(Collider2D other)
    {
    }
    public virtual void Remove()
    {
    }
}
