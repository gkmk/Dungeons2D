using UnityEngine;
using UnityEngine.GameFoundation;

public class PlayerStats : MonoBehaviour
{
    public int attack;
    public int defense;
    public int agility;

    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.Current.OnGFInitialized += LoadStats;
    }

    void LoadStats()
    {
        //Debug.Log("loading stats... oooo mamaaa");
        //StatManager.catalog.GetStatDefinitions()
        //var items = InventoryManager.GetItems();
        var myItem = InventoryManager.FindItem("playerHero");
        if (myItem == null)
            myItem = InventoryManager.CreateItem("playerHero");
        var attackValue = GameFoundation.catalogs.statCatalog.FindStatDefinition("attack");
        var agilityValue = GameFoundation.catalogs.statCatalog.FindStatDefinition("agility");
        var defenseValue = GameFoundation.catalogs.statCatalog.FindStatDefinition("defense");

        attack = StatManager.GetValue(myItem, attackValue);
        agility = StatManager.GetValue(myItem, agilityValue);
        defense = StatManager.GetValue(myItem, defenseValue);
    }

}
