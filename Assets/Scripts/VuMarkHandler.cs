using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Vuforia;
using HoloToolkit.Unity;

/// <summary>
/// A custom handler which uses the vuMarkManager.
/// </summary>
public class VuMarkHandler : Singleton<VuMarkHandler>
{
    #region PRIVATE_MEMBER_VARIABLES

    private VuMarkManager mVuMarkManager;
    //private GameObjectManager _gameObjectManager;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region UNTIY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        // register callbacks to VuMark Manager
        mVuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
        mVuMarkManager.RegisterVuMarkDetectedCallback(OnVuMarkDetected);
        mVuMarkManager.RegisterVuMarkLostCallback(OnVuMarkLost);


        //_gameObjectManager = GameObjectManager.Instance;

    }

    void Update()
    {
        foreach (var bhvr in mVuMarkManager.GetActiveBehaviours())
        {
            //_gameObjectManager.CreateNewGameObjectFromVumark(bhvr);
        }
    }

    new void OnDestroy()
    {
        base.OnDestroy();
        // unregister callbacks from VuMark Manager
        mVuMarkManager.UnregisterVuMarkDetectedCallback(OnVuMarkDetected);
        mVuMarkManager.UnregisterVuMarkLostCallback(OnVuMarkLost);
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    /// <summary>
    /// This method will be called whenever a new VuMark is detected
    /// </summary>
    public void OnVuMarkDetected(VuMarkTarget target)
    {
        Debug.Log("New VuMark: " + GetVuMarkDataAsString(target));
    }

    /// <summary>
    /// This method will be called whenever a tracked VuMark is lost
    /// </summary>
    public void OnVuMarkLost(VuMarkTarget target)
    {
        Debug.Log("Lost VuMark: " + GetVuMarkDataAsString(target));
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    /// <summary>
    /// Returns data typ of vumark
    /// </summary>
    /// <param name="vumark"></param>
    /// <returns></returns>
    private string GetVuMarkDataType(VuMarkTarget vumark)
    {
        switch (vumark.InstanceId.DataType)
        {
            case InstanceIdType.BYTES:
                return "Bytes";
            case InstanceIdType.STRING:
                return "String";
            case InstanceIdType.NUMERIC:
                return "Numeric";
        }
        return "";
    }

    /// <summary>
    /// Returns the data of a vumark as string.
    /// </summary>
    /// <param name="vumark"></param>
    /// <returns></returns>
    private string GetVuMarkDataAsString(VuMarkTarget vumark)
    {
        switch (vumark.InstanceId.DataType)
        {
            case InstanceIdType.BYTES:
                return vumark.InstanceId.HexStringValue;
            case InstanceIdType.STRING:
                return vumark.InstanceId.StringValue;
            case InstanceIdType.NUMERIC:
                return vumark.InstanceId.NumericValue.ToString();
        }
        return "";
    }

    #endregion // PRIVATE_METHODS
}

