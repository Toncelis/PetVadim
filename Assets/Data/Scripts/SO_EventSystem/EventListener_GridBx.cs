using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventListener_GridBx : EventListener
{
    public abstract override void InvokeResponse();

    public abstract void InvokeResponse(GridBox gridBox);
}
