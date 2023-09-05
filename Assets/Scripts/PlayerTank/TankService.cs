using UnityEngine;
using Random = UnityEngine.Random;

public class TankService : GenericSingleton<TankService>
{
    [SerializeField]
    private TankScriptableObjectList tankScriptableObjectList;
    public TankController TankController { get; private set; }


    private void Start()
    {
        CreateNewTank();
    }

    private TankController CreateNewTank()
    {
        int randomNumber = (int)Random.Range(0, tankScriptableObjectList.playerTanks.Length - 1);
        TankScriptableObject tankObject = tankScriptableObjectList.playerTanks[randomNumber];
        Debug.Log("Created tank of type: " + tankObject.name);
        TankModel tankModel = new TankModel(tankObject);
        TankController tankController = new TankController(tankModel, tankObject.tankView);
        return tankController;

    }
    public TankController GetTankController()
    {
        return TankController;
    }
}
