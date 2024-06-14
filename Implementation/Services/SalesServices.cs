using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;

namespace CLH_Final_Project.Implementation.Services
{
    public class SalesServices : ISalesServices
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBookingRepository _bookingRepository;
        public SalesServices(ISalesRepository salesRepository, IOrderRepository orderRepository, IBookingRepository bookingRepository)
        {
            _salesRepository = salesRepository;
            _orderRepository = orderRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<SalesResponseModel> CalculateAllMonthlySalesAsync(int year)
        {
            List<double> percentagesales = new List<double>();
            int month = 1;

            for (int i = 1; i < 13; i++)
            {
               
                var sale = await _salesRepository.GetTotalMonthlySalesAsync(month,year);

                if (sale == 0)
                {
                    percentagesales.Add(0);
                }
                else
                {
                    percentagesales.Add(100); 
                }

                month++;
            }

            var salesDtos = percentagesales.Select(x => new SalesDto
            {
                AmountPaid = Math.Ceiling(x)
            }).ToList();

            return new SalesResponseModel
            {
                Message = "Successful",
                Sucesss = true,
                Data = salesDtos
            };

        }

        public async Task<BaseResponse> CreateSales(int id,int bookingId)
        {
            var order = await _orderRepository.GetAsync(id);
            var book = await _bookingRepository.GetAsync(bookingId);
            double totalAmount = order.Payment.Amount + book.Payment.Amount;
            var sales = new Sale
            {
                OrderId = order.Id,
                BookingId = book.Id,
                AmountPaid = totalAmount
            };
            await _salesRepository.CreateAsync(sales);
            return new BaseResponse
            {
                Message = "Sales Created Sucessfully",
                Sucesss = true
            };
            
        }

        public async Task<SalesResponseModel> GetAllSales()
        {
            var sales = await _salesRepository.GetAllSales();
            if (sales.Count == 0)
            {
                return new SalesResponseModel
                {
                    Message = "No Sales Found",
                    Sucesss = false
                };
            }
            var salesDtos = sales.Select(MapSaleToSalesDto).ToList();
            return new SalesResponseModel
            {
                Message = "Sales Are Available",
                Sucesss = true,
                Data = salesDtos
            };
        }

        public Task<SalesResponseModel> GetSalesByCustomerNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<SalesResponseModel> GetSalesByProductNameForTheMonth(int productId, int month, int year)
        {
            throw new NotImplementedException();
        }

        public async Task<SalesResponseModel> GetSalesForThisMonth()
        {
            var sales = await _salesRepository.GetThisMonthSales();
            if(sales.Count == 0)
            {
                return new SalesResponseModel
                {
                    Message = "No Sales Available for this Month",
                    Sucesss = false
                };
            }

            double monthlySales = sales.Sum(s => s.AmountPaid);
            var salesDtos = sales.Select(MapSaleToSalesDto).ToList();
            return new SalesResponseModel
            {
                Message = "Sales Are Available",
                Sucesss = true,
                Data = salesDtos
            };
        }

        public async Task<SalesResponseModel> GetSalesForThisYear()
        {

            var sales = await _salesRepository.GetThisYearSales();
            if(sales.Count == 0)
            {
                return new SalesResponseModel
                {
                    Message = "No Sales found for this year",
                    Sucesss = false
                };
            }
            double totalYearlySales = sales.Sum(s => s.AmountPaid);

            var salesDtos = sales.Select(MapSaleToSalesDto).ToList();

            
            var responseModel = new SalesResponseModel
            {
                Sucesss = true,
                Message = "Yearly sales retrieved successfully",
                Data = salesDtos
               
            };

            return responseModel;


        }


        private SalesDto MapSaleToSalesDto(Sale sale)
        {
            return new SalesDto
            {
                AmountPaid = sale.AmountPaid,
                CustomerDto = MapCustomerDto(sale.Order.Customer),
                OrderId = sale.OrderId,
                OrderDto = sale.Order != null ? new List<OrderDto> { MapOrderDto(sale.Order) } : null,
                BookingDtos = sale.Booking != null ? new List<BookingDto> { MapBookingDtos(sale.Booking)} : null
            };
        }



        private CustomerDto MapCustomerDto(Customer customer)
        {
            return new CustomerDto
            {
                
                Id = customer.Id,
                Name = customer.User.Name,
                Email = customer.User.Email,
                PhoneNumber = customer.User.PhoneNumber,
                Address = customer.User.Address
            };
        }

        private OrderDto MapOrderDto(Order order)
        {
            return new OrderDto
            {
                
               Packages = new PackagesDto
               {
                   Id = order.Packages.Id,
                   Name = order.Packages.Name,
                   Description = order.Packages.Description,
                   Types = order.Packages.Types,
                   Price = order.Packages.Price,
                   Images = order.Packages.Images
               }
                
            };
        }

       

        private BookingDto MapBookingDtos(Booking booking)
        {
            return new BookingDto
            {

                Id = booking.Id,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                ReferenceNo = booking.ReferenceNo,
                Quantity = booking.Quantity,

            };
        }
    }
}
