using System;

public class GameEvents
{
    private static GameEvents _current = null;

    private static readonly object padlock = new object();

    GameEvents()
    {
    }

    public static GameEvents Current
    {
        get
        {
            lock (padlock)
            {
                if (_current == null)
                {
                    _current = new GameEvents();
                }
                return _current;
            }
        }
    }

    /// <summary>
    /// Trigger when GameFoundation has initialized
    /// </summary>
    public event Action OnGFInitialized;
    /// <summary>
    /// Game Foundation has initialized. 
    /// Inventory and such are now available.
    /// </summary>
    public void GFInitialized()
    {
        OnGFInitialized?.Invoke();
    }

    /// <summary>
    /// Listener for when Hero health has changed
    /// </summary>
    public event Action<int, int> OnHeroHealthChanged;
    /// <summary>
    /// Event trigger for when player health has changed.
    /// Possibly update GUI?
    /// </summary>
    /// <param name="current"></param>
    /// <param name="maxHealth"></param>
    public void SetPlayerHealth(int current, int maxHealth)
    {
        OnHeroHealthChanged?.Invoke(current, maxHealth);
    }
}
