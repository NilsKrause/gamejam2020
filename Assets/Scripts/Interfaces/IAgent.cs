public interface IAgent
{
    float GetSpeed();
    int GetCurrentHealth();
    void DealDamage(int damage);

    AgentStats GetStats();
}