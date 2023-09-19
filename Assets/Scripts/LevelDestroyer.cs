using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class LevelDestroyer : GenericSingleton<LevelDestroyer>
{
    [SerializeField] private GameObject Level;
    [SerializeField] private GameObject LevelFailed;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button exitButton;
    private bool isRunning = false;

    void Start()
    {
        replayButton.onClick.AddListener(() => { TankService.Instance.TankController.TankView.LoadLevel(); });
        exitButton.onClick.AddListener(() => { TankService.Instance.TankController.TankView.LoadLobby(); });
        EventManager.OnTankDestroyed += OnTankDestroyed;
    }
    private void OnTankDestroyed()
    {
        if (!isRunning)
        {
            StartCoroutine(DestroyAll());
        }
    }

    public IEnumerator DestroyAll()
    {
        isRunning = true;
        StartCoroutine(DestroyEnemies(2));
        StartCoroutine(DestroyGround(3));
        yield return null;      
    }

    IEnumerator DestroyGround(int sec)
    {
        yield return new WaitForSeconds(sec);
        Level.SetActive(false);  
        Invoke(nameof(LevelFailedMenu), 1);
    }
    IEnumerator DestroyEnemies(int sec)
    {
        yield return new WaitForSeconds(sec);
        List<EnemyController> enemiesCopy = new List<EnemyController>(EnemyService.Instance.ListofEnemies);

        foreach (EnemyController enemy in enemiesCopy)
        {
            if (enemy != null)
            {
                enemy.DestroyEnemy();
            }
        }
    }

    private void LevelFailedMenu()
    {
        SoundManager.Instance.PlaySound(Sounds.GameOver);
        LevelFailed.SetActive(true);
    }
}
