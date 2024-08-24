using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VFXValue : MonoBehaviour
{
    public TextMeshProUGUI alphabetText;
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //往上移動一點距離，使用time.deltaTime來讓移動速度不受framerate影響
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public void SetText(string text)
    {
        alphabetText.text = text;
    }
}
