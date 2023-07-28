﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;
using WebApplication2.Models.Room;
using WebApplication2.Models.RoomType;
using WebApplication2.services;
using WebApplication2.services.Common;

namespace WebApplication2.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;


        public RoomsController(ApplicationDBContext context, IRoomService roomService, IRoomTypeService roomTypeService)
        {
            _context = context;
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            

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

            var model = new RoomInputModel
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
        public async Task<IActionResult> Create(RoomInputModel model)
        {
            if (ModelState.IsValid)
            {
                var room = new Room
                {
                    Capacity = model.Capacity,
                    IsTaken = model.IsTaken,
                    AdultPrice = model.AdultPrice,
                    ChildrenPrice = model.ChildrenPrice,
                    RoomNumber = model.RoomNumber,
                    RoomTypeId = model.RoomTypeId
                };

                await _roomService.Add(room,model.RoomImages);

                // Redirect to the room list or any other desired page after successful addition
                return RedirectToAction("Index");
            }

            // If there are any validation errors, display the create view again with the model
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the room and its images from the database
            var room = await _context.Rooms.Include(r => r.RoomImages).FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }

            // Map the room to the view model and pass it to the edit view
            var viewModel = new RoomInputModel
            {
                Capacity = room.Capacity,
                IsTaken = room.IsTaken,
                AdultPrice = room.AdultPrice,
                ChildrenPrice = room.ChildrenPrice,
                RoomNumber = room.RoomNumber,
                RoomTypeId = room.RoomTypeId,
                /*RoomImages = room.RoomImages*/ // Assign the existing images to the view model
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
        public async Task<IActionResult> Edit(int id, RoomInputModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the room from the database
                var room = await _context.Rooms.Include(r => r.RoomImages).FirstOrDefaultAsync(r => r.RoomId == id);

                if (room == null)
                {
                    return NotFound();
                }

                // Update the room information with the data from the view model
                room.Capacity = viewModel.Capacity;
                room.IsTaken = viewModel.IsTaken;
                room.AdultPrice = viewModel.AdultPrice;
                room.ChildrenPrice = viewModel.ChildrenPrice;
                room.RoomNumber = viewModel.RoomNumber;

                // Handle image changes (if the user uploaded new images)
                if (viewModel.RoomImages != null && viewModel.RoomImages.Any())
                {
                    await _roomService.Update(room, viewModel.RoomImages);
                }
                else
                {
                    // If no new images were uploaded, update the room information only
                    _context.Rooms.Update(room);
                }

                await _context.SaveChangesAsync();

                // Redirect to the room list or any other desired page after successful update
                return RedirectToAction("Index");
            }

            // If there are any validation errors, display the edit view again with the model
            return View(viewModel);
        }

    }
}