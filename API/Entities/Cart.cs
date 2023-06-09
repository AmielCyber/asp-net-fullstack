namespace API.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        // May be replaced with an ordered list.
        // Many to One Relationship.
        public List<CartItem> Items { get; set; } = new();
        // Both properties below will be used for the client to make a payment with stripe directly.
        // For stripe payment intent id, before the user makes a payment.
        public string PaymentIntentId { get; set; }
        // For stripe client secret.
        public string ClientSecret { get; set; }

        public void AddItem(Product product, int quantity)
        {
            // If is not in our list already.
            if (Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
        }

        public void RemoveItem(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return;
            item.Quantity -= quantity;
            if (item.Quantity < 1)
            {
                Items.Remove(item);
            }
        }

    }
}