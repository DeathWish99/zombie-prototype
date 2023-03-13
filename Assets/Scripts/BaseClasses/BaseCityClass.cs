using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseCityClass : MonoBehaviour
{
    public int population;
    public int gold;
    public int food;
    public int weapons;

    [SerializeField] private GameObject cityPanel;
    [SerializeField] private GameObject highlight;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeResources(int _population, int _gold, int _food, int _weapons)
    {
        population = _population;
        gold = _gold;
        food = _food;
        weapons = _weapons;
        cityPanel.GetComponent<CityUiStatController>().SetCityUiStats(this.gameObject);
    }

    public void ChangeResources(int _population, int _gold, int _food, int _weapons)
    {
        population += _population;
        gold += _gold;
        food += _food;
        weapons += _weapons;
        cityPanel.GetComponent<CityUiStatController>().SetCityUiStats(this.gameObject);
    }

    public void ChangePopulation(int _population)
    {
        population += _population;
        cityPanel.GetComponent<CityUiStatController>().SetCityUiStats(this.gameObject);
    }
    public void ChangeFood(int _food)
    {
        food += _food;
        cityPanel.GetComponent<CityUiStatController>().SetCityUiStats(this.gameObject);
    }

    public void ChangeGold(int _gold)
    {
        gold += _gold;
        cityPanel.GetComponent<CityUiStatController>().SetCityUiStats(this.gameObject);
    }

    public void ChangeWeapons(int _weapons)
    {
        weapons += _weapons;
        cityPanel.GetComponent<CityUiStatController>().SetCityUiStats(this.gameObject);
    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }
}
