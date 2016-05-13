namespace Assets.Scripts.General
{
    public interface IHealthBehavior
    {
        void TakeDamage(DamageContext context);
        void Die(DamageContext context);
    }
}