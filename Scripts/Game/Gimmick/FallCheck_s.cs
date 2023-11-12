using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheck_s : MonoBehaviour
{
    Vector3 _prevPosition;
    // Start is called before the first frame update
    void Start()
    {
        _prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 現在位置取得
        var position = transform.position;

        // 現在速度計算
        var velocity = (position - _prevPosition) / Time.deltaTime;

        Debug.Log(velocity);

        // 前フレーム位置を更新
        _prevPosition = position;
    }
}
