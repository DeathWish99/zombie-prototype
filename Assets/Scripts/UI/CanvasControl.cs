using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] private Canvas mainUI;
    [SerializeField] private Canvas resourceUI;
    [SerializeField] private Canvas tradeUI;


    [SerializeField] private Button investButton;
    [SerializeField] private Button tradeButton;
    [SerializeField] private Button investClose;
    [SerializeField] private Button tradeClose;

    private void Awake()
    {
        resourceUI.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        tradeUI.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        resourceUI.gameObject.SetActive(false);
        tradeUI.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        investButton.onClick.AddListener(OpenResourceUI);
        tradeButton.onClick.AddListener(OpenTradeUI);
        investClose.onClick.AddListener(CloseResourceUI);
        tradeClose.onClick.AddListener(CloseTradeUI);
    }

    // Update is called once per frame
    void Update()
    {
        if(!resourceUI.gameObject.activeSelf && !tradeUI.gameObject.activeSelf)
        {
            mainUI.gameObject.SetActive(true);
        }
        else
        {
            mainUI.gameObject.SetActive(false);
        }
    }

    public void OpenResourceUI()
    {
        resourceUI.gameObject.SetActive(true);
        resourceUI.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OpenTradeUI()
    {
        tradeUI.gameObject.SetActive(true);
        tradeUI.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void CloseResourceUI()
    {
        resourceUI.gameObject.SetActive(false);
        resourceUI.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void CloseTradeUI()
    {
        tradeUI.gameObject.SetActive(false);
        tradeUI.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
