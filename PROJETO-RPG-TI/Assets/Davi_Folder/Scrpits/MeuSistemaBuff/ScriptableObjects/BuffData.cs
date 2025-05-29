using UnityEngine;

public interface IBuff
{
    void Apply(PlayerStats playerStats);
    void Remove(PlayerStats playerStats);
}

[CreateAssetMenu(fileName = "NewBuff", menuName = "Buff/BuffData")]
public class BuffData : ScriptableObject, IBuff
{
    public string buffName;
    public string description;
    public float duration;
    public float effectValue;
    public Sprite icon;
    public int cost;

    public void Apply(PlayerStats playerStats)
    {
        playerStats.damageMultiplier += effectValue;

        if (duration > 0)
        {
            playerStats.RemoveBuffAfterDuration(this, duration);
        }
    }

    public void Remove(PlayerStats playerStats)
    {
        playerStats.damageMultiplier -= effectValue;
    }
}