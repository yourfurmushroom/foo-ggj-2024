using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public SpeedAttribute speedAttribute = new SpeedAttribute();
    // public float _speed = 0.1f;
    private Transform[] _backgrounds;

    private Vector3 startPosition;
    private Vector3 startPosition2;
    private int _count;

    private float _startTime;
    private float[] _heights;
    private float[] _startPosY;
    private float imageHeight;
    private void Awake()
    {

        _count = this.transform.childCount;
        _backgrounds = new Transform[_count];
        _heights = new float[_count];
        _startPosY = new float[_count];

        for (int i = 0; i < _count; i++)
        {
            _backgrounds[i] = this.transform.GetChild(i).transform;
        }
        UpdateHeightAndPosY();
        //獲取第一張圖片的位置
        startPosition = _backgrounds[0].position;
        //獲取第二張圖片的位置
        startPosition2 = _backgrounds[1].position;
        //獲取第一章圖片的高度
        imageHeight = _backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("imageHeight: " + imageHeight);
    }
    private void Update()
    {
        //控制圖片的滾動
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            //獲取圖片的當前位置
            Vector3 pos = _backgrounds[i].position;
            //控制圖片的滾動
            pos.y += speedAttribute.speed * Time.deltaTime;
            //如果圖片的位置大於等於圖片的高度，就將圖片的位置設置為重設位置
            if (pos.y + 0.02f >= startPosition.y + imageHeight)
            {
                pos.y = startPosition2.y;
            }
            //設置圖片的位置
            _backgrounds[i].position = pos;
        }
    }
    // public void FixedUpdate()
    // {
    //     OnUpdate(Time.time);
    // }

    public void Initialize(float startTime)
    {
        _startTime = startTime;
    }
    public void OnUpdate(float time)
    {
        var timeElapse = time - _startTime;
        var totalHeight = _heights.Sum();
        timeElapse %= totalHeight / speedAttribute.speed;
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            _backgrounds[i].position += Vector3.up * speedAttribute.speed * Time.deltaTime;
        }
        if (_backgrounds[_count - 1].position.y > 0)
        {
            _backgrounds[0].position -= totalHeight * Vector3.up;
        }
    }
    private void UpdateHeightAndPosY()
    {
        for (int i = 0; i < _count; i++)
        {
            var sprite = _backgrounds[i].GetComponent<SpriteRenderer>().sprite;
            _heights[i] = sprite.texture.height / sprite.pixelsPerUnit;
        }
        for (int i = 1; i < _count; i++)
        {

            _startPosY[i] =
                _startPosY[i - 1] -
                _heights[i - 1] / 2 -
                _heights[i] / 2;
        }
    }
}

