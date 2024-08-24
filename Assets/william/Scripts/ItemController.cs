using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemController : MonoBehaviour
{
    public Transform posRoot;
    //隨機產生的position位置
    private List<Transform> points = new List<Transform>();
    //產生的間隔時間
    public float interval = 1.0f;
    //speedAttribute 的 speed
    public float speed = 1.0f;
    //產生的物品prefab
    public List<GameObject> itemPrefabs;
    public BoxCollider deadZoneBoxCollider;
    //所有產生的物品
    private List<Item> _items = new List<Item>();
    private SpeedAttribute speedAttribute = new SpeedAttribute();

    private void Awake()
    {
        for (int i = 0; i < posRoot.childCount; i++)
        {
            points.Add(posRoot.GetChild(i));
        }
        speedAttribute.speed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_items.Count == 0)
        {
            return;
        }
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] != null)
                _items[i].Move();
        }
    }

    //開始自動產生物品
    public void StartAutoGenerate()
    {
        //用InvokeRepeating來重複呼叫GenerateItem方法
        InvokeRepeating("GenerateItem", 0, interval);
    }

    //停止自動產生物品
    public void StopAutoGenerate()
    {
        //停止InvokeRepeating
        CancelInvoke("GenerateItem");
    }

    //產生物品
    public void GenerateItem()
    {
        //隨機選擇一個位置
        int index = UnityEngine.Random.Range(0, points.Count);
        Vector3 position = points[index].position;
        //產生物品
        Item item = Instantiate(itemPrefab, position, Quaternion.identity).GetComponent<Item>();
        item.Init(speedAttribute);
        item.onHit += (itemTag) =>
        {
            switch (itemTag)
            {
                case "Player":
                    Debug.Log("Player Hit");
                    item.ItemCustomAction();
                    break;
                case "DeadZone":
                    Debug.Log("DeadZone Hit");
                    break;
            };

            OnRemoveItem(item);
        };
        _items.Add(item);
    }

    public void OnRemoveItem(Item item)
    {
        _items.Remove(item);
        item.Remove();
    }

    private GameObject itemPrefab
    {
        get
        {
            return itemPrefabs[UnityEngine.Random.Range(0, itemPrefabs.Count)];
        }
    }
}

[Serializable]
public class SpeedAttribute
{
    public float speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            //最大速度為10
            if (_speed > 10)
            {
                _speed = 10;
            }
            //最小速度為1
            if (_speed < 1)
            {
                _speed = 1;
            }
        }
    }
    private float _speed;
}
