﻿namespace MarketplaceBlazor.DtoModels;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public  string Username { get; set; }
    public string Password { get; set; } = null!;
}