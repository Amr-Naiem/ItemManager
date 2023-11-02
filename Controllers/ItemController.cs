using ItemManager.Data;
using ItemManager.Models;
using ItemManager.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ItemManager.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemManagerContext itemManagerDbContext;

        public ItemController(ItemManagerContext itemManagerDbContext)
        {
            this.itemManagerDbContext = itemManagerDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await itemManagerDbContext.Items.ToListAsync();
            return View(items);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddItemViewModel item)
        {
            var newItem = new Item()
            {
                Serial = Guid.NewGuid(),
                CustomerName = item.CustomerName,
                InvoiceDate = item.InvoiceDate,
                ItemCode = item.ItemCode,
                ItemName = item.ItemName,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                DiscountRate = item.DiscountRate,
                TaxType = item.TaxType,
                TaxRate = item.TaxRate,
                // SubTotal, DiscountValue, TaxValue, and Total will be calculated automatically
            };

            newItem.UpdateCalculatedValues();

            await itemManagerDbContext.Items.AddAsync(newItem);
            await itemManagerDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid serial)
        {
            var item = await itemManagerDbContext.Items.FirstOrDefaultAsync(x => x.Serial == serial);

            if (item != null)
            {
                var viewModel = new UpdateItemViewModel()
                {
                    Serial = item.Serial,
                    CustomerName = item.CustomerName,
                    InvoiceDate = item.InvoiceDate,
                    ItemCode = item.ItemCode,
                    ItemName = item.ItemName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    DiscountRate = item.DiscountRate,
                    TaxType = item.TaxType,
                    TaxRate = item.TaxRate,
                    // SubTotal, DiscountValue, TaxValue, and Total will be calculated automatically
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateItemViewModel model)
        {
            var item = await itemManagerDbContext.Items.FindAsync(model.Serial);

            if (item != null)
            {
                item.CustomerName = model.CustomerName;
                item.InvoiceDate = model.InvoiceDate;
                item.ItemCode = model.ItemCode;
                item.ItemName = model.ItemName;
                item.UnitPrice = model.UnitPrice;
                item.Quantity = model.Quantity;
                item.DiscountRate = model.DiscountRate;
                item.TaxType = model.TaxType;
                item.TaxRate = model.TaxRate;
                // Manually calculate values
                item.UpdateCalculatedValues();

                await itemManagerDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> Delete(Guid Serial)
        {
            var item = await itemManagerDbContext.Items.FindAsync(Serial);

            if (item != null)
            {
                itemManagerDbContext.Items.Remove(item);
                await itemManagerDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
