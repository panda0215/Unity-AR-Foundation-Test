using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseObj : MonoBehaviour
{
    [SerializeField] GameObject choose_1st_obj;
    
    [SerializeField] GameObject choose_2nd_obj;


    /// <summary>
    /// Choose Object
    /// </summary>
    /// <param name="val">
    /// false => 1st obj
    /// true => 2nd obj
    /// </param>
    public void ChooseObject(bool val)
    {
        AppManager._instance.selectedObjType = val;
        switch (val)
        {
            case false:
                GetReadyObj(choose_1st_obj, choose_2nd_obj);
                break;
            case true:
                GetReadyObj(choose_2nd_obj, choose_1st_obj);
                break;
        }
    }

    /// <summary>
    /// Getting Ready the object to show
    /// </summary>
    /// <param name="choosedObj"></param>
    /// <param name="releasedObj"></param>
    void GetReadyObj(GameObject choosedObj, GameObject releasedObj)
    {
        choosedObj.GetComponent<Image>().color = UIController._instance.colGreen;
        releasedObj.GetComponent<Image>().color = UIController._instance.colWhite;

        UIController._instance.GetReadyBtnShowObj();
    }

    /// <summary>
    /// Release Choosed Object
    /// </summary>
    public void ReleaseSeletedObjects()
    {
        choose_1st_obj.GetComponent<Image>().color = UIController._instance.colWhite;
        choose_2nd_obj.GetComponent<Image>().color = UIController._instance.colWhite;
    }
}


/// <summary>
/// To expand project for online system and integration with back-api
/// </summary>
public class PlaceObj : MonoBehaviour
{
    public Sprite sprite_1;
    public Sprite sprite_2;
    public string objName_1;
    public string objName_2;

    /// When managing objs dynamically using assetbundle

    //public GameObject Obj1;
    //public GameObject Obj2;



    public PlaceObj()
    {
        this.sprite_1 = null;
        this.sprite_2 = null;
        this.objName_1 = "";
        this.objName_2 = "";

        /// When managing objs dynamically using assetbundle

        //this.Obj1 = null;
        //this.Obj2 = null;
    }

    public PlaceObj(Sprite _spr1, Sprite _spr2, string name1, string name2)
    {
        this.sprite_1 = _spr1;
        this.sprite_2 = _spr2;
        this.objName_1 = name1;
        this.objName_2 = name2;

        /// When managing objs dynamically using assetbundle

        //this.Obj1 = obj1;
        //this.Obj2 = obj2;
    }
}
