using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;

namespace CLH_Final_Project.Implementation.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoomServices(IRoomRepository roomRepository,IWebHostEnvironment webHostEnvironment)
        {
            _roomRepository = roomRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<RoomsResponseModel> Create(CreateRoomRequestModel model)
        {
            var room = await _roomRepository.ExistsAsync(x => x.RoomName == model.RoomName);
            if (room)
            {
                return new RoomsResponseModel
                {
                    Message = "Room already Exist",
                    Sucesss = false,
                };
            }
            var roomImages = new List<Room>();

            for (int i = 0; i < model.Quantity; i++)
            {
                var roomImage = "";
                if (model.ImagePics != null)
                {
                    var imgPath = _webHostEnvironment.WebRootPath;
                    var imagePath = Path.Combine(imgPath, "Images");
                    Directory.CreateDirectory(imagePath);
                    var imagetype = model.ImagePics.ContentType.Split('/')[1];
                    roomImage = $"{Guid.NewGuid()}.{imagetype}";
                    var fullPath = Path.Combine(imagePath, roomImage);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        model.ImagePics.CopyTo(fileStream);
                    }
                }
                var roomNumber = model.RoomNumber + i;
                var rooms = new Room
                {
                    RoomName = model.RoomName,
                    RoomNumber = roomNumber,
                    Description = model.Description,
                    Occupancy = Occupancy(model.Occupancy),
                    RoomImage = roomImage,
                    Price = model.Price,
                    Types = model.Types,
                    Quantity = 1,
                    IsAvailable = true

                };

                await _roomRepository.CreateAsync(rooms);
                roomImages.Add(rooms);


            }

            return new RoomsResponseModel
            {
                Message = "Rooms Created Sucessfully",
                Sucesss = true,
                Data = roomImages.Select(x => new RoomDto
                {
                    Id = x.Id,
                    RoomName = x.RoomName,
                    RoomNumber = x.RoomNumber,
                    Description = x.Description,
                    price = x.Price,
                    Occupancy = x.Occupancy,
                    Quantity = x.Quantity,
                    Types = x.Types,
                    Image = x.RoomImage,

                }).ToList()
               
                
            };




        }

        public int Occupancy(int Occupy)
        {
            if(Occupy > 0)
            {
                return Occupy;
            }
            return 0;

        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var room = await _roomRepository.GetAsync(id);
            if(room == null)
            {
                return new BaseResponse
                {
                    Message = "Room Not Found",
                    Sucesss = false,
                };
            }
            room.IsDeleted = true;
            await _roomRepository.UpdateAsync(room);
            return new BaseResponse
            {
                Message = "Room Has Been Sucessfully Deleted",
                Sucesss = true,
            };
        }

        public async Task<RoomsResponseModel> GetAllAvailableRoom()
        {
            var roomAvailable = await _roomRepository.GetAllAvailableRoomsAsync();
            if (roomAvailable != null)
            {
                foreach (var item in roomAvailable)
                {
                    if(item.IsAvailable == false)
                    {
                        return new RoomsResponseModel
                        {
                            Message = "The room that are Available",
                            Sucesss = true,
                            Data = roomAvailable.Select(x => new RoomDto
                            {
                                Id = x.Id,
                                RoomName = x.RoomName,
                                RoomNumber = x.RoomNumber,
                                Description = x.Description,
                                Occupancy = x.Occupancy,
                                Image = x.RoomImage,
                                price = x.Price,
                                IsAvailable = true,
                                Types = x.Types,
                                Quantity = x.Quantity
                            }).ToList(),
                        };
                    }
                }
              
            }
            return new RoomsResponseModel
            {
                Message = "Not Found",
                Sucesss = false,
            };
        }

        public async Task<RoomResponseModel> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room != null)
            {
                return new RoomResponseModel
                {
                    Message = "Room Found",
                    Sucesss = true,
                    Data = new RoomDto
                    {
                        Id = room.Id,
                        RoomName = room.RoomName,
                        RoomNumber = room.RoomNumber,
                        Description = room.Description,
                        Occupancy = room.Occupancy,
                        Image = room.RoomImage,
                        price = room.Price,
                        Types = room.Types,
                        Quantity = room.Quantity,
                        IsAvailable = true
                    }
                };
            }
            return new RoomResponseModel
            {
                Message = "Room Not Found",
                Sucesss = false,
            };
        }

        public async Task<RoomsResponseModel> GetUnAvailableRoom()
        {
             var roomUnavailable = await _roomRepository.GetUnAvailableRoomsAsync();
            if (roomUnavailable != null)
            {
                foreach (var item in roomUnavailable)
                {
                    if(item.IsAvailable == true)
                    {
                        return new RoomsResponseModel
                        {
                            Message = "The  rooms that are not Available",
                            Sucesss = true,
                            Data = roomUnavailable.Select(x => new RoomDto
                            {
                                Id = x.Id,
                                RoomName = x.RoomName,
                                RoomNumber = x.RoomNumber,
                                Description = x.Description,
                                Occupancy = x.Occupancy,
                                Image = x.RoomImage,
                                price = x.Price,
                                IsAvailable = false,
                                Types = x.Types,
                                Quantity = x.Quantity,
                            }).ToList(),

                        };
                    }
                }
              
            }
            
            return new RoomsResponseModel
            {
                Message = "There is No Room Unavailable",
                Sucesss = false,
            };
        }

        public async Task<RoomResponseModel> UpdateRoomAsync(UpdateRoomRequestModel model, int id)
        {
            var roomUpt = await _roomRepository.GetRoomByIdAsync(id);
            if (roomUpt == null)
            {
                return new RoomResponseModel
                {
                    Message = "Room Not Found",
                    Sucesss = false,
                };
            }
            var roomImage = "";
            if (model.ImagePics != null)
            {
                var imgPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(imgPath, "Images");
                Directory.CreateDirectory(imagePath);
                var imagetype = model.ImagePics.ContentType.Split('/')[1];
                roomImage = $"{Guid.NewGuid()}.{imagetype}";
                var fullPath = Path.Combine(imagePath, roomImage);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.ImagePics.CopyTo(fileStream);
                }
            }
            roomUpt.RoomNumber = model.RoomNumber;
            roomUpt.Occupancy = model.Occupancy;
            roomUpt.RoomName = model.RoomName ?? roomUpt.RoomName;
            roomUpt.Description = model.Description ?? roomUpt.Description;
            roomUpt.Price = model.Price;
            roomUpt.RoomImage = roomImage; 
            
            var room = await _roomRepository.UpdateAsync(roomUpt);
            return new RoomResponseModel
            {
                Message = "Room Has been SuccessFully been Updated",
                Sucesss = true,
                Data = new RoomDto
                {
                    Id = room.Id,
                    RoomName = room.RoomName,
                    RoomNumber = room.RoomNumber,
                    Description = room.Description,
                    Occupancy = room.Occupancy,
                    price = room.Price,
                    Image = room.RoomImage,
                }
            };

        }

        public async Task<RoomsResponseModel> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRooms();
            if (rooms != null)
            {
                foreach (var item in rooms)
                {
                    if (item.IsDeleted == false)
                    {
                        return new RoomsResponseModel
                        {
                            Message = "All The Rooms",
                            Sucesss = true,
                            Data = rooms.Select(x => new RoomDto
                            {
                                Id = x.Id,
                                RoomName = x.RoomName,
                                RoomNumber = x.RoomNumber,
                                Description = x.Description,
                                Occupancy = x.Occupancy,
                                Image = x.RoomImage,
                                price = x.Price,
                                Quantity = x.Quantity,
                                Types = x.Types
                            }).ToList(),
                        };
                    }
                }
            }
            return new RoomsResponseModel
            {
                Message = "Not Found",
                Sucesss = false,
            };
        }

        /*private bool ImageExistsWithHash(string hash, string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath);

            foreach (var filePath in files)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    using (var md5 = System.Security.Cryptography.MD5.Create())
                    {
                        var hashBytes = md5.ComputeHash(fileStream);
                        var existingHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                        if (existingHash == hash)
                        {
                            return true; 
                        }
                    }
                }
            }

            return false; 
        }*/




    }
}
