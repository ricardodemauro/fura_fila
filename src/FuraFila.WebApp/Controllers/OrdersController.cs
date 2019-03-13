using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FuraFila.Domain.Models;
using FuraFila.Repository.EF;
using FuraFila.WebApp.Infrastructure.Extensions;

namespace FuraFila.WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Orders.Include(o => o.Seller);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Items/{orderId}
        public async Task<IActionResult> Items(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = order.Id;
            ViewData["TableId"] = order.TableId;
            ViewData["IsActive"] = order.IsActive;
            ViewData["IsPaid"] = order.IsPaid;
            return View(order.Items);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            BuildView();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UnitPrice,Description,TableId,ExternalId,IsPaid,IsActive,SellerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order = order.SetCreated()
                             .SetCreatedBy(User);

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            BuildView(order.SellerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            BuildView(order.SellerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UnitPrice,Description,Paid,Created,CreatedBy,SellerId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order = this.SetCreated(order)
                            .SetCreatedBy(User);

                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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

            BuildView(order.SellerId);
            return View(order);
        }

        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private void BuildView(string sellerId = null)
        {
            if (string.IsNullOrEmpty(sellerId))
            {
                ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name");
            }
            else
            {
                ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", sellerId);
            }
        }
    }
}
