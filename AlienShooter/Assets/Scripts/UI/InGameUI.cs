using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject moveStick;
    [SerializeField] private GameObject fireStick;

    public void SetEnableSticks()
    {
        moveStick.SetActive(true);
        fireStick.SetActive(true);
    }
    public void SetDisableSticks()
    {
        moveStick.SetActive(false);
        fireStick.SetActive(false);
    }
}
