using UnityEngine;
using System.Collections.Generic;
using System;
using Vuforia;
using HoloToolkit.Unity;
using HoloToolkit.Sharing.Spawning;

public class GameObjectManager : Singleton<GameObjectManager>
{

    #region PRIVATE_MEMBER_VARIABLES

    private Dictionary<ulong, GameObject> _gameObjects;
    #endregion

    #region PUBLIC_MEMBER_VARIABLES
    public Material[] Colors;
    public GameObject PrefabToSpawn;
    #endregion

    #region PUBLIC_METHODS
    void Start()
    {
        if (PrefabToSpawn == null)
        {
#warning missing errorhandling (NullReference)
        }

    }

    /// <summary>
    /// Deletes a gameobject by a given id
    /// </summary>
    /// <param name="id">ID of gameobject to delete</param>
    public void RemoveGameObjectById(ulong id)
    {
        _gameObjects.Remove(id);
    }

    public void CreateNewGameObjectFromVumark(VuMarkAbstractBehaviour bhvr)
    {
        //check if gameobject for given id is already there
        if (_gameObjects.ContainsKey(bhvr.VuMarkTarget.InstanceId.NumericValue))
        {
            return;
        }

        //Inistaniate new gameobject
        GameObject go;
        go = Instantiate(PrefabToSpawn, Vector3.zero, Quaternion.identity) as GameObject;

        //Place gameobject on marker position
        go.transform.position = bhvr.transform.position;
        go.transform.rotation = bhvr.transform.rotation;
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        //Get controller from gameobejct
        var cubeController = go.GetComponent<CubeController>();

        //Get id from marker
        var id = bhvr.VuMarkTarget.InstanceId.NumericValue;


        cubeController.ID = id;
        var rend = go.GetComponent<Renderer>();
        if (rend != null)
        {
            //Color the gameobject
            rend.material = Colors[(int)id % Colors.Length];
        }

        //Add gameobject to gameobjectmanager
        _gameObjects.Add(bhvr.VuMarkTarget.InstanceId.NumericValue, go);
    }
    #endregion
}


