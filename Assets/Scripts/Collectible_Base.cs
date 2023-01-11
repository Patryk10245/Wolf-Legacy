using UnityEngine;

public enum ENUM_CollectibleType
{
    gold,
    health,
    energy
}


public class Collectible_Base : MonoBehaviour
{
    public float amount;

    public ENUM_CollectibleType type;
    public void Collect(Player player)
    {
        switch (type)
        {
            case ENUM_CollectibleType.gold:
                AddGold();
                break;
            case ENUM_CollectibleType.health:
                AddHealth(player);
                break;
            case ENUM_CollectibleType.energy:
                AddEnergy(player);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect(collision.gameObject.GetComponent<Player>());
        }
    }

    void AddGold()
    {
        ScoreTable.ins.currentlyCollectedGold += (int)amount;
    }
    void AddHealth(Player player)
    {
        player.stats.TakeDamage(-amount);
    }
    void AddEnergy(Player player)
    {
        player.stats.ModifyEnergy(amount);
    }
}
