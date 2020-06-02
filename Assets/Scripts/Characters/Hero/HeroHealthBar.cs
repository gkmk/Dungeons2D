public class HeroHealthBar : HealthBar
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Current.OnHeroHealthChanged += UpdateHealthBar;
    }
}
