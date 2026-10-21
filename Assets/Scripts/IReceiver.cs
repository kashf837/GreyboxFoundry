public interface IReceiver
{
    bool CanAccept(Direction travelDirection);
    bool AddOre(Ore ore, Direction travelDirection);
}
