using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CityUiStatController : MonoBehaviour
{
    [SerializeField] protected TMP_Text name;
    [SerializeField] protected TMP_Text population;
    [SerializeField] protected TMP_Text gold;
    [SerializeField] protected TMP_Text food;
    [SerializeField] protected TMP_Text weapons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCityUiStats(GameObject city)
    {
        BaseCityClass curCity = city.GetComponent<BaseCityClass>();
        name.text = city.name;
        population.text = curCity.population.ToString();
        gold.text = curCity.gold.ToString();
        food.text = curCity.food.ToString();
        weapons.text = curCity.weapons.ToString();
    }
}
