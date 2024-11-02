using DatabaseFirst_EF_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DatabaseFirst_EF_Core.Controllers
{
    public class HomeController : Controller
    {

        private readonly MyDbContext _context;


        public HomeController(MyDbContext context)
        {
            _context = context;
        }

        // C - CREATE
        // when i click on create then this method call first  
        public IActionResult Create()
        {
            return View();
        }

        // when i click post this method call 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student obj_Student)
        {

            if (ModelState.IsValid)
            {
                await _context.Students.AddAsync(obj_Student);
                await _context.SaveChangesAsync();
                //Data Inserted Msg
                TempData["insert_success"] = "Inserted....";
                return RedirectToAction("Index", "Students");
            }
            return View(obj_Student);
        }

        // R - READ
        public async Task<IActionResult> Index()
        {
            var stud_data = await _context.Students.ToListAsync(); //this Students is not table name this is instance of student dataset
            return View(stud_data);
        }

        // U - UPDATE

        public async Task<IActionResult> Edit(int? id)
        {
            //var stud_data = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var stud_data = await _context.Students.FindAsync(id);

            return View(stud_data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student obj_Student)
        {
            if (id != obj_Student.Id)
            {
                return NotFound();

            }

            if (ModelState.IsValid)
            {
                // add to studentdbcontext
                _context.Students.Update(obj_Student);
                await _context.SaveChangesAsync();
                TempData["update_success"] = "Updated....";

                return RedirectToAction("Index", "Home");

            }
            return View(obj_Student);
        }




        // D - DELETE 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var stud_data = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (stud_data == null)
            {
                return NotFound();
            }

            return View(stud_data);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_record(int? id)
        {
            var studData = await _context.Students.FindAsync(id);

            if (studData != null)
            {
                _context.Students.Remove(studData);
            }
            await _context.SaveChangesAsync();
            TempData["Delete_success"] = "Deleted....";
            return RedirectToAction("Index", "Students");

        }




        // D - DETAIL
        public async Task<IActionResult> Details(int? id) // if id is null // we are not post any data so there is no HTTPOST Method
        {
            var stud_data = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            if (stud_data == null)
            {
                return NotFound();
            }

            return View(stud_data);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
