using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchBarUiControl : MonoBehaviour
{
    [SerializeField] private Slider researchBar;
    [SerializeField] private Image researchFill;
    [SerializeField] private Gradient researchGradient;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitialResearchBar()
    {
        researchBar.maxValue = 100f;
        researchBar.minValue = 0f;
        researchBar.value = 0f;

        researchFill.color = researchGradient.Evaluate(0f);
    }

    public void UpdateResearchBar(float newValue)
    {
        researchBar.value = newValue;
        researchFill.color = researchGradient.Evaluate(researchBar.normalizedValue);
    }
}
