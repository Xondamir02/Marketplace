namespace Marketplace.Services.Organizations.Entities;

public class OrganizationAddress
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public Organization? Organization { get; set; }

    public required string Address { get; set; }
}