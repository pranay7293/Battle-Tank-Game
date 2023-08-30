using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/NewTankScriptableObjects")]
public class TankScriptableObject : ScriptableObject
{
    public TankTypes type;
    public string tankName;
    public int health;
    public float speed;
    public int damage;
    public TankView tankView;
    public AudioClip shootClip;
}