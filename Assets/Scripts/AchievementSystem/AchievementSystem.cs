using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AchievementSystem : MonoBehaviour, IObserver
{
    private TankView tankPlayer;
    private EnemyView enemyTank;

    private int numberOfBullets; 
    private int enemyKills;
    private int totalDamage;

    [SerializeField] private TextMeshPro textDisplay;
    [SerializeField] private Animator textAnim;
    private Queue<string> achievements = new Queue<string>();

    private Dictionary<int, string> bulletsFiredAchievements = new Dictionary<int, string>
    {
        { 10, "10 Bullets Fired!" },
        { 25, "25 Bullets Fired!" },
        { 50, "50 Bullets Fired!" }
    };
    private HashSet<int> bulletsFiredAchievementsUnlocked = new HashSet<int>();

    private Dictionary<int, string> damageAchievements = new Dictionary<int, string>
    {
        { 500, "500 Damage Inflicted!" },
        { 1000, "1000 Damage Inflicted!" },
        { 2000, "2000 Damage Inflicted!" }
    };
    private HashSet<int> damageAchievementsUnlocked = new HashSet<int>();

    private Dictionary<int, string> killsAchievements = new Dictionary<int, string>
    {
        { 5, "5 EnemyTanks Killed!" },
        { 10, "10 EnemyTanks Killed!" },
        { 20, "20 EnemyTanks Killed!" }
    };
    private HashSet<int> killsAchievementsUnlocked = new HashSet<int>();

    private void Start()
    {
        enemyKills = 0;
        numberOfBullets = 0;
        totalDamage = 0;
        Invoke(nameof(InitializePlayer), 0.2f);
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void InitializePlayer()
    {
        enemyTank = EnemyService.Instance.EnemyController.EnemyView;
        tankPlayer = TankService.Instance.TankController.TankView;
        transform.SetParent(tankPlayer.transform);
        tankPlayer.AddBulletsFiredObservers(this);
    }

    private void DisplayQueuedAchievement()
    {
        if (achievements.Count > 0 && tankPlayer != null)
        {
            string achievement = achievements.Dequeue();
            textDisplay.text = achievement;
            textAnim.SetTrigger("ShowText");
            StartCoroutine(ResetText());
        }
    }

    private IEnumerator ResetText()
    {
        yield return new WaitForSeconds(3f);
        DisplayQueuedAchievement();
    }

    public void OnBulletsFired()
    {
        numberOfBullets++;
        CheckAchievements(bulletsFiredAchievements, bulletsFiredAchievementsUnlocked, numberOfBullets);
    }

    public void OnDamage(int damage)
    {
        totalDamage += damage;
        CheckAchievements(damageAchievements, damageAchievementsUnlocked, totalDamage);
    }

    public void OnKills()
    {
        enemyKills++;
        CheckAchievements(killsAchievements, killsAchievementsUnlocked, enemyKills);
    }

    private void CheckAchievements(Dictionary<int, string> achievementsDict, HashSet<int> unlockedSet, int currentValue)
    {
        foreach (var achievement in achievementsDict)
        {
            int targetValue = achievement.Key;
            string achievementText = achievement.Value;

            if (currentValue >= targetValue && !unlockedSet.Contains(targetValue))
            {
                achievements.Enqueue(achievementText);
                unlockedSet.Add(targetValue);
            }
        }

        DisplayQueuedAchievement();
    }

    private void OnDisable()
    {
        if (tankPlayer != null)
        {
            tankPlayer.RemoveBulletsFiredObservers(this);
        }

        if (enemyTank != null)
        {
            enemyTank.RemoveDamageObservers(this);
            enemyTank.RemoveKillsObservers(this);
        }
    }
}
