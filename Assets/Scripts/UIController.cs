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

    [Header("Buttons")]

    [SerializeField] GameObject btn_home = null;

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

        panel_Main.GetComponent<Image>().enabled = false;
        ReleaseBtnShowObj();
        panel_ChooseObj.SetActive(false);
    }

    public void GetReadyBtnShowObj()
    {
        btn_ShowObject.GetComponent<Image>().sprite = btn_placeObjsOff;
        btn_ShowObject.GetComponent<Image>().color = colGreen;
        btn_ShowObject.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    void ReleaseBtnShowObj()
    {
        panel_ChooseObj.GetComponent<ChooseObj>().ReleaseSeletedObjects();

        btn_ShowObject.GetComponent<Image>().sprite = btn_placeObjsOff;
        btn_ShowObject.GetComponent<Image>().color = colWhite;
        btn_ShowObject.GetComponent<Button>().onClick.AddListener(ShowSelectObjPanel);
    }
}
