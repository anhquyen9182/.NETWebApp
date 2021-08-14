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
    public class StudentsController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public StudentsController(SchoolCommunityContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(int? id)
        {
            var studentviewModel = new StudentViewModel();
            studentviewModel.Students = await _context.Students
                .Include(s => s.CommunityMemberships)
                .ThenInclude(s => s.Community)
                .OrderBy(s => s.ID)
                .ToListAsync();
            if (id != null)
            {
                studentviewModel.CommunityMemberships = studentviewModel.Students
                    .Where(a => a.ID == id).Single().CommunityMemberships
                    .Where(a => a.StudentID == id);
            }
            return View(studentviewModel);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        //GET: Students/EditMembership
        public async Task<IActionResult> EditMemberships(int? id)
        {
            var studentviewModel = new StudentMembershipViewModel();
            studentviewModel.Student = 
                _context.Students
                .Where(s => s.ID == id)
                .Single();

            var communityMemberships =
                from community in _context.Communities
                select new CommunityMembershipViewModel()
                {
                    CommunityId = community.ID,
                    Title = community.Title,
                    IsMember = _context.Students
                        .Where(s => s.ID == id)
                        .Single()
                        .CommunityMemberships.Any(s => s.CommunityID == community.ID)
                };
            studentviewModel.Memberships = await communityMemberships.AsNoTracking().ToListAsync();
            return View(studentviewModel);
        }

        //GET: Students/RemoveMembership
        public async Task<IActionResult> RemoveMembership(int? id, string communityID)
        {
            _context.CommunityMemberships.Remove(_context.CommunityMemberships
                                   .Where(s => s.StudentID == id)
                                   .Where(c => c.CommunityID == communityID)
                                   .Single()
                               );
            await _context.SaveChangesAsync();
            var finalMember = nameof(EditMemberships);
            var finalComID = new { id = id };
            return RedirectToAction(finalMember, finalComID);
        }

        //GET: Students/AddMembership
        public async Task<IActionResult> Addmembership(int? id, string communityId)
        {
            var studentviewModel = new StudentMembershipViewModel();
            studentviewModel.Student = _context.Students
                .Where(s => s.ID == id)
                .Single();
            _context.CommunityMemberships.Add(new CommunityMembership()
            {
                Student = studentviewModel.Student,
                StudentID = studentviewModel.Student.ID,
                CommunityID = communityId,
                Community = _context.Communities.Where(c => c.ID == communityId).Single()
            });
            await _context.SaveChangesAsync();
            var finalMember = nameof(EditMemberships);
            var finalComID = new { id = id };
            return RedirectToAction(finalMember, finalComID);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
