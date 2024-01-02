using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;

public abstract class SceneUIBase : MonoBehaviour
{
    public UIType myUIType;

    public abstract void Init();
    public abstract void SetUp();
}
