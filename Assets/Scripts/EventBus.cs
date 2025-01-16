using UnityEngine;
using UnityEngine.Events;

public static class EventBus
{
    public static UnityEvent DragObjectStart = new();
    public static UnityEvent DragObjectStop = new();
    

}
