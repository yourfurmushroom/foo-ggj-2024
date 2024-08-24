using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Action<string> onHit;
    public Dictionary<string, Attribute> attributeDic = new Dictionary<string, Attribute>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //初始化
    public virtual void Init(Dictionary<string, Attribute> dic)
    {
        attributeDic = dic;
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
