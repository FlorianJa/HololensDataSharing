using UnityEngine;
using System.Collections;
using HoloToolkit.Unity.InputModule;
using System;
using HoloToolkit.Unity;

public class CubeController : MonoBehaviour, IInputClickHandler
{
    #region PRIVATE_MEMBER_VARIABLES

    private GameObjectManager _gameObjectManager;

    #endregion

    #region PUBLIC_MEMBER_VARIABLES
    public ulong ID;
    #endregion

    #region PUBLIC_METHODS

    // Use this for initialization
    public void Start()
    {
        _gameObjectManager = GameObjectManager.Instance;
    }

    /// <summary>
    /// Removes the gameobject from the gameobjectmanager and destorys it.
    /// </summary>
    public void Remove()
    {
        _gameObjectManager.RemoveGameObjectById(ID);
        Destroy(gameObject);
    }

    /// <summary>
    /// Callback for clickevent
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("CubeCommand OnClick() " + ID);
        Remove();
    }
    #endregion

}
