namespace Marketplace.Services.Organizations.Entities;


public class Organization
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public string? Logo { get; set; }
    public string? Contact { get; set; }

    public List<OrganizationUser>? Users { get; set; }
    public List<OrganizationAddress>? Addresses { get; set; }
}