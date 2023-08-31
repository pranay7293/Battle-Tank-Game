using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankService : MonoBehaviour 
{ 
    [SerializeField]
    private TankScriptableObjectList TankScriptableObjectList;
    private void Start()
    {
        CreateNewTank();
    }

    private TankController CreateNewTank()
    {
        int randomNumber = (int)Random.Range(0, TankScriptableObjectList.tanks.Length - 1);
        TankScriptableObject tankObject = TankScriptableObjectList.tanks[randomNumber];
        Debug.Log("Created tank of type: " + tankObject.name);
        TankModel tankModel = new TankModel(tankObject);
        TankController tankController = new TankController(tankModel, tankObject.tankView);
        return tankController;

    }
}
