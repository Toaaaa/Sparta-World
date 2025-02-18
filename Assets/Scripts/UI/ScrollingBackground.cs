using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    public float xspd, yspd;// x, y축으로 얼마나 움직일지 결정.

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(xspd, yspd) * Time.deltaTime, _img.uvRect.size);
    }
}
