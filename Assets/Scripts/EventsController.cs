using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DummyEvents
{
    public int key;
    public string eventName;
}
public class EventsController : MonoBehaviour
{

    public List<DummyEvents> dummyEvents;

    public int wavesToZombieAttack;
    public int baseZombieDamage;

    public bool isZombiesInvading;

    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private TMP_Text eventsText;
    [SerializeField] private TMP_Text zombieText;

    private int targetCityIdx;
    // Start is called before the first frame update
    void Start()
    {
        eventsText.gameObject.SetActive(false);
        zombieText.gameObject.SetActive(false);
        notificationPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartZombieOrEvent()
    {
        Debug.Log("Start Event triggering");
        if (!isZombiesInvading)
        {
            if (Random.value < .5)
            {
                TriggerEvent();
            }
            else
            {
                TriggerZombie();
            }
        }
        else
        {
            if (Random.value < .8)
            {
                TriggerEvent();
            }
        }
    }

    public void ZombieAttack()
    {
        if(wavesToZombieAttack > 1)
        {
            wavesToZombieAttack -= 1;
            StartZombieOrEvent();
            return;
        }
        else
        {
            BaseCityClass currentPlayerResources = gameObject.GetComponent<PlayersController>().players[targetCityIdx].GetComponent<BaseCityClass>();
            int trueZombieDamage = baseZombieDamage / currentPlayerResources.weapons;
            int weaponsLost = currentPlayerResources.weapons / 2;
            currentPlayerResources.ChangePopulation(-trueZombieDamage);
            currentPlayerResources.ChangeWeapons(-weaponsLost);
            isZombiesInvading = false;
            wavesToZombieAttack = 0;
            StartCoroutine(NotificationOn(true, "Zombies have attacked " + gameObject.GetComponent<PlayersController>().players[targetCityIdx].name + " and killed " + trueZombieDamage.ToString() + " people. " + weaponsLost.ToString() + " weapons have been lost."));
        }
    }
    private void TriggerEvent()
    {
        StopCoroutine("NotificationOn");
        PlayersController playersController = gameObject.GetComponent<PlayersController>();
        int randomCity = Random.Range(0, playersController.players.Count);
        BaseCityClass currentPlayerResources = gameObject.GetComponent<PlayersController>().players[randomCity].GetComponent<BaseCityClass>();
        int eventIdx = Random.Range(0, dummyEvents.Count);
        switch (eventIdx)
        {
            case 0:
                currentPlayerResources.ChangeFood(IncreaseFood());
                StartCoroutine(NotificationOn(false, "Event: Food increasing for " + gameObject.GetComponent<PlayersController>().players[randomCity].name + "!"));
                break;
            case 1:
                currentPlayerResources.ChangeWeapons(IncreaseWeapons());
                StartCoroutine(NotificationOn(false, "Event: Weapons increasing for " + gameObject.GetComponent<PlayersController>().players[randomCity].name + "!"));
                break;
            case 2:
                currentPlayerResources.ChangeGold(-Corruption());
                StartCoroutine(NotificationOn(false, "Event: Corruption rampant! Lost gold for " + gameObject.GetComponent<PlayersController>().players[randomCity].name + "!"));
                break;
            case 3:
                gameObject.GetComponent<PlayersController>().UpdateResearchValue(ResearchBreakthrough());
                StartCoroutine(NotificationOn(false, "Event: Breakthrough in research! Research value increasing!"));
                break;
            default:
                Debug.Log("Out of Range");
                break;
        }
    }

    private void TriggerZombie()
    {
        StopCoroutine("NotificationOn");
        isZombiesInvading = true;
        PlayersController playersController = gameObject.GetComponent<PlayersController>();
        targetCityIdx = Random.Range(0, playersController.players.Count);

        wavesToZombieAttack = Random.Range(1, 3);

        StartCoroutine(NotificationOn(true, "ZOMBIE ATTACK!! WILL REACH " + gameObject.GetComponent<PlayersController>().players[targetCityIdx].name + " IN " + wavesToZombieAttack.ToString() + " WAVES!"));
    }

    private IEnumerator NotificationOn(bool isZombie, string text)
    {
        notificationPanel.gameObject.SetActive(true);
        if (!isZombie) 
        {
            eventsText.gameObject.SetActive(true);
            eventsText.text = text;
        }
        else
        {
            zombieText.gameObject.SetActive(true);
            zombieText.text = text;
        }
        yield return new WaitForSeconds(3f);
        eventsText.gameObject.SetActive(false);
        zombieText.gameObject.SetActive(false);
        notificationPanel.gameObject.SetActive(false);
    }

    private int IncreaseFood()
    {
        return 1000;
    }

    private int IncreaseWeapons()
    {
        return 100;
    }

    private int Corruption()
    {
        return 1000;
    }
    private float ResearchBreakthrough()
    {
        return 10f;
    }
}
