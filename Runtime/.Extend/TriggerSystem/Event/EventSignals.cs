using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventIndex
{
    EVENT_GAME_START = 0,
    EVENT_TIMER_EXPIRED = 1,
    EVENT_NEXT_WEEK = 2,
    EVENT_COIN_GET = 3,
    EVENT_COIN_LOST = 4,
    EVENT_DEBUFF_GET = 5,
    EVENT_DEBUFF_LOST = 6,
    EVENT_PRESSURE_GET = 7,
    EVENT_PRESSURE_REDUCE = 8,
    EVENT_ITEM_GET = 9,
    EVENT_WEEK_FIN_SPECIAL = 10,
    EVENT_GAME_CLEAR = 30,
    EVENT_TEST_MODULE = 1001,
}

public static class EventSignals
{
    public static EventGameHandler [] OnGameEvent;
    public delegate void EventGameHandler();
    public static void DoGameEvent(EventIndex index) { OnGameEvent[(int)index]?.Invoke(); }

    static EventSignals(){
        //OnGameEvent = new EventGameHandler[System.Enum.GetNames(typeof(EventIndex)).Length];
        OnGameEvent = new EventGameHandler[1024];
    }
}