using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_s : BaseBehaviour_s
{
    Vector2 lastMousePosition;
    Vector2 newAngle = Vector2.zero;
    [SerializeField] Camera mainCamera;
    [SerializeField] float jump_value = 6;
    Transform Player;
    AudioSource FootSound;
    bool flag;//�����p
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        FootSound = GameObject.Find("Player/footSound").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Controller.GetLeftShift())
        {
            if (pms.IsMoveReverse)
            {
                pms.WalkSpeed = 2;
            }
            else
            {
                pms.WalkSpeed = 6;
            }
            
        }
        else
        {
            if (pms.IsMoveReverse)
            {
                pms.WalkSpeed = 6;
            }
            else
            {
                pms.WalkSpeed = 2;
            }
            
        }

        if (Controller.GetSpaceDown())
        {
            OnJump();
        }

        OnMove();
        //CameraControll_Mouse();
        CameraControll_Gamepad();
    }

    void OnMove()
    {
        //�ړ�
        Vector2 axis = Controller.GetAxis();
        OnFootSound(axis);
        Vector3 newPos = Vector3.zero;
        newPos.z += pms.WalkSpeed * axis.y;
        newPos.x += pms.WalkSpeed * axis.x;
        Vector3 tmp = mainCamera.transform.TransformVector(newPos);
        Player.transform.position += new Vector3(tmp.x, 0, tmp.z) * Time.deltaTime;
    }

    void OnJump()
    {
        //�W�����v
        //Debug.Log("a");
        Vector3 vector = new Vector3(0, jump_value, 0);
        Player.gameObject.GetComponent<Rigidbody>().AddForce(vector, ForceMode.Impulse);
    }

    void CameraControll_Mouse()
    {
        //Debug.Log("camera");
        //���_����
        if (Controller.GetMouseDown())
        {
            //Debug.Log("pp");
            newAngle = mainCamera.transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
            
        }
        else if (Controller.GetMouseStay())
        {
            //Debug.Log("aaaa");
            if (pms.IsCameraReverse)
            {
                newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * pms.rotationSpeed.y;
                newAngle.x -= (lastMousePosition.y - Input.mousePosition.y) * pms.rotationSpeed.x;
            }
            else
            {
                newAngle.y -= (lastMousePosition.x - Input.mousePosition.x) * pms.rotationSpeed.y;
                newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * pms.rotationSpeed.x;
            }

            mainCamera.transform.localEulerAngles = newAngle;
            lastMousePosition = Input.mousePosition;
        }
    }

    void CameraControll_Gamepad()
    {
        //�����ŃQ�[���p�b�h�̃J��������
        newAngle = Controller.GetLook();
        if (pms.IsCameraReverse)
        {
            mainCamera.transform.localEulerAngles -= new Vector3(newAngle.y * pms.rotationSpeed.y, -newAngle.x * pms.rotationSpeed.x, 0) * 10;
        }
        else
        {
            mainCamera.transform.localEulerAngles += new Vector3(newAngle.y * pms.rotationSpeed.y, -newAngle.x * pms.rotationSpeed.x, 0) * 10;
        }
        
    }

    void OnFootSound(Vector2 axis)
    {
        if (axis.x == 0 && axis.y == 0)
        {
            FootSound.Stop();
            flag = true;
            //�����I�t
            return;
        }

        if (flag)
        {
            flag = false;
            FootSound.Play();
        }
    }
}