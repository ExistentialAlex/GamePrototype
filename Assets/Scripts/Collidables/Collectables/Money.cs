namespace Prototype.Collectables
{
    public class Money : Collectable
    {
        protected int MoneyAmount { get; set; }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            this.MoneyAmount = 0;
        }
    }
}
