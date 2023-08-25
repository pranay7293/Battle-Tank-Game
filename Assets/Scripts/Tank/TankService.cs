using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankService : TankServiceGeneric<TankService>
{
    public TankView tankViewprefab;
    private void Start()
    {
        CreateNewTank();
        //StartGame();

    }
    /*private void StartGame()
    {
        for (int i = 0; i < 3; i++)
        {
            CreateNewTank();
        }
    }*/

    private void  CreateNewTank()
    {
        TankModel model = new TankModel(5, 100);
        TankController tank = new TankController(model, tankViewprefab);
         
    }
}
