using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_s : MonoBehaviour
{
    GameObject canvas;
    [SerializeField] GameObject sound;
    [SerializeField] GameObject player;
    [SerializeField] Slider RotateSpeed;
    [SerializeField] Toggle CameraReverse;
    [SerializeField] Toggle MouseEnable;
    [SerializeField] Toggle MoveReverse;
    [SerializeField] Slider Sound;
    int index = 0;
    parameterManager_s pms;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("MainCanvas");
        pms = GameObject.Find("ParameterManager").GetComponent<parameterManager_s>();
    }

    public void OnBackButtonClicked()
    {
        canvas.transform.GetChild(index).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnSoundButtonClicked()
    {
        sound.SetActive(true);
        player.SetActive(false);
    }

    public void OnPlayerButtonClicked()
    {
        sound.SetActive(false);
        player.SetActive(true);
    }

    public void SetIndex(int i)
    {
        index = i;
    }

    public void ReverseToggle()
    {
        pms.IsCameraReverse = CameraReverse.isOn;
    }

    public void MoveReverseToggle()
    {
        pms.IsMoveReverse = MoveReverse.isOn;
    }

    public void RotateSpeedSlider()
    {
        pms.rotationSpeed.x = RotateSpeed.value / 100;
        pms.rotationSpeed.y = RotateSpeed.value / 100;
    }

    public void MouseEnableToggle()
    {
        pms.IsMouseEnable = MouseEnable.isOn;
    }

    public void SoundSlider()
    {
        pms.Mastervol = Sound.value;
    }
}
