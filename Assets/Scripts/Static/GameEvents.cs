using System;

public static class GameEvents
{
    public static Action _On_Player_Defeated;
    public static Action _On_Player_Won;

    public static void OnPlayerDefeated() => _On_Player_Defeated?.Invoke();
    public static void OnPlayerWon() => _On_Player_Won?.Invoke();
}