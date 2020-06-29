using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUtilies : MonoBehaviour {

    public delegate void PlayerLevelUp();
    public static event PlayerLevelUp PLUEvent;

    public static void PLUEventInvoke()
    {
        PLUEvent.Invoke();
    }
}
