using UnityEngine;

public class Turret : Building, IReceiver
{
    [SerializeField] Transform barrelMount;   // 炮管的转轴
    [SerializeField] float spinSpeed = 180f;  // 度/秒
    [SerializeField] float spinPerGear = 3f;  // 一颗齿轮能让它转几秒
    [SerializeField] float maxFuel = 9f;      // 料仓上限（秒）

    float fuel = 0f;

    public bool CanAccept(Direction travelDirection)
    {
        return fuel < maxFuel;
    }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (CanAccept(travelDirection) == false) return false;
        if (ore.itemType != data.inputType) return false;

        Destroy(ore.gameObject);
        fuel = Mathf.Min(fuel + spinPerGear, maxFuel);
        return true;
    }

    void Update()
    {
        if (fuel <= 0f) return;

        fuel -= Time.deltaTime;
        barrelMount.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }
}
