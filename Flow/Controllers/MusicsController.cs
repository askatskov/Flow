using Flow.Data;
using Flow.Domain;
using Flow.Dto;
using Flow.Models.Music;
using Flow.Models.Musics;
using Flow.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Flow.Controllers
{
	public class MusicsController : Controller
	{

		private readonly MusicContext _context;
		private readonly IMusicServices _musicServices;
		private readonly IFileServices _fileServices;

		public MusicsController(MusicContext context, IMusicServices musicServices, IFileServices fileServices)
		{
			_context = context;
			_musicServices = musicServices;
			_fileServices = fileServices;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var resultingInventory = _context.Musics
                .OrderByDescending(y => y.Rating)
				.Select(x => new MusicIndexViewModel
				{
					ArtistId = x.ArtistId,
					Artist = x.Artist,
					Song = x.Song,
					Rating = x.Rating,
				});
			return View(resultingInventory);
		}
		[HttpGet]
		public IActionResult Create()
		{
			MusicCreateViewModel vm = new();
			return View("Create", vm);
		}
		[HttpPost, ActionName("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(MusicCreateViewModel vm)
		{
			var dto = new MusicDto
			{
				Artist = vm.Artist,
				Song = vm.Song,
				Rating = vm.Rating,
				Files = vm.Files,
				Image = vm.Image.Select(x => new FileToDatabaseDto
				{
					Id = x.ImageID,
					ImageData = x.ImageData,
					ImageTitle = x.ImageTitle,
					ArtistId = x.ArtistId,
				}).ToArray()
			};
			var result = await _musicServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index", vm);
		}
		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			//if (id == null)
			//{
			// return NotFound();
			//}

			var music = await _musicServices.DetailsAsync(id);

			if (music == null)
			{
				return NotFound();
			}


			var images = await _context.FileToDatabase
				.Where(c => c.ArtistId == id)
				.Select(y => new MusicImageViewModel
				{
					ArtistId = y.Id,
					ImageID = y.Id,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

			var vm = new MusicDetailsViewModel();
			vm.ArtistId = music.ArtistId;
			vm.Artist = music.Artist;
			vm.Song = music.Song;
			vm.Rating = music.Rating;
			vm.Image.AddRange(images);


			return View(vm);
		}
		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			if (id == null) { return NotFound(); }

			var music = await _musicServices.DetailsAsync(id);

			if (id == null) { return NotFound(); }

			var images = await _context.FileToDatabase
				.Where(x => x.ArtistId == id)
				.Select(y => new MusicImageViewModel
				{
					ArtistId = y.ArtistId,
					ImageID = y.Id,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

			var vm = new MusicCreateViewModel();
			vm.Artist = music.Artist;
			vm.Song = music.Song;
			vm.Rating = music.Rating;

			return View("Update", vm);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			if (id == null) { return NotFound(); }

			var music = await _musicServices.DetailsAsync(id);

			if (id == null) { return NotFound(); }
			;

			var images = await _context.FileToDatabase
				.Where(x => x.ArtistId == id)
				.Select(y => new MusicImageViewModel
				{
					ArtistId = y.Id,
					ImageID = y.Id,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();
			var vm = new MusicDeleteViewModel();

			vm.ArtistId = music.ArtistId;
			vm.Artist = music.Artist;
			vm.Song = music.Song;
			vm.Rating = music.Rating;
			vm.Image.AddRange(images);

			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var ToDelete = await _musicServices.Delete(id);

			if (ToDelete == null) { return RedirectToAction("Index"); }

			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> RemoveImage(MusicImageViewModel vm)
		{
			var dto = new FileToDatabase()
			{
				Id = vm.ImageID
			};
			var image = await _fileServices.RemoveImageFromDatabase(dto);
			if (image == null) { return RedirectToAction("Index"); }
			return RedirectToAction("Index");
		}
	}
}