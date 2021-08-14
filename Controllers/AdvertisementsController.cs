using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2NET.Data;
using Assignment2NET.Models;
using Assignment2NET.Models.ViewModels;

namespace Assignment2NET.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public AdvertisementsController(SchoolCommunityContext context)
        {
            _context = context;
        }

        // GET: Advertisements
        public async Task<IActionResult> Index(string ID)
        {
            var viewModel = new AdsViewModel();
            viewModel.Community = await _context.Communities.FindAsync(ID);
            viewModel.Advertisements = await _context.Advertisements
                                .Include(i => i.Community)
                                .Where(c => c.CommunityID == ID)
                                .OrderBy(c => c.AdvertisementId)
                                .AsNoTracking()
                                .ToListAsync();

            return View(viewModel);
        }


        // GET: Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.Community)
                .FirstOrDefaultAsync(m => m.AdvertisementId == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.AdvertisementId == id);
        }
    }
}
