﻿namespace ServiceManagerBackEnd.Models.Requests;

public class CreateUserRequest
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}