using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemBehavior : MonoBehaviour
{
    public ItemController itemController;
    // Start is called before the first frame update
    void Start()
    {
        itemController.StartAutoGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
