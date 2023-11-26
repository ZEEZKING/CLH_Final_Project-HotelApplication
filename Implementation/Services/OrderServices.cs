using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;

namespace CLH_Final_Project.Implementation.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderServices(IOrderRepository orderRepository, IPackageRepository packageRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _packageRepository = packageRepository;
            _customerRepository = customerRepository;
        }

        public async Task<BaseResponse> CreateOrderAsync(int userId, int id)
        {
           var customer = await _customerRepository.GetAsync(userId);
            var package = await _packageRepository.GetPackagesByIdAsync(id);
            if (customer == null)
            {
                return new BaseResponse
                {
                    Message = "Customer not found",
                    Sucesss = false
                };
            }

            var ord = new Order
            {
                CustomerId = customer.Id,
                PackagesId = package.Id,
            };
            await _orderRepository.CreateAsync(ord);
            return new BaseResponse
            {
                Message = "Order Created",
                Sucesss = true
            };
        }

        public async Task<OrdersResponseModel> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            var pack = await _packageRepository.GetAllPackagesAsync();
            if (orders == null)
            {
                return new OrdersResponseModel
                {
                    Message = "Order empty",
                    Sucesss = false
                };
            }
            return new OrdersResponseModel
            {
                Message = "Order Successfully found",
                Sucesss = true,
                Data = orders.Select(x => new OrderDto
                {
                   Packages = new PackagesDto
                   {
                     Id = x.Id,
                     Name = x.Packages.Name,
                     Description = x.Packages.Description,
                     Images = x.Packages.Images,
                     Price = x.Packages.Price,
                     Types = x.Packages.Types
                   },

                }).ToList(),
            };
        }

        public async Task<OrderResponseModel> GetOrderByIdAsync(int id)
        {
           var order = await _orderRepository.GetAsync(id);
            if(order == null)
            {
                return new OrderResponseModel
                {
                    Message = "Order not found",
                    Sucesss = false
                };
            }
            return new OrderResponseModel
            {
                Message = "Sucessfully found",
                Sucesss = true,

            };
        }
    }
}
