using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestItem : Item
{
    //改變速度的值
    public float modifySpeed = 1.0f;
    public override void Init(SpeedAttribute speed)
    {
        base.Init(speed);
        Debug.Log("TestItem Init");
    }

    public override void Move()
    {
        // Debug.Log("TestItem Move");
        //往上移動一點距離，使用time.deltaTime來讓移動速度不受framerate影響
        transform.position += Vector3.up * speedAttribute.speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ItemTriggerEnter(other);
    }

    public override void ItemTriggerEnter(Collider2D other)
    {
        Debug.Log("TestItem OnTriggerEnter");
        //如果碰到deadZoneBoxCollider，就呼叫Remove方法
        onHit?.Invoke(other.tag);
    }
    override public void ItemCustomAction()
    {
        speedAttribute.speed += modifySpeed;
        Debug.Log("TestItem CustomAction");
    }
    override public void Remove()
    {
        Debug.Log("TestItem Remove");
        //移除物品
        Destroy(gameObject);
    }
}