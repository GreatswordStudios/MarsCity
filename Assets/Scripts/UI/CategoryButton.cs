using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryButton : MonoBehaviour
{
    public GameObject menuToToggle;

    public void ToggleMenu() {
        menuToToggle.SetActive(!menuToToggle.activeSelf);
    }
}
