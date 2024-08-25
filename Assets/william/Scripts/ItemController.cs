using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemController : MonoBehaviour
{
    public movement playerMovement;
    public Transform posRoot;
    //隨機產生的position位置
    private List<Transform> points = new List<Transform>();
    //開始前的等待時間
    public float startDelay = 1.0f;
    //產生的間隔時間
    public float interval = 1.0f;

    //產生的物品prefab
    public List<GameObject> itemPrefabs;
    // public BoxCollider deadZoneBoxCollider;
    public GameObject vfxValuePrefab;
    //所有產生的物品
    private List<Item> _items = new List<Item>();
    private Dictionary<string, Attribute> attributeDic = new Dictionary<string, Attribute>();
    private bool buffActive = false;
    [SerializeField] private GameContext _context;
    private void Awake()
    {
        for (int i = 0; i < posRoot.childCount; i++)
        {
            points.Add(posRoot.GetChild(i));
        }
    }

    public void SetGameContext(GameContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 設定生命力屬性
    /// </summary>
    /// <param name="hp">傳入外部參考</param>
    public void SetHpAttribute(HpAttribute hp)
    {
        attributeDic.Add("HpAttribute", hp);
        hp.onDead += () =>
        {
            Debug.Log("Dead");
            //TODO:死亡處理
        };
    }

    /// <summary>
    /// 設定速度屬性
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeedAttribute(SpeedAttribute speed)
    {
        attributeDic.Add("SpeedAttribute", speed);
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
        StartCoroutine(GenerateItemCoroutine());
    }

    IEnumerator GenerateItemCoroutine()
    {
        yield return new WaitForSeconds(startDelay);
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

        //產生物品的數量，1次 = weight 5, 2次 = weight 3,3次 = weight 2
        int count = UnityEngine.Random.Range(0, 10) < 5 ? 1 : UnityEngine.Random.Range(0, 10) < 3 ? 2 : 3;
        //紀錄這次產生的物品
        List<GameObject> tempItems = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            //隨機選擇一個位置
            int index = UnityEngine.Random.Range(0, points.Count);
            Vector3 position = points[index].position;
            GameObject obj = itemPrefab;
            //如果這次產生的物品已經有了，就重新產生
            while (tempItems.Contains(obj))
            {
                obj = itemPrefab;
            }
            tempItems.Add(obj);
            Item item = Instantiate(obj, position, Quaternion.identity).GetComponent<Item>();
            item.Init(attributeDic);
            item.SetVFXValuePrefab(vfxValuePrefab);
            item.onHitFrom += (FromTag) =>
            {
                switch (FromTag)
                {
                    case "Player":
                        Debug.Log("Player Hit");
                        if (!buffActive)
                        {

                            string alphabetTag = item.ItemCustomAction();
                            float addTime = alphabetTag == "T" ? 10 : 2;
                            _context.time += addTime;

                            StartCoroutine(ActiveBuff(alphabetTag));
                        }
                        break;
                    case "DeadZone":
                        Debug.Log("DeadZone Hit");
                        break;
                };

                OnRemoveItem(item);
            };
            _items.Add(item);
        }

    }

    IEnumerator ActiveBuff(string alphabetTag)
    {
        buffActive = true;
        SpeedAttribute speedAttribute = attributeDic["SpeedAttribute"] as SpeedAttribute;
        float ori_speed = speedAttribute.speed;
        switch (alphabetTag)
        {
            case "F":
                //速度變兩倍
                speedAttribute.speed *= 2;
                break;
            case "S":
                //速度變一半
                speedAttribute.speed /= 2;
                break;
            case "P":
                //玩家無法移動
                playerMovement.enabled = false;
                break;
            case "D":
                //全場景變暗
                break;
            case "B":
                //全場景變亮
                break;
            case "M":
                //標示其他字母
                break;
        }
        //過5秒後取消buff
        yield return new WaitForSeconds(5);
        //速度變回原本的速度
        speedAttribute.speed = ori_speed;
        playerMovement.enabled = true;
        switch (alphabetTag)
        {
            case "F":
                break;
        }
        buffActive = false;
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
public class Attribute
{

}

[Serializable]
public class SpeedAttribute : Attribute
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
            Debug.Log("Speed: " + _speed);
        }
    }
    [SerializeField]
    private float _speed;
    public void ToZero()
    {
        _speed = 0;
    }
}

[Serializable]
public class HpAttribute : Attribute
{
    public float hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            //最大生命力為10
            if (_hp > 10)
            {
                _hp = 10;
            }
            if (_hp <= 0)
            {
                onDead?.Invoke();
                //TODO:死亡處理
            }
        }
    }
    [SerializeField]
    private float _hp;

    public Action<float> onHpChange;
    public Action onDead;
}