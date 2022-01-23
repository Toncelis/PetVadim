using Data.Scripts.BattleGrid;

namespace Data.Scripts.SO_EventSystem
{
    public abstract class EventListener_GridBx : EventListener
    {
        public abstract override void InvokeResponse();

        public abstract void InvokeResponse(GridBox gridBox);
    }
}
