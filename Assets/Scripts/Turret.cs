using TMPro;
using UnityEngine;

public class Turret : Building, IReceiver
{
    [SerializeField] Transform barrelMount;
    [SerializeField] TMP_Text counterLabel;
    [SerializeField] float spinSpeed = 180f;
    [SerializeField] float spinPerGear = 3f;
    [SerializeField] float maxFuel = 9f;

    float fuel = 0f;
    int eaten = 0;

    public bool CanAccept(Direction travelDirection)
    {
        return fuel < maxFuel;
    }

    public bool AddOre(Ore ore, Direction travelDirection)
    {
        if (CanAccept(travelDirection) == false) return false;
        if (ore.itemType != data.inputType) return false;

        Destroy(ore.gameObject);
        eaten++;
        counterLabel.text = eaten.ToString();
        fuel = Mathf.Min(fuel + spinPerGear, maxFuel);
        return true;
    }

    void Update()
    {
        if (fuel <= 0f) return;
        fuel -= Time.deltaTime;
        barrelMount.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }

    void LateUpdate()
    {
        counterLabel.transform.rotation = Camera.main.transform.rotation;
    }
}
