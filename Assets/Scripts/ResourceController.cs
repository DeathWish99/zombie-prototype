using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceController : MonoBehaviour
{
    private Color researchColor;

    private GameObject gameController;
    private GameObject currentActivePlayer;

    private string currentCityName;
    private int changedWeapons;
    private int changedFood;
    private int changedGold;

    private int initialGold;
    private int initialFood;
    private int initialWeapons;

    private int foodGoldInterval;
    private int weaponGoldInterval;
    private int researchGoldInterval;

    private float foodExchangeRate;
    private float weaponExchangeRate;

    private float prevFoodSliderValue;
    private float prevWeaponSliderValue;

    private bool isDonating;

    [SerializeField] private Slider foodSlider;
    [SerializeField] private Slider weaponSlider;

    [SerializeField] private Button donateToResearch;
    [SerializeField] private Button confirmButton;

    [SerializeField] private TMP_Text cityNameText;
    [SerializeField] private TMP_Text changedWeaponsText;
    [SerializeField] private TMP_Text changedFoodText;
    [SerializeField] private TMP_Text changedGoldText;
    private void Awake()
    {
        researchColor = new Color(63f / 255f, 193f / 255f, 201 / 255f);
        gameController = GameObject.FindGameObjectWithTag("GameController");
        currentActivePlayer = new GameObject();
        foodGoldInterval = 0;
        weaponGoldInterval = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        foodExchangeRate = 10f;
        weaponExchangeRate = 100f;
        foodSlider.onValueChanged.AddListener(delegate { OnFoodSliderChanged(); });
        weaponSlider.onValueChanged.AddListener(delegate { OnWeaponSliderChanged(); });
        donateToResearch.onClick.AddListener(OnDonateToResearchClick);
        confirmButton.onClick.AddListener(SaveValues);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        currentActivePlayer = gameController.GetComponent<PlayersController>().currentActivePlayer;

        foodSlider.value = 0f;
        weaponSlider.value = 0f;
        isDonating = false;
        donateToResearch.GetComponent<Image>().color = Color.white;

        foodGoldInterval = 0;
        weaponGoldInterval = 0;
        researchGoldInterval = 0;

        initialGold = currentActivePlayer.GetComponent<BaseCityClass>().gold;
        initialFood = currentActivePlayer.GetComponent<BaseCityClass>().food;
        initialWeapons = currentActivePlayer.GetComponent<BaseCityClass>().weapons;
        changedFood = initialFood;
        changedWeapons = initialWeapons;
        changedGold = initialGold;

        currentCityName = currentActivePlayer.name;

        cityNameText.text = currentCityName;
        changedFoodText.text = changedFood.ToString();
        changedWeaponsText.text = changedWeapons.ToString();
        changedGoldText.text = changedGold.ToString();
    }

    private void OnFoodSliderChanged()
    {
        foodGoldInterval = (int)foodExchangeRate * (int)foodSlider.value;
        changedFood = initialFood + (int)foodSlider.value;
        changedFoodText.text = changedFood.ToString();
        if(initialGold - foodGoldInterval - weaponGoldInterval - researchGoldInterval < 0)
        {
            foodSlider.value = prevFoodSliderValue;
            return;
        }
        prevFoodSliderValue = foodSlider.value;
        ChangeGoldValue();
    }

    private void OnWeaponSliderChanged()
    {
        weaponGoldInterval = (int)weaponExchangeRate * (int)weaponSlider.value;
        changedWeapons = initialWeapons + (int)weaponSlider.value;
        changedWeaponsText.text = changedWeapons.ToString();
        if (initialGold - foodGoldInterval - weaponGoldInterval - researchGoldInterval < 0)
        {
            weaponSlider.value = prevWeaponSliderValue;
            return;
        }
        prevWeaponSliderValue = weaponSlider.value;
        ChangeGoldValue();
    }
    private void OnDonateToResearchClick()
    {
        if (initialGold - foodGoldInterval - weaponGoldInterval - 5000 < 0 && !isDonating)
        {
            return;
        }
        if (!isDonating)
        {
            researchGoldInterval = 5000;
            isDonating = true;
            donateToResearch.GetComponent<Image>().color = researchColor;
        }
        else
        {
            researchGoldInterval = 0;
            isDonating = false;
            donateToResearch.GetComponent<Image>().color = Color.white;
        }
        ChangeGoldValue();
    }
    private void ChangeGoldValue()
    {
        changedGold = initialGold - foodGoldInterval - weaponGoldInterval - researchGoldInterval;
        changedGoldText.text = changedGold.ToString();
    }

    private void SaveValues()
    {
        currentActivePlayer.GetComponent<BaseCityClass>().ChangeResources(0, -(foodGoldInterval + weaponGoldInterval + researchGoldInterval), (int)foodSlider.value, (int)weaponSlider.value);
        if (isDonating)
        {
            gameController.GetComponent<PlayersController>().UpdateResearchValue(20);
        }

        gameController.GetComponent<CanvasControl>().CloseResourceUI();
    }
}
