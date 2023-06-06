namespace Marketplace.Services.Organizations.Entities
{
    public class OrganizationUser
    {
        public Guid OrganizationId { get; set; }
        public Guid UserId { get; set; }
        public OrganizationUserRole UserRole { get; set; }
    }
}
