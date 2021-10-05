using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, GameEvent> gameEventsDirectory = new Dictionary<string, GameEvent>();
    // List<GameEvent> gameEventsList = new List<GameEvent>();
    public static GameManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        var gameEvents = Resources.LoadAll<GameEvent>("GameEvents");
        foreach (GameEvent gameEvent in gameEvents)
        {
            gameEventsDirectory.Add(gameEvent.name, gameEvent);
            gameEvent.eventRaised = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
