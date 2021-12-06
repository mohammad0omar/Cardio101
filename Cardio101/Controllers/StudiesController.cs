using Cardio101.Data;
using Cardio101.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cardio101.Controllers
{
    public class StudiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Studies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Study.Include(s => s.Device).Include(s => s.Patient);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Studies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Study
                .Include(s => s.Device)
                .Include(s => s.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // GET: Studies/Create
        public IActionResult Create()
        {
            ViewData["DeviceId"] = new SelectList(_context.Device, "Id", "SerialNumber");
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "Name");
            return View();
        }

        // POST: Studies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,Duration,PatientId,DeviceId,NormalHeartRate,LowHeartRate,HighHeartRate")] Study study)
        {
            if (ModelState.IsValid)
            {
                _context.Add(study);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceId"] = new SelectList(_context.Device, "Id", "SerialNumber", study.DeviceId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "Name", study.PatientId);
            return View(study);
        }

        // GET: Studies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Study.FindAsync(id);
            if (study == null)
            {
                return NotFound();
            }
            ViewData["DeviceId"] = new SelectList(_context.Device, "Id", "SerialNumber", study.DeviceId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "Name", study.PatientId);
            return View(study);
        }

        // POST: Studies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,Duration,PatientId,DeviceId,NormalHeartRate,LowHeartRate,HighHeartRate")] Study study)
        {
            if (id != study.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(study);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyExists(study.Id))
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
            ViewData["DeviceId"] = new SelectList(_context.Device, "Id", "SerialNumber", study.DeviceId);
            ViewData["PatientId"] = new SelectList(_context.Patient, "Id", "Name", study.PatientId);
            return View(study);
        }

        // GET: Studies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Study
                .Include(s => s.Device)
                .Include(s => s.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // POST: Studies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var study = await _context.Study.FindAsync(id);
            _context.Study.Remove(study);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyExists(int id)
        {
            return _context.Study.Any(e => e.Id == id);
        }
    }
}
