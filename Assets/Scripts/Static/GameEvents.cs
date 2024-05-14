using System;

public static class GameEvents
{
    public static event Action OnPlayerDefeated = null;
    public static event Action OnPlayerWon = null;

    public static void PlayerDefeated() => OnPlayerDefeated?.Invoke();
    
    public static void PlayerWon() => OnPlayerWon?.Invoke();
}