namespace MarketplaceBlazor.DtoModel.ConversationsModel
{
    public class NewMessageModel
    {
        public Guid ToUserId { get; set; }

        public required string Text { get; set; }
    }
}
