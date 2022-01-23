using System.Collections.Generic;
using Data.Scripts.BattleGrid;

namespace Data.Scripts.SO_EventSystem
{
    public class SO_Event_GridBox : SO_Event
    {
        private new List<EventListener_GridBx> listeners = 
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
}
