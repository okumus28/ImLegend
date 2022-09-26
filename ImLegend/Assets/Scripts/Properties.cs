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
            speed = this.speed,
            armor = this.armor,
            fuelTank = this.fuelTank,
            zombieResist = this.zombieResist,
            monsterDuration = this.monsterDuration,
            comboDuration = this.comboDuration
        };

        return prop;
    }

    public static Properties AddPropertiesValue(Properties mainProp , Properties addProp , int islem)
    {
        mainProp.speed += addProp.speed * islem;
        mainProp.armor += addProp.armor * islem;
        mainProp.fuelTank += addProp.fuelTank * islem;
        mainProp.zombieResist += addProp.zombieResist * islem;
        mainProp.monsterDuration += addProp.monsterDuration * islem;
        mainProp.comboDuration += addProp.comboDuration * islem;

        return mainProp;
    }
}
