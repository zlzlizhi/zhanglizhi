using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager _instance;
    public static CursorManager install;
    public Texture2D Cursor_Normal;
    public Texture2D Cursor_Npctalk;
    public Texture2D Cursor_LocktaRGET;
    public Texture2D Cursor_Pick;
    public Texture2D Cursor_Attack;


    private Vector2 hotspot = Vector2.zero;
    private CursorMode mode = CursorMode.Auto;

    private void Awake()
    {
        _instance = this;
    }
    public void SetNormal()
    {
        Cursor.SetCursor(Cursor_Normal, hotspot, mode);
    }
    public void SetNpcTalk()
    {
        Cursor.SetCursor(Cursor_Npctalk, hotspot, mode);
    }
    public void SetAttack()
    {
        Cursor.SetCursor(Cursor_Attack, hotspot, mode);
    }
    public void SetLockTarget()
    {
        Cursor.SetCursor(Cursor_LocktaRGET, hotspot, mode);
    }
}
