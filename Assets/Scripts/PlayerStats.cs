using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.Current.OnGFInitialized += LoadStats;
    }

    void LoadStats()
    {
        Debug.Log("loading stats... oooo mamaaa");
        //StatManager.catalog.GetStatDefinitions()
        //var items = InventoryManager.GetItems();
        //var item = InventoryManager.FindItem("playerHero");
        //if (items == null)
        //    myItem = InventoryManager.CreateItem("playerHero");
        //var statValue = GameFoundation.catalogs.statCatalog.FindStatDefinition("attack");

        //var attack = StatManager.GetValue(myItem, statValue);
        //Debug.Log("MY ATack is: " + attack);
    }

}
