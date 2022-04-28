using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var result = _context.Invoices.ToList();
            return View(result);
        }

        // GET: InvoiceController/Details/5
        public ActionResult GetInvoice(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .FirstOrDefault(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }



         //get invoices by customer name
        public ActionResult GetInvoicesByCustomerName(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .FirstOrDefault(m => m.CustomerName == name);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }



        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Date,CustomerName,IsBuy,AmountPaied")] Invoice invoice)
        {
            _context.Add(invoice);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));


            return View(invoice);

        }


        // GET: InvoiceController/
        public ActionResult Edit(int id)
        {
            return View();
        }


        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Date,CustomerName,IsBuy,AmountPaied")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(invoice);
        }


            // GET: InvoiceController/Delete/5
            public ActionResult Delete(int id)      //get
            {
                return View();
            }



            // POST: InvoiceController/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteInvoice(int id)    //post
            {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var invoice = _context.Invoices.Find(id);
                _context.Invoices.Remove(invoice);
                _context.SaveChanges();
                
                return RedirectToAction(nameof(Index));

            }
            
               }
        
            
          } 
        }
    
