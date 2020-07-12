using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNames : MonoBehaviour
{
    public static readonly string FREEZE_TICK = "Freeze Tick";
    public static readonly string FREEZE_START = "Freeze Start";
    public static readonly string FREEZE_STOP = "Freeze Stop";
    
    public static readonly string WAVE_START = "Wave Start";
    public static readonly string WAVE_END = "Wave End";
    public static readonly string GAME_WON = "Game Won";
    
    public static readonly string ENEMY_DIED = "Enemy Died";
    public static readonly string PLAYER_DIED = "Player died";
}
