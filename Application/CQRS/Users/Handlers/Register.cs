﻿using Application.CQRS.Users.DTOs;
using AutoMapper;
using Common.Exceptions;
using Common.GlobalResopnses.Generics;
using Common.Security;
using Domain.Entites;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;
public class Register
{
    public class Command : IRequest<ResponseModel<RegisterDto>>
    {
        public Command(string name, string surname, string email, string phone, string password)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Password = password;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, ResponseModel<RegisterDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ResponseModel<RegisterDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);

            if (currentUser is not null)
                throw new BadRequestException("User already exists with provided email");

            var user = _mapper.Map<User>(request);
            var hashPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);
            user.PasswordHash = hashPassword;
            user.CreatedBy = 1;

            await _unitOfWork.UserRepository.RegisterAsync(user);
            await _unitOfWork.SaveChanges();

            var response = _mapper.Map<RegisterDto>(user);

            return new ResponseModel<RegisterDto>
            {
                Data = response,
                Errors = [],
                IsSuccess = true
            };
        }
    }
}