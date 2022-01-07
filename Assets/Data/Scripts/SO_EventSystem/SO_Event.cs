using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Event : MonoBehaviour
{
    protected List<EventListener> listeners = 
        new List<EventListener>();

    public void Raise()
    {
        for(int i = listeners.Count -1; i >= 0; i--)
            listeners[i].InvokeResponse();
    }

    public void RegisterListener(EventListener listener)
    { listeners.Add(listener); }

    public void UnregisterListener(EventListener listener)
    { listeners.Remove(listener); }
}
