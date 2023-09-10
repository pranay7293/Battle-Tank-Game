public interface IObserver
{
    public void OnBulletsFired();

    public void OnDamage(int damage);

    public void OnKills();
}