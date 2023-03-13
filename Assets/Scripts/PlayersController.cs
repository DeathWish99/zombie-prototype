using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    public List<GameObject> players;
    // Start is called before the first frame update
    public GameObject currentActivePlayer;
    [SerializeField] private GameObject researchObj;
    private int currentActivePlayerIdx;
    private float researchValue;
    void Awake()
    {
        InitializePlayerResources();
        InitializePlayerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPlayer()
    {
        if(currentActivePlayerIdx < players.Count - 1)
        {
            currentActivePlayerIdx += 1;
            currentActivePlayer.GetComponent<BaseCityClass>().SetHighlight(false);
            currentActivePlayer = players[currentActivePlayerIdx];
            currentActivePlayer.GetComponent<BaseCityClass>().SetHighlight(true);

            gameObject.GetComponent<TurnController>().SetTurn(currentActivePlayer, false);
        }
        else
        {
            currentActivePlayerIdx = 0;
            currentActivePlayer.GetComponent<BaseCityClass>().SetHighlight(false);
            currentActivePlayer = players[currentActivePlayerIdx];
            currentActivePlayer.GetComponent<BaseCityClass>().SetHighlight(true);

            gameObject.GetComponent<TurnController>().SetTurn(currentActivePlayer, true);
        }
    }
    public void UpdateResearchValue(float addResearch)
    {
        researchValue += addResearch;
        researchObj.GetComponent<ResearchBarUiControl>().UpdateResearchBar(researchValue);
    }
    private void InitializePlayerResources()
    {
        foreach(GameObject player in players)
        {
            player.GetComponent<BaseCityClass>().InitializeResources(200000, 16000, 5000, 700);
        }
        researchValue = 0;
    }
    private void InitializePlayerTurn()
    {
        currentActivePlayer = players[0];
        currentActivePlayerIdx = 0;
        gameObject.GetComponent<TurnController>().SetTurn(currentActivePlayer, false);
        currentActivePlayer.GetComponent<BaseCityClass>().SetHighlight(true);
    }

}
