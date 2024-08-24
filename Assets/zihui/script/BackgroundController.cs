using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed = 0.5f;
    float totalHeight;  
    List<Transform> backgrounds; 

    Vector3 startPosition;
    

    private void Awake()
    {
        backgrounds = new List<Transform>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            backgrounds.Add(this.transform.GetChild(i));
        }
        startPosition = backgrounds[0].position;
        totalHeight = backgrounds[0].transform.localScale.y * backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].Translate(Vector2.up * scrollSpeed * Time.deltaTime);

            if (backgrounds[i].position.y > startPosition.y + totalHeight/3)
            {
                Vector3 newPos = backgrounds[i].position;
                newPos.y -= totalHeight;
                backgrounds[i].position = newPos;
            }
        }
    }

}

