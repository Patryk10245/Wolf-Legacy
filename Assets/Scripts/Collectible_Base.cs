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
        //ScoreTable.ins.currentlyCollectedGold += (int)amount;
        ScoreTable.ins.AddGold((int)amount);
        Destroy(gameObject);
    }
    void AddHealth(Player player)
    {
        if(player.stats.currentHealth >= player.stats.maxHealth)
        {
            return;
        }
        player.stats.TakeDamage(-amount);
        Destroy(gameObject);
    }
    void AddEnergy(Player player)
    {
        if (player.stats.currentEnergy >= player.stats.maxEnergy)
        {
            return;
        }
        player.stats.ModifyEnergy(amount);
        Destroy(gameObject);

    }
}
