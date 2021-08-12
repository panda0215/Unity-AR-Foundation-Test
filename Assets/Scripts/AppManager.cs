using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager _instance = null;

    public GameObject _prefab = null;

    public GameObject _placedObj = null;

    public bool selectedObjType;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null)
        {
            _instance = null;
        }

        _instance = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("App Finished!");
            Application.Quit();
        }
    }

    /// <summary>
    /// Place object in Virtual mode
    /// </summary>
    public void PlaceObj()
    {
        GameObject go = Instantiate(_prefab, new Vector3(0f, 0f, -3f), Quaternion.identity);
        _placedObj = go;
        go.GetComponent<ModelDummy>().ChangeObj(selectedObjType);
    }

    /// <summary>
    /// Place object in AR mode
    /// </summary>
    /// <param name="targetPos">
    /// targetPos => Position detected by Raycast hit.
    /// </param>
    public void PlaceObj(Vector3 targetPos)
    {
        GameObject go = Instantiate(_prefab, targetPos, Quaternion.identity);
        _placedObj = go;
        go.GetComponent<ModelDummy>().ChangeObj(selectedObjType);
    }


    /// <summary>
    /// Clear Objects
    /// </summary>
    public void ClearObjs()
    {
        if (_placedObj != null)
        {
            Destroy(_placedObj);
        }
        _placedObj = null;
    }
}
