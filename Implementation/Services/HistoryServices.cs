using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;

namespace CLH_Final_Project.Implementation.Services
{
    public class HistoryServices : IHistoryServices
    {
        private readonly IHistoryRepository _historyRepository;
        public HistoryServices(IHistoryRepository  historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public async Task<HistorysResponseModel> GetAllHistory()
        {
           var history = await _historyRepository.GetAllHistorys();
            if(history != null)
            {
                return new HistorysResponseModel
                {
                    Message = "History Found Successfully",
                    Sucesss = true,
                    Data = history.Select(x => new HistoryDto
                    {
                        Id = x.Id,
                        BookingId = x.BookingId,
                        CustomerId = x.CustomerId,
                        BookingDtos = new BookingDto
                        {
                            Id = x.Bookings.Id,
                            CheckIn = x.Bookings.CheckIn,
                            CheckOut = x.Bookings.CheckOut,
                            Duration = x.Bookings.Duration,
                            ReferenceNo = x.Bookings.ReferenceNo,
                            Quantity = x.Bookings.Quantity,
                        }
                    }).ToList()
                };
               
            }
            return new HistorysResponseModel
            {
                Message = "History Not Found",
                Sucesss = false
            };
        }

        public async Task<HistoryResponseModel> GetHistoryByCustomerId(int customerId)
        {
           var customerHistory =  await _historyRepository.GetHistoryByCustomerId(customerId);
            if(customerHistory != null)
            {
                return new HistoryResponseModel
                {
                    Message = "History was found sucessfully",
                    Sucesss = true,
                    Data = new HistoryDto
                    {
                        Id = customerHistory.Id,
                        BookingId = customerHistory.BookingId,
                        BookingDtos = new BookingDto
                        {
                            CheckIn = customerHistory.Bookings.CheckIn,
                            CheckOut = customerHistory.Bookings.CheckOut,
                            Terminate = customerHistory.Bookings.Terminate,
                            Duration = customerHistory.Bookings.Duration,
                            ReferenceNo = customerHistory.Bookings.ReferenceNo,
                            Quantity =  customerHistory.Bookings.Quantity,
                        }

                    }
                };
            }
            return new HistoryResponseModel
            {
                Message = "History Not Found",
                Sucesss = false,
            };
        }

        public async Task<HistoryResponseModel> GetHistoryById(int id)
        {
            var history = await _historyRepository.GetHistoryById(id);
            if(history != null)
            {
                return new HistoryResponseModel
                {
                    Message = "History was found sucessfully",
                    Sucesss = true,
                    Data = new HistoryDto
                    {
                        Id = history.Id,
                        BookingId = history.BookingId,
                        BookingDtos = new BookingDto
                        {
                            CheckIn = history.Bookings.CheckIn,
                            CheckOut = history.Bookings.CheckOut,
                            Terminate = history.Bookings.Terminate,
                            Duration = history.Bookings.Duration,
                            ReferenceNo = history.Bookings.ReferenceNo,
                            Quantity = history.Bookings.Quantity,
                        }

                    }
                };
            }
            return new HistoryResponseModel
            {
                Message = "History Not Available",
                Sucesss = false,
            };
        }
    }
}
