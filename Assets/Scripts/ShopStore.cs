using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.CatalogManagement;

public class ShopStore : MonoBehaviour
{
    public string storeId;

    Store store;
    BaseTransaction[] storeItems;
    private void Awake()
    {
        GameEvents.Current.OnGFInitialized += LoadItems;
    }

    void LoadItems()
    {
        Debug.Log("listing shop items");
        //var silver = WalletManager.GetBalance("silverCoin");
        //Debug.Log("BEFORE buy i have silver:" + silver);
        store = GameFoundation.catalogs.storeCatalog.FindItem(storeId);
        storeItems = store.GetStoreItems();
        //foreach (var item in storeItems)
        //{
        //    Debug.Log(item.displayName);
        //    BuyItem(item.id);
        //}
    }

    void BuyItem(string transactionId)
    {
        // Gets the transaction catalog
        var catalog = GameFoundation.catalogs.transactionCatalog;

        // Finds the transaction by its id.
        var transaction = catalog.FindTransaction<VirtualTransaction>(transactionId);
        if (transaction is null)
        {
            Debug.LogError($"Transaction {transactionId} not found");
            return;
        }

        StartCoroutine(InitiateTransaction(transaction));
    }

    IEnumerator InitiateTransaction(BaseTransaction transaction)
    {
        // Gets the handle of the transaction.
        var deferredResult = TransactionManager.BeginTransaction(transaction);

        try
        {
            // Waits for the process to finish
            while (!deferredResult.isDone)
            {
                yield return null;
            }

            // The process failed
            if (!deferredResult.isFulfilled)
            {
                Debug.LogException(deferredResult.error);
            }

            // The process succeeded
            else
            {
                var result = deferredResult.result;

                DisplayResult(result);
            }
        }
        finally
        {
            // A process handle can be released.
            deferredResult.Release();
        }
    }

    void DisplayResult(TransactionResult result)
    {
        var builder = new StringBuilder();

        var paidCurrencies = result.costs.currencies;
        foreach (var exchange in paidCurrencies)
        {
            builder.AppendLine($"Paid '{exchange.currency.displayName}' X {exchange.amount}");
        }

        var consumedItems = result.costs.itemIds;
        foreach (var consumedItemId in consumedItems)
        {
            builder.AppendLine($"Consumed item {consumedItemId}");
        }

        var rewardedCurrencies = result.rewards.currencies;
        foreach (var exchange in rewardedCurrencies)
        {
            builder.AppendLine($"Earned '{exchange.currency.displayName}' X {exchange.amount}");
        }

        var rewardedItems = result.rewards.items;
        foreach (var rewardedItem in rewardedItems)
        {
            builder.AppendLine($"Obtained '{rewardedItem.definition.displayName}' ({rewardedItem.id})");
        }

        Debug.Log(builder.ToString());
    }

}