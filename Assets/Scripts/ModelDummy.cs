using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDummy : MonoBehaviour
{
    public GameObject object1;

    public GameObject object2;

    private void Start()
    {
        if (!AppManager._instance.selectedObj)
        {
            object1.SetActive(true);
            object1.SetActive(false);
        } else
        {
            object1.SetActive(false);
            object1.SetActive(true);
        }
    }
}
