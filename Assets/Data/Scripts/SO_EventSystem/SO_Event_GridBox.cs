using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_Event_GridBox : SO_Event
{
    private List<EventListener_GridBx> listeners = 
        new List<EventListener_GridBx>();
    
    public void Raise(GridBox gridBox)
    {
        for(int i = listeners.Count -1; i >= 0; i--)
            listeners[i].InvokeResponse(gridBox);
    }
    
    public void RegisterListener(EventListener_GridBx listener)
    { listeners.Add(listener); }

    public void UnregisterListener(EventListener_GridBx listener)
    { listeners.Remove(listener); }
}
