using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.EmailServices;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Enum;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;
using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Diagnostics;

namespace CLH_Final_Project.Implementation.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailServices;
        private readonly IHistoryRepository _historyRepository;
        public BookingServices(IBookingRepository bookingRepository, IRoomRepository roomRepository, IUserRepository userRepository, IMailServices mailServices, IHistoryRepository historyRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _mailServices = mailServices;
            _historyRepository = historyRepository;
        }

        public async Task<BookingsResponseModel> CreateBooking(CreateBookingRequestModel model, int userId)
        {
            List<BookingDto> bookingDtos = new List<BookingDto>();

            
            if (model == null ||model.BookingItems == null || model.BookingItems.Count == 0)
            {
                throw new ArgumentException("Invalid booking request");
            }

            // Calculate total duration for the entire booking
            int totalDuration = 0;
            foreach (var item in model.BookingItems)
            {
                totalDuration += item.Quantity;
            }
            var user = await _userRepository.GetUserById(userId);
            // Create individual BookingDto objects for each room
            foreach (var item in model.BookingItems)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    var book = new Booking
                    {
                        CheckIn = model.CheckIn,
                        CheckOut = model.CheckOut.AddDays(model.Duration),
                        Duration = model.Duration,
                        Bookings = BookingStatus.pending,
                        ReferenceNo = GenerateNo(),
                        RoomId = item.RoomId,
                        Quantity = item.Quantity,
                        DateCreated = DateTime.Now,
                    };

                    await _bookingRepository.CreateAsync(book);

                    var hist = new History
                    {
                        BookingId = book.Id,
                        CustomerId = user.Customer.Id,
                        DateCreated =  DateTime.Now
                    };
                    await _historyRepository.CreateAsync(hist);


             

                    var room = await _roomRepository.GetRoomByIdAsync(item.RoomId);
                    if (room != null)
                    {
                        room.IsAvailable = true; 
                        await _roomRepository.UpdateAsync(room);
                    }

                    
                    var sender = new MailRequset
                    {
                        Subject = "Booking @SkyBoxHotel",
                        ToEmail = user.Email,
                        ToName = user.Name,
                        HtmlContent = $"<html><body><h1>Hello {user.Name}, You Have Successfully Book a Room With the RoomNumber {room.RoomNumber} and your BookingCard {book.ReferenceNo} </h1><h4>Your BookingCard will expire in {book.CheckOut}</h4></body></html>",
                    };
                    _mailServices.SendEmailAsync(sender);

                    var bookingDto = new BookingDto
                    {
                        Id = book.Id, 
                        CheckIn = book.CheckIn,
                        CheckOut = book.CheckOut,
                        Duration = book.Duration,
                        Bookings = book.Bookings,
                        ReferenceNo = book.ReferenceNo,
                        Quantity = book.Quantity,
                    };

                    
                    bookingDtos.Add(bookingDto);
                }
            
            }


            return new BookingsResponseModel
            {
                Message = "Booking Created Sucessfully",
                Sucesss = true,
                Data = bookingDtos.Select(x => new BookingDto
                {
                    Id = x.Id,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    Duration = x.Duration,
                    Quantity = x.Quantity,

                }).ToList(),
            };
        }
    




        public async Task<BookingsResponseModel> GetAllBookings()
        {
            var bookings = await _bookingRepository.GetAllBookingAsync();
            if(bookings == null)
            {
                return new BookingsResponseModel
                {
                    Message = "Bookings Not Found",
                    Sucesss = false
                };
            }
            return new BookingsResponseModel
            {
                Message = "Bookings found",
                Sucesss = true,
                Data = bookings.Select(x => new BookingDto
                {
                    Id = x.Id,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    Duration = x.Duration,
                    ReferenceNo = x.ReferenceNo,
                    Bookings = x.Bookings,

                }).ToList()

            };
        }

        public async Task<BookingResponseModel> GetBookingById(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking != null)
            {
                return new BookingResponseModel
                {
                    Message = "Booking found",
                    Sucesss = true,
                    Data = new BookingDto
                    {
                        Id = booking.Id,
                        CheckIn = booking.CheckIn,
                        CheckOut = booking.CheckOut,
                        Duration = booking.Duration,
                        Quantity = booking.Quantity, 
                        ReferenceNo = booking.ReferenceNo,
                    }
                };
            }
            return new BookingResponseModel
            {
                Message = " BookingId Not Found",
                Sucesss = false,
            };
        }





        public async Task<BaseResponse> TerminateBooking(int id,int userId)
        {
            var booking = await _bookingRepository.GetAsync(x => x.Id == id);
            var roomUpt = await _roomRepository.GetRoomByIdAsync(booking.RoomId);
            var user = await _userRepository.GetAsync(userId);
            if (booking != null && booking.Bookings != BookingStatus.CheckedOut)
            {
                if (booking.Bookings == BookingStatus.pending)
                {
                    booking.Terminate = DateTime.Now;
                    booking.Bookings = BookingStatus.Terminate;
                    roomUpt.IsAvailable = false;
                    await _roomRepository.UpdateAsync(roomUpt);
                    
                    var sender = new MailRequset
                    {
                        Subject = "Booking @SkyBoxHotel",
                        ToEmail = user.Email,
                        ToName = user.Name,
                        HtmlContent = $"<html><body><h1>Hello {user.Name}, You Have Successfully Canceled your Booking </h1><h4>Your booking was canceled on {booking.Terminate}</h4></body></html>",
                    };
                    _mailServices.SendEmailAsync(sender);

                }
                return new BaseResponse
                {
                    Message = "Booking Successfully terminated",
                    Sucesss = true
                };
            }
            return new BaseResponse
            {
                Message = "Booking Was not Successfully terminated or Booking Has Already Been CheckedOut ",
                Sucesss = false,
            };
        }

        public async Task<BookingResponseModel> UpdateCheckIn(UpdateBookingRequestModel model)
        {
            var bookingCd = await _bookingRepository.GetBookingByCard(model.ReferenceNo);
            if (bookingCd == null)
            {
                return new BookingResponseModel
                {
                    Message = "Booking Not Found",
                    Sucesss = false,
                };
            }
            if (bookingCd.Bookings == BookingStatus.CheckedOut || bookingCd.Bookings == BookingStatus.Terminate)
            {
                return new BookingResponseModel
                {
                    Message = "BookingCard Already used",
                    Sucesss = false,
                };
            }
            else
            {
                
                bookingCd.CheckIn = DateTime.Now;
                bookingCd.Bookings = BookingStatus.CheckedIn;
                 await _bookingRepository.UpdateAsync(bookingCd);
                return new BookingResponseModel
                {
                    Message = "You have Successfully CheckedIn",
                    Sucesss = true,

                };

            }
        }

        public async Task<BookingResponseModel> UpdateCheckOut(UpdateBookingRequestModel model, int userId)
        {
            var bookingCd = await _bookingRepository.GetBookingByCard(model.ReferenceNo);
            var roomUpt = await _roomRepository.GetRoomByIdAsync(bookingCd.RoomId);
            var user = await _userRepository.GetAsync(userId);

            if (bookingCd == null)
            {
                return new BookingResponseModel
                {
                    Message = "Booking Card not Found",
                    Sucesss = false
                };
            }
            if (bookingCd.Bookings == BookingStatus.CheckedOut || bookingCd.Bookings == BookingStatus.Terminate)
            {
                return new BookingResponseModel
                {
                    Message = "BookingCard Already used Or It has been Terminated",
                    Sucesss = false,
                };
            }
            else
            {
                bookingCd.CheckOut = DateTime.Now;
                bookingCd.Bookings = BookingStatus.CheckedOut;
                await _bookingRepository.UpdateCheckOutDateAsync(bookingCd.Id);
                roomUpt.IsAvailable = false;
                await _roomRepository.UpdateAsync(roomUpt);
                var sender = new MailRequset
                {
                    Subject = "Booking @SkyBoxHotel",
                    ToEmail = user.Email,
                    ToName = user.Name,
                    HtmlContent = $"<html><body><h1>Hello {user.Name}, You Have Successfully CheckedOut your Booking </h1><h4>Your booking was checkedout on {bookingCd.CheckOut}</h4></body></html>",
                };
                _mailServices.SendEmailAsync(sender);
                return new BookingResponseModel
                {
                    Message = "You Have Successfully Checked Out",
                    Sucesss = true,
                };
                
            }
        }

       /* private async Task<int> Duration(int duration)
        {
            *//*  var bookings = await _bookingRepository.GetAllBookingAsync();
              foreach (var booking in bookings)
              {
                  if (booking.Bookings == BookingStatus.CheckedIn && booking.CheckIn.AddMinutes(1 * duration) >= booking.CheckOut)
                  {
                      booking.CheckOut = DateTime.Now;
                      booking.Bookings = BookingStatus.CheckedOut;
                      await Console.Out.WriteLineAsync("It work oooo");
                     await  _bookingRepository.UpdateAsync(booking);
                      booking.Room.IsAvailable = false;
                      await _roomRepository.UpdateAsync(booking.Room);
                  }
              }*//*
            //await CountDown(duration);
            
            
            return await CountDown(duration);

        }*/

        private string GenerateNo()
        {
            Random rand = new();
            return $"BookingCard/Num/{rand.Next(10000, 99999)}";
        }

        private  async Task<int> CountDown(int duration)
        {
            var booking = await _bookingRepository.GetAllBookingAsync();
            
             int durations =  duration * 24;  // Set the countdown duration in second
             Stopwatch stopwatch = new Stopwatch();
               stopwatch.Start();

            //         // Store the initial cursor position
                    int cursorTop = Console.CursorTop;

                     bool outputDisplayed = false;  // Flag to track if the output has been displayed

                    while (true)
                     {
                       TimeSpan elapsedTime = stopwatch.Elapsed;
                       TimeSpan remainingTime = TimeSpan.FromMinutes(duration) - elapsedTime;

                        if (remainingTime.TotalMilliseconds <= 0)
                        {
                             Console.SetCursorPosition(0, cursorTop);
                            Console.WriteLine("Countdown Complete!");
                            foreach(var book in booking)
                            {
                                if (book.Bookings == BookingStatus.pending || book.Bookings == BookingStatus.CheckedIn)
                                {
                                    book.CheckOut = DateTime.Now;
                                    book.Bookings = BookingStatus.CheckedOut;
                                    await _bookingRepository.UpdateAsync(book);
                                    book.Room.IsAvailable = false;
                                    await _roomRepository.UpdateAsync(book.Room);
                            

                                }
                                
                            }
                            break;
                        }

                        if (!outputDisplayed)  // Check if the output has been displayed already
                        {
                            Console.SetCursorPosition(0, cursorTop);
                           Console.WriteLine("Time Test Time Remaining: {0} day(s) {1} hrs {2} min {3} sec {4}", remainingTime.Days, remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds, DateTime.Now);
                             outputDisplayed = true;  // Set the flag to indicate that the output has been displayed
                             // BookingMain();                                                                                                                                                                                                           
                         }

                // Adjust the sleep duration to change the update interval
                        await Task.Delay(1000);
                   }
            return durations;
        }
    }
}
