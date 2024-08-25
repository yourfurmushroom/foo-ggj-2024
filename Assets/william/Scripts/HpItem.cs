using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : Item
{
    //改變生命力的值
    public float value = 1.0f;
    public SpeedAttribute speedAttribute;
    public HpAttribute hpAttribute;
    public override void Init(Dictionary<string, Attribute> dic)
    {
        base.Init(dic);
        speedAttribute = attributeDic["SpeedAttribute"] as SpeedAttribute;
        hpAttribute = attributeDic["HpAttribute"] as HpAttribute;
    }

    public override void Move()
    {
        // Debug.Log("HpItem Move");
        //往上移動一點距離，使用time.deltaTime來讓移動速度不受framerate影響
        transform.position += Vector3.up * speedAttribute.speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ItemTriggerEnter(other);
    }

    public override void ItemTriggerEnter(Collider2D other)
    {
        Debug.Log("HpItem OnTriggerEnter");
        onHitFrom?.Invoke(other.tag);
    }
    // override public void ItemCustomAction()
    // {
    //     Debug.Log("HpItem CustomAction");
    //     hpAttribute.hp += value;
    //     //判斷value是正數還是負數決定要顯示的字串
    //     if (value < 0)
    //         vfxValue = "Hp - " + Mathf.Abs(value);
    //     else
    //         vfxValue = "Hp + " + value;
    //     base.ItemCustomAction();
    // }
    override public void Remove()
    {
        Debug.Log("HpItem Remove");
        //移除物品
        Destroy(gameObject);
    }
}
