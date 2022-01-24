using AutoMapper;
using AutoParts.Application.Consignments.Queries;
using AutoParts.Application.Repositories;
using AutoParts.Domain.Entities;
using MediatR;

namespace AutoParts.Application.Consignments.Commands.Create;

public class CreateConsignmentCommand : IRequest<ConsignmentDto>
{
    public DateTime Date { get; set; }
    public Dictionary<int, int> ProductsList { get; set; } = null!;
}

public class CreateConsignmentCommandHandler : IRequestHandler<CreateConsignmentCommand, ConsignmentDto>
{
    private readonly IConsignmentRepository consignmentRepo;
    private readonly IMapper mapper;

    public CreateConsignmentCommandHandler(IConsignmentRepository consignmentRepo, IMapper mapper)
    {
        this.consignmentRepo = consignmentRepo;
        this.mapper = mapper;
    }

    public async Task<ConsignmentDto> Handle(CreateConsignmentCommand request, CancellationToken cancellationToken)
    {
        Consignment consignment = new() { Date = request.Date };

        foreach (var s in request.ProductsList)
        {
            consignment.ConsignmentDetails.Add(new()
            {
                Consignment = consignment,
                ConsignmentId = consignment.Id,
                ProductId = s.Key,
                Quantity = s.Value
            });
        }

        consignment = await consignmentRepo.Create(consignment);

        return mapper.Map<ConsignmentDto>(consignment);
    }
}