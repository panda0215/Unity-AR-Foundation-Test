using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDummy : MonoBehaviour
{
    public GameObject object1;

    public GameObject object2;

    /// <summary>
    /// Switching Object
    /// </summary>
    /// <param name="val"></param>
    public void ChangeObj(bool val)
    {
        AppManager._instance.selectedObjType = val;
        if (!AppManager._instance.selectedObjType)
        {
            object1.SetActive(true);
            object2.SetActive(false);
        }
        else
        {
            object1.SetActive(false);
            object2.SetActive(true);
        }
    }
}
