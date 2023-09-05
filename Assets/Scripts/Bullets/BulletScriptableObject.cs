using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/NewBulletScriptableObject")]
public class BulletScriptableObject : ScriptableObject
{
    public BulletView bulletView;
    public float speedBullet;
    public ParticleSystem explosionType;
    public AudioClip explosionClip;
    public AudioSource sourceAudio;
}
