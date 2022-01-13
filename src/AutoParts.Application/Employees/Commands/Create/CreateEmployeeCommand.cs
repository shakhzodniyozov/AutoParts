using AutoMapper;
using AutoParts.Application.Identity.Models;
using AutoParts.Application.Repositories;
using MediatR;

namespace AutoParts.Application.Employees.Commands.Create
{
    public class CreateEmployeeCommand : IRequest<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int Salary { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository employeeRepo;
        private readonly IMapper mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepo, IMapper mapper)
        {
            this.employeeRepo = employeeRepo;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employee = mapper.Map<Employee>(request);

            if (employee != null)
            {
                employee = await employeeRepo.Create(employee);
                return employee.Id;
            }

            return 0;
        }
    }
}