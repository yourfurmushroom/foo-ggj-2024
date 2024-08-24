using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public float _speed = 0.1f;
    [SerializeField] private Transform[] _backgrounds; 

    private Vector3 startPosition;
    private int _count;
    
    private float _startTime;
    private float[] _heights;
    private float[] _startPosY;
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
    }
    void Start()
    {

        Debug.Log(17.56 % 8);
    }
    public void FixedUpdate()
    {
        OnUpdate(Time.time);
    }

    public void Initialize(float startTime)
    {
        _startTime = startTime;
    }
    public void OnUpdate(float time)
    {
        var timeElapse = time - _startTime;
        var totalHeight = _heights.Sum();
        Debug.Log(totalHeight);
        timeElapse %= (totalHeight / _speed);
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            _backgrounds[i].position =  Vector2.up * (_startPosY[i] + _speed * timeElapse);
        }
        if (_backgrounds[_count-1].position.y > 0)
        {
            _backgrounds[0].position -= totalHeight * Vector3.up;
        }
    }
    //private void Rotate()
    //{
    //    var bg = _backgrounds[0];
    //    for (int i = 0; i < _count-1; i++)
    //    {
    //        _backgrounds[i] = _backgrounds[i + 1];
    //    }
    //    _backgrounds[_count - 1] = bg;
    //    UpdateHeightAndPosY();
    //}
    private void UpdateHeightAndPosY()
    {
        for (int i = 0; i < _count; i++)
        {
            var sprite = _backgrounds[i].GetComponent<SpriteRenderer>().sprite;
            _heights[i] = sprite.texture.height / sprite.pixelsPerUnit;
        }
        for (int i = 1; i < _count; i++)
        {
           
            Debug.Log($"{i}: {_heights[i]}");
            _startPosY[i] =
                _startPosY[i - 1] -
                _heights[i - 1] / 2 -
                _heights[i] / 2;
        }
    }
}

