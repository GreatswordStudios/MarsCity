using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleButton : MonoBehaviour
{
    public double timeScale = 1.0;

    public void SetTimeScale() {
        SceneMgr.singleton.tickMultiplier = timeScale;
    }
}
