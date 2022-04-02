namespace Prototype.Collectables
{
    public class Tomato : HealthCollectable
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            this.HealthValue = 2;
        }
    }
}
