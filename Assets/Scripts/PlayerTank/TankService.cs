using UnityEngine;
using Random = UnityEngine.Random;

public class TankService : GenericSingleton<TankService>
{
    [SerializeField]
    private TankScriptableObjectList tankScriptableObjectList;

    private TankController tankController;

    public TankController TankController => tankController;


    private void Start()
    {
        CreateNewTank();
    }

    private void CreateNewTank()
    {
        int randomNumber = (int)Random.Range(0, tankScriptableObjectList.playerTanks.Length);
        TankScriptableObject tankObject = tankScriptableObjectList.playerTanks[randomNumber];
        Debug.Log("Created tank of type: " + tankObject.name);
        TankModel tankModel = new TankModel(tankObject);
        tankController = new TankController(tankModel, tankObject.tankView);
    }
   
}
