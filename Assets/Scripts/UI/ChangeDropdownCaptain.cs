using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeDropdownCaptain : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public void AssignDropdownValue()
    {
        GameManager.instance.captainCount = dropdown.value;
    }
}
