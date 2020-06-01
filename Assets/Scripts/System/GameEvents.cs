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

    public event Action OnGFInitialized;
    public void GFInitialized()
    {
        OnGFInitialized?.Invoke();
    }
}
