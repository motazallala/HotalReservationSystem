using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Data;
using WebApplication2.Data.Model;
using WebApplication2.Models.Room;
using WebApplication2.Models.RoomType;
using WebApplication2.services;
using WebApplication2.services.Common;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRoomImageService _roomImageService;

        public RoomsController(WebApplication2DBContext context, IRoomService roomService, IRoomTypeService roomTypeService, IRoomImageService roomImageService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _roomImageService = roomImageService;
        }

        public async Task<IActionResult> Index(int id = 1, int pageSize = 5, bool availableOnly = false, int minCapacity = 0, int? type = null)
        {
            var searchResults = await _roomService.GetSearchResults<RoomViewModel>(availableOnly, minCapacity, type);
            var resultsCount = searchResults.Count();
            if (pageSize <= 0)
            {
                pageSize = 5;
            }
            var pages = (int)Math.Ceiling((double)resultsCount / pageSize);
            if (id > resultsCount || id < 1)
            {
                id = 1;
            }

            var room = new RoomIndexVirwModel
            {
                PagesCount = pages,
                CurrentPage = id,
                Rooms = searchResults.GetPageItems(id, pageSize),
                Controller = "Rooms",
                Action = nameof(Index),
                AvailableOnly = availableOnly,
                MinCapacity = minCapacity,
                MaxCapacity = await _roomService.GetMaxCapacity(),
                roomType = type,
                roomTypes = await _roomService.GetRoomTypeList<RoomTypeViewModel>()
            };

            return View(room);
        }

        public async Task<IActionResult> Create()
        {
            // Fetch available room types from the database and populate the dropdown list
            var availableRoomTypes = await _roomTypeService.GetAllRoomTypes();

            var model = new RoomWithRoomTypeInputModel
            {
                RoomTypes = availableRoomTypes.Select(rt => new SelectListItem
                {
                    Value = rt.RoomTypeId.ToString(),
                    Text = rt.Type
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomWithRoomTypeInputModel model)
        {
            if (ModelState.IsValid)
            {
                var room = new Room
                {
                    Capacity = model.Input.Capacity,
                    AdultPrice = model.Input.AdultPrice,
                    ChildrenPrice = model.Input.ChildrenPrice,
                    RoomNumber = model.Input.RoomNumber,
                    RoomTypeId = model.Input.RoomTypeId
                };

                await _roomService.Add(room, model.Input.RoomImagesFile);

                // Redirect to the room list or any other desired page after successful addition
                return RedirectToAction("Index");
            }

            // If there are any validation errors, display the create view again with the model
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, RoomInputModel input)
        {
            // Retrieve the room and its images from the database
            var rooms = await _roomService.GetId<RoomInputModel>(id);

            if (rooms == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            // Map the room to the view model and pass it to the edit view
            var viewModel = new RoomInputModel
            {
                Capacity = rooms.Capacity,
                AdultPrice = rooms.AdultPrice,
                ChildrenPrice = rooms.ChildrenPrice,
                RoomNumber = rooms.RoomNumber,
                RoomTypeId = rooms.RoomTypeId,
                RoomImages = rooms.RoomImages, // Assign the existing images to the view model
                RoomImagesFile = rooms.RoomImagesFile
            };

            // Fetch available room types from the database and populate the dropdown list
            var availableRoomTypes = await _roomTypeService.GetAllRoomTypes();
            viewModel.RoomTypes = availableRoomTypes.Select(rt => new SelectListItem
            {
                Value = rt.RoomTypeId.ToString(),
                Text = rt.Type
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomInputModel viewModel, List<int> imageIds)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var room = await _roomService.GetId<Room>(id);

            if (room == null)
            {
                return NotFound();
            }

            if (!await _roomService.IsRoomNumberFree(viewModel.RoomNumber, id))
            {
                ModelState.AddModelError(nameof(viewModel.RoomNumber), "Number with same Id already exists");
            }

            var froom = new Room
            {
                Capacity = viewModel.Capacity,
                RoomNumber = viewModel.RoomNumber,
                ChildrenPrice = viewModel.ChildrenPrice,
                AdultPrice = viewModel.AdultPrice,
                RoomTypeId = viewModel.RoomTypeId
            };

            // Handle image changes (if the user uploaded new images)
            if (viewModel.RoomImagesFile != null && viewModel.RoomImagesFile.Any())
            {
                await _roomService.Update(id, froom, viewModel.RoomImagesFile);
            }
            else
            {
                // If no new images were uploaded, update the room information only
                await _roomService.Update(id, froom);
            }

            if (imageIds != null && imageIds.Any())
            {
                foreach (var imageId in imageIds)
                {
                    // Use the IRoomImageService to delete the image by its ID
                    await _roomImageService.Remove(imageId);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            // if it null redirect to same page.
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Call the delete service.
            await _roomService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            // Retrieve the room details using the RoomService
            var room = await _roomService.GetId<RoomViewModel>(id);

            if (room == null)
            {
                return NotFound(); // Room not found, handle this case in the view
            }

            return View(room);
        }
    }
}