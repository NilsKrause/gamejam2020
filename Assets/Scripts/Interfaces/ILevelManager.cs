public interface ILevelManager
{
    ITurret[] GetBuildableTurrrets();
    bool PurchaseTurret(ITurret turret);
    int GetPlayerLife();
    int GetCurrentGold();
}