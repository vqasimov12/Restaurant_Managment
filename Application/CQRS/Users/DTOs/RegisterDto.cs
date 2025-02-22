﻿using Common.GlobalResopnses.Generics;
using MediatR;

namespace Application.CQRS.Users.DTOs;

public class RegisterDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

}
