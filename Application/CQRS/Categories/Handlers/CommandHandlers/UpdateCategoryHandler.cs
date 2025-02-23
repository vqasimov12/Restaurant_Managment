using Application.CQRS.Categories.Commands.Requests;
using Application.CQRS.Categories.Commands.Responses;
using AutoMapper;
using Common.GlobalResopnses.Generics;
using Domain.Entites;
using FluentValidation;
using MediatR;
using Repository.Common;
using System.ComponentModel.DataAnnotations;

namespace Application.CQRS.Categories.Handlers.CommandHandlers;

public class UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCategoryRequest> validator) : IRequestHandler<UpdateCategoryRequest, ResponseModel<UpdateCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateCategoryRequest> _validator = validator;

    public async Task<ResponseModel<UpdateCategoryResponse>> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);
        await _unitOfWork.CategoryRepository.Update(category);

        var response = _mapper.Map<UpdateCategoryResponse>(category);

        return new ResponseModel<UpdateCategoryResponse>
        {
            Data = response,
            Errors = [],
            IsSuccess = true
        };
    }
}
