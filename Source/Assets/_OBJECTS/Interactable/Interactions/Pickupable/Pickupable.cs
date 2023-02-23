public class Pickupable : Interaction
{
    public string itemName = "Default";   
    public override void Do()
    {
        Game.Get().Player.GetComponent<Interact>().HoldItem(this.gameObject);
    }
}
