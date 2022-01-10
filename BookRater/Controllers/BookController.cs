namespace BookRater.Controllers
{
    using System.Threading.Tasks;
    using Services;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class BookController : Controller
    {
        private const string UserId = "1";
        private readonly ICosmosDbService cosmosDbService;

        public BookController(ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Title,Author")] Book book)
        {
            if (!ModelState.IsValid) return View(book);
            await cosmosDbService.AddItemAsync(book);
            return RedirectToAction("Index");

        }

        [HttpPost]
        [ActionName("Rate")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RateAsync(string bookId, int rate)
        {
            var book = await cosmosDbService.GetItemAsync(bookId);
            book.SaveOrUpdateRate(UserId, rate);
            await cosmosDbService.UpdateItemAsync(bookId, book);

            TempData["RateMessage"] = "Your rate was submitted";
            return RedirectToAction("Details", new { id = bookId });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Title,Author")] Book book)
        {
            if (ModelState.IsValid)
            {
                await cosmosDbService.UpdateItemAsync(book.Id, book);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Book item = await cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Book book = await cosmosDbService.GetItemAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await cosmosDbService.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var item = await cosmosDbService.GetItemAsync(id);
            var book = new BookWithRates(item, UserId);

            return View(book);
        }
    }
}