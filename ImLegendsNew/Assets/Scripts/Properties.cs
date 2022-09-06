using UnityEngine;

[System.Serializable]
public class Properties 
{
    [Range(0, 300)] public float speed;
    [Range(0,300)] public float armor;
    [Range(0,200)] public float fuelTank;
    [Range(0,100)] public float zombieResist;
    [Range(0,10)] public float monsterDuration;
    [Range(0,10)] public float comboDuration;

    public Properties GetValues()
    {
        Properties prop = new()
        {
            speed = speed,
            armor = this.armor,
            fuelTank = this.fuelTank,
            zombieResist = this.zombieResist,
            monsterDuration = this.monsterDuration,
            comboDuration = this.comboDuration
        };

        return prop;
    }
}
