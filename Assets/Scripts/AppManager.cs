using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager _instance = null;

    public GameObject _prefab = null;

    public GameObject _placedObj = null;

    public bool selectedObj;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null)
        {
            _instance = null;
        }

        _instance = this;
    }

    /// <summary>
    /// Place object in Virtual mode
    /// </summary>
    public void PlaceObj()
    {
        GameObject go = Instantiate(_prefab, new Vector3(0f, 0f, -3f), Quaternion.identity);
        _placedObj = go;
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
    }
}
