namespace Race.Speed
{
    public class ConstantSpeed(double speed) : Speed
    {
        public override double GetSpeed(double time) => speed;
    }
}