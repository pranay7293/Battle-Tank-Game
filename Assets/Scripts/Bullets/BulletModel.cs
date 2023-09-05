using UnityEngine;

public class BulletModel
{
    public BulletController BulletController { get; set; }
    public float BulletSpeed { get; }
    public ParticleSystem bulletExplosion;
    public AudioClip bulletClip;
    public AudioSource bulletAudioSource;

    public BulletModel(BulletScriptableObject bulletScriptableObject)
    {
        BulletSpeed = bulletScriptableObject.speedBullet;
        bulletExplosion = bulletScriptableObject.explosionType;
        bulletClip = bulletScriptableObject.explosionClip;
        bulletAudioSource = bulletScriptableObject.sourceAudio;
    }
    public void SetBulletController(BulletController bulletCtrl)
    {
        BulletController = bulletCtrl;
    }
}
