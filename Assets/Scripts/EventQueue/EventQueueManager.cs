using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    static public EventQueueManager instance;

    public List<ICommand> Events => _events;
    private List<ICommand> _events = new List<ICommand>();

    public Queue<ICommand> EventQueue => _eventQueue;
    private Queue<ICommand> _eventQueue = new Queue<ICommand>();

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    private void Update()
    {
        foreach (var command in _events)
        {
            command.Execute();
        }

        foreach (var command in _eventQueue)
        {
            command.Execute();
        }

        _events.Clear();
        _eventQueue.Clear();
    }

    public void AddEvent(ICommand command) => _events.Add(command);
    public void AddEventToQueue(ICommand command) => _eventQueue.Enqueue(command);
}
