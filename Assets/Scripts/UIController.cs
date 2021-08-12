using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    public static UIController _instance = null;

    [Header("Panels")]

    [SerializeField] GameObject panel_TakeTour = null;

    [SerializeField] GameObject panel_Main = null;

    [SerializeField] GameObject panel_ChooseObj = null;

    [SerializeField] GameObject panel_TapToPlaceObj = null;

    [SerializeField] GameObject panel_ChangeObj = null;

    [Header("Buttons")]

    [SerializeField] GameObject btn_home = null;

    [SerializeField] GameObject btn_clear = null;

    [SerializeField] GameObject btn_ShowObject = null;

    [SerializeField] GameObject btn_Back = null;

    [Header("Sprites")]

    [SerializeField] Sprite btn_homeOn = null;

    [SerializeField] Sprite btn_homeOff = null;

    [SerializeField] Sprite btn_placeObjsOn = null;

    [SerializeField] Sprite btn_placeObjsOff = null;
        
    [SerializeField] Sprite icon_plusOn = null;

    [SerializeField] Sprite icon_plusOff = null;

    [Header("Colors")]

    public Color colGreen = Color.clear;
    
    public Color colWhite = Color.clear;
    
    public Color colBlack = Color.clear;
    
    public Color colEmpty = Color.clear;

    [Header("Others")]

    [SerializeField] ScrollSnap scrSnap = null;

    [SerializeField] ToggleController showObjCtrl = null;

    [SerializeField] float animTime = 1.0f;

    private bool isMainPanel = false;


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
    /// when click btn_takeATour..
    /// </summary>
    public void TakeATour()
    {
        btn_home.GetComponent<Image>().sprite = btn_homeOn;
        panel_Main.SetActive(true);
        btn_ShowObject.GetComponent<Button>().onClick.AddListener(ShowSelectObjPanel);
        scrSnap.SnapToIndex(0);

        panel_TakeTour.SetActive(false);
        isMainPanel = true;
    }

    /// <summary>
    /// Back to homePanel when click btn_home
    /// </summary>
    public void BackToHome()
    {
        if (!isMainPanel) return;

        btn_home.GetComponent<Image>().sprite = btn_homeOff;

        btn_ShowObject.GetComponent<RectTransform>().DOLocalMoveX(0f, animTime).SetEase(Ease.Linear);
        btn_ShowObject.GetComponent<Image>().sprite = btn_placeObjsOff;
        btn_ShowObject.GetComponent<Image>().color = colWhite;
        btn_ShowObject.transform.GetChild(0).GetComponent<Text>().color = colBlack;
        btn_ShowObject.transform.GetChild(1).GetComponent<Image>().color = colBlack;
        
        btn_ShowObject.SetActive(true);
        btn_ShowObject.GetComponent<Button>().onClick.RemoveAllListeners();
        btn_ShowObject.GetComponent<Button>().onClick.AddListener(ShowSelectObjPanel);

        btn_Back.GetComponent<CanvasGroup>().alpha = 0.0f;
        btn_Back.SetActive(true);
        btn_Back.GetComponent<Button>().onClick.RemoveAllListeners();

        AppManager._instance.ClearObjs();
        AppManager._instance.selectedObjType = false;

        panel_Main.GetComponent<Image>().enabled = false;
        panel_ChooseObj.GetComponent<ChooseObj>().ReleaseSeletedObjects();
        panel_ChooseObj.SetActive(false);
        panel_TapToPlaceObj.SetActive(false);
        panel_ChangeObj.SetActive(false);

        panel_Main.SetActive(false);
        panel_TakeTour.SetActive(true);
        isMainPanel = false;
    }


    /// <summary>
    /// Show select panel when click btn_placeObject
    /// </summary>
    public void ShowSelectObjPanel()
    {
        btn_ShowObject.GetComponent<RectTransform>().DOLocalMoveX(100f, animTime).SetEase(Ease.Linear);
        btn_ShowObject.GetComponent<Image>().sprite = btn_placeObjsOn;
        btn_ShowObject.transform.GetChild(1).GetComponent<Image>().sprite = icon_plusOn;
        btn_ShowObject.transform.GetChild(0).GetComponent<Text>().color = colWhite;
        btn_ShowObject.transform.GetChild(1).GetComponent<Image>().color = colWhite;

        btn_Back.GetComponent<CanvasGroup>().DOFade(1.0f, animTime).SetEase(Ease.Linear);
        btn_Back.GetComponent<Button>().onClick.AddListener(HideSelectObjPanel);

        panel_Main.GetComponent<Image>().enabled = true;
        panel_ChooseObj.SetActive(true);
    }

    /// <summary>
    /// Hide select panel when click btn_back
    /// </summary>
    public void HideSelectObjPanel()
    {
        btn_ShowObject.GetComponent<RectTransform>().DOLocalMoveX(0f, animTime).SetEase(Ease.Linear);
        btn_ShowObject.GetComponent<Image>().sprite = btn_placeObjsOff;
        btn_ShowObject.transform.GetChild(1).GetComponent<Image>().sprite = icon_plusOff;
        btn_ShowObject.transform.GetChild(0).GetComponent<Text>().color = colBlack;
        btn_ShowObject.transform.GetChild(1).GetComponent<Image>().color = colBlack;
        
        btn_Back.GetComponent<CanvasGroup>().DOFade(0.0f, animTime).SetEase(Ease.Linear);
        btn_Back.GetComponent<Button>().onClick.RemoveAllListeners();

        ReleaseBtnShowObj();
        panel_Main.GetComponent<Image>().enabled = false;
        panel_ChooseObj.SetActive(false);
    }

    /// <summary>
    /// Getting ready to tap to show object.
    /// </summary>
    public void GetReadyBtnShowObj()
    {
        btn_ShowObject.GetComponent<Image>().sprite = btn_placeObjsOff;
        btn_ShowObject.GetComponent<Image>().color = colGreen;
        btn_ShowObject.GetComponent<Button>().onClick.RemoveAllListeners();
        btn_ShowObject.GetComponent<Button>().onClick.AddListener(ShowTapToPlaceObjPanel);
    }

    /// <summary>
    /// Release BtnShowObject
    /// </summary>
    void ReleaseBtnShowObj()
    {
        panel_ChooseObj.GetComponent<ChooseObj>().ReleaseSeletedObjects();

        btn_ShowObject.GetComponent<Image>().sprite = btn_placeObjsOff;
        btn_ShowObject.GetComponent<Image>().color = colWhite;

        btn_ShowObject.GetComponent<Button>().onClick.RemoveAllListeners();
        btn_ShowObject.GetComponent<Button>().onClick.AddListener(ShowSelectObjPanel);
    }

    /// <summary>
    /// Show Tap To Place Panel
    /// </summary>
    void ShowTapToPlaceObjPanel()
    {
        panel_ChooseObj.SetActive(false);
        panel_Main.GetComponent<Image>().enabled = false;

        btn_ShowObject.SetActive(false);
        btn_Back.SetActive(false);

        panel_TapToPlaceObj.SetActive(true);

        if(!AppManager._instance.selectedObjType)
        {
            panel_TapToPlaceObj.transform.GetChild(0).gameObject.SetActive(true);
            panel_TapToPlaceObj.transform.GetChild(1).gameObject.SetActive(false);
        } else
        {
            panel_TapToPlaceObj.transform.GetChild(0).gameObject.SetActive(false);
            panel_TapToPlaceObj.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Hide Tap To Place Panel
    /// </summary>
    public void HideTapToPlaceObjPanel()
    {
        panel_ChooseObj.SetActive(true);
        panel_Main.GetComponent<Image>().enabled = true;

        btn_ShowObject.SetActive(true);
        btn_Back.SetActive(true);

        panel_TapToPlaceObj.transform.GetChild(0).gameObject.SetActive(false);
        panel_TapToPlaceObj.transform.GetChild(1).gameObject.SetActive(false);
        panel_TapToPlaceObj.SetActive(false);
    }

    /// <summary>
    /// Tap To Place Object
    /// </summary>
    public void TapToPlaceObj()
    {
        panel_TapToPlaceObj.transform.GetChild(0).gameObject.SetActive(false);
        panel_TapToPlaceObj.transform.GetChild(1).gameObject.SetActive(false);

        panel_ChangeObj.SetActive(true);
        
        showObjCtrl.isOn = AppManager._instance.selectedObjType;
        showObjCtrl.Switching();

        btn_clear.SetActive(true);

        panel_TapToPlaceObj.SetActive(false);

        AppManager._instance.PlaceObj();
    }

    /// <summary>
    /// Clear Placed Object
    /// </summary>
    public void Clear()
    {
        AppManager._instance.ClearObjs();

        panel_ChangeObj.SetActive(false);
        panel_ChooseObj.SetActive(true);
        panel_Main.GetComponent<Image>().enabled = true;

        btn_ShowObject.SetActive(true);
        btn_Back.SetActive(true);
        btn_clear.SetActive(false);
    }
}
