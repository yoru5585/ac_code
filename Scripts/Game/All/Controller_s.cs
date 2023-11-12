using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller_s : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector2 move;
    Vector2 look;
    parameterManager_s pms;

    bool IsMouseButtonDown;
    bool IsMouseButtonStay;
    bool IsEscapeDown;
    bool IsActionDown;
    bool IsLeftShift;
    bool IsDebugDown;
    bool IsRestartDown;
    bool IsSpaceDown;
    bool IsCentralDown;
    // Start is called before the first frame update
    void Start()
    {
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
        Init();
    }

    private void Update()
    {

        //Action使うようになって不要になりました。
        if (pms.IsStop)
        {
            return;
        }

        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    IsEscapeDown = true;
        //}

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    IsLeftShift = true;
        //}

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    IsDebugDown = true;
        //}

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            IsMouseButtonDown = true;
            //Debug.Log("d");
        }

        if (Mouse.current.rightButton.IsPressed())
        {
            IsMouseButtonStay = true;
            //Debug.Log("s");
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    IsActionDown = true;
        //}

        //if (Input.GetMouseButtonDown(2))
        //{
        //    IsCentralDown = true;
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    IsRestartDown = true;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    IsSpaceDown = true;
        //}
    }

    private void LateUpdate()
    {
        Init();
    }

    public Vector2 GetAxis()
    {
        return new Vector2(horizontal, vertical);
    }

    public bool GetMouseDown()
    {
        return IsMouseButtonDown;
    }

    public bool GetMouseStay()
    {
        return IsMouseButtonStay;
    }

    public bool GetActionDown()
    {
        return IsActionDown;
    }

    public bool GetEscapeDown()
    {
        return IsEscapeDown;
    }

    public bool GetLeftShift()
    {
        return IsLeftShift;
    }

    public bool GetDebugDown()
    {
        return IsDebugDown;
    }

    public bool GetRestartDown()
    {
        return IsRestartDown;
    }

    public bool GetSpaceDown()
    {
        return IsSpaceDown;
    }

    public bool GetCentralDown()
    {
        return IsCentralDown;
    }

    public Vector2 GetLook()
    {
        return look;
    }

    void Init()
    {
        IsMouseButtonDown = false;
        IsMouseButtonStay = false;
        IsActionDown = false;
        IsEscapeDown = false;
        IsDebugDown = false;
        IsRestartDown = false;
        IsSpaceDown = false;
        IsCentralDown = false;
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }
        look = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }

        move = context.ReadValue<Vector2>();
        horizontal = move.x;
        vertical = move.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }

        if (context.phase == InputActionPhase.Performed && IsSpaceDown != true)
        {
            IsSpaceDown = true;
        }
    }

    public void OnRestartInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }

        if (context.phase == InputActionPhase.Performed && IsRestartDown != true)
        {
            IsRestartDown = true;
        }
    }

    public void OnLensInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }

        if (context.phase == InputActionPhase.Performed && IsCentralDown != true)
        {
            IsCentralDown = true;
        }
    }

    public void OnActionInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }

        if (context.phase == InputActionPhase.Performed && IsActionDown != true)
        {
            IsActionDown = true;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }

        if (context.phase == InputActionPhase.Canceled)
        {
            IsLeftShift = false;
        }

        if (context.phase == InputActionPhase.Started)
        {
            IsLeftShift = true;
        }
    }

    public void OnEscapeInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }

        if (context.phase == InputActionPhase.Performed && IsEscapeDown != true)
        {
            IsEscapeDown = true;
        }
    }

    public void OnDebugInput(InputAction.CallbackContext context)
    {
        if (pms.IsStop) { return; }

        if (context.phase == InputActionPhase.Performed && IsDebugDown != true)
        {
            IsDebugDown = true;
        }
    }

}
