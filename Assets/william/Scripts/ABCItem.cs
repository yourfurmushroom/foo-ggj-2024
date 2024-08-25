using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ABCItem : Item
{
    //改變生命力的值
    public float value = 1.0f;
    public SpeedAttribute speedAttribute;

    public TextMeshProUGUI alphabetText;
    public override void Init(Dictionary<string, Attribute> dic)
    {
        base.Init(dic);
        speedAttribute = attributeDic["SpeedAttribute"] as SpeedAttribute;
        alphabetText.text = alphabetTag;
    }

    public override void Move()
    {
        //往上移動一點距離，使用time.deltaTime來讓移動速度不受framerate影響
        transform.position += Vector3.up * speedAttribute.speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ItemTriggerEnter(other);
    }

    public override void ItemTriggerEnter(Collider2D other)
    {
        onHitFrom?.Invoke(other.tag);
    }
    override public string ItemCustomAction()
    {
        Debug.Log("ABCItem CustomAction get alphabetTag: " + alphabetTag);
        vfxValue = "Got " + alphabetTag + "!";

        return base.ItemCustomAction();
    }
    override public void Remove()
    {
        //移除物品
        Destroy(gameObject);
    }
}
