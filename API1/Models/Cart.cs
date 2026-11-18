namespace API1.Models;

public partial class Cart
{
    public int IdCart { get; set; }

    public int UserId { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual User User { get; set; } = null!;
}
