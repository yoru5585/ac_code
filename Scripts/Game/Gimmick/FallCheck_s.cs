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
        // ���݈ʒu�擾
        var position = transform.position;

        // ���ݑ��x�v�Z
        var velocity = (position - _prevPosition) / Time.deltaTime;

        Debug.Log(velocity);

        // �O�t���[���ʒu���X�V
        _prevPosition = position;
    }
}