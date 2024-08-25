using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class buffFollow : MonoBehaviour
{
    public GameObject followGameObject;
    [SerializeField] private RectTransform _buffRectTransform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (followGameObject != null)
        {
            //讓 _buffText 跟隨 buffFollow 物件在世界座標上的位置
            Camera camera = Camera.main;
            Vector3 screenPos = camera.WorldToScreenPoint(followGameObject.transform.position);
            _buffRectTransform.anchoredPosition = screenPos;
        }
    }
}
