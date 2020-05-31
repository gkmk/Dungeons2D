using System;
using System.Collections;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DataAccessLayers;
using UnityEngine.GameFoundation.DataPersistence;
using UnityEngine.Promise;

public class GFInit : MonoBehaviour
{
    PersistenceDataLayer dataLayer;
    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        //Deferred saveOperation = dataLayer.Save();

        // Check if the operation is already done.
        //if (saveOperation.isDone)
        //{
        //    LogSaveOperationCompletion(saveOperation);
        //}
        //else
        //{
        //    StartCoroutine(WaitForSaveCompletion(saveOperation));
        //}
    }

    void Awake()
    {
        // Creates a new data layer for Game Foundation,
        // with the default parameters.
        dataLayer = new PersistenceDataLayer(
                new LocalPersistence("game_foundation", new JsonDataSerializer()));// MemoryDataLayer();

        // Initializes Game Foundation with the data layer.
        GameFoundation.Initialize
            (dataLayer, OnInitSucceeded, OnInitFailed);
    }

    // Called when Game Foundation is successfully
    // initialized.
    void OnInitSucceeded()
    {
        Debug.Log("Game Foundation is successfully initialized");
        GameEvents.Current.GFInitialized();
    }

    // Called if Game Foundation initialization fails 
    void OnInitFailed(Exception error)
    {
        Debug.LogException(error);
    }

    private static IEnumerator WaitForSaveCompletion(Deferred saveOperation)
    {
        // Wait for the operation to complete.
        yield return saveOperation.Wait();

        LogSaveOperationCompletion(saveOperation);
    }

    private static void LogSaveOperationCompletion(Deferred saveOperation)
    {
        // Check if the operation was successful.
        if (saveOperation.isFulfilled)
        {
            Debug.Log("Saved!");
        }
        else
        {
            Debug.LogError($"Save failed! Error: {saveOperation.error}");
        }
    }
}