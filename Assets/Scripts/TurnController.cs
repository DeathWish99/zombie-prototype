using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnController : MonoBehaviour
{
    public string _currentTurn;
    public int _currentWave;

    [SerializeField] private Button nextTurn;
    [SerializeField] private TMP_Text currentTurn;
    [SerializeField] private TMP_Text currentWave;

    private EventsController eventController;

    // Start is called before the first frame update
    void Start()
    {
        nextTurn.onClick.AddListener(ProcessNextTurn);
        eventController = GetComponent<EventsController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTurn(GameObject city, bool increaseWave)
    {
        _currentTurn = city.name;
        if (increaseWave)
        {
            _currentWave += 1;
            if(eventController.wavesToZombieAttack > 0 && eventController.isZombiesInvading)
            {
                eventController.ZombieAttack();
            }
            else
            {
                eventController.StartZombieOrEvent();
            }
        }
        SetTurnText();
        SetWaveText();
    }

    void ProcessNextTurn()
    {
        gameObject.GetComponent<PlayersController>().NextPlayer();
    }

    void SetTurnText()
    {
        currentTurn.text = _currentTurn + "'s turn";
    }
    void SetWaveText()
    {
        currentWave.text = "Wave " + _currentWave.ToString();
    }
}
