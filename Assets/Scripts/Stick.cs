public class Stick : A_Item
{
    public override void Use(A_Entity entity)
    {
        entity.TakeDamage(_ItemData.Damage);
    }
}