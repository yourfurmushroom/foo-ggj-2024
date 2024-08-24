using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemBehavior : MonoBehaviour
{
    public List<ItemController> itemControllers;
    //speedAttribute 的 speed
    public float speed = 1.0f;
    public float hp = 100;
    public HpAttribute hpAttribute = new HpAttribute();
    public SpeedAttribute speedAttribute = new SpeedAttribute();
    // Start is called before the first frame update
    void Start()
    {
        hpAttribute.hp = hp;
        speedAttribute.speed = speed;
        foreach (var itemController in itemControllers)
        {
            itemController.SetHpAttribute(hpAttribute);
            itemController.SetSpeedAttribute(speedAttribute);
            itemController.StartAutoGenerate();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
