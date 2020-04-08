using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using chatProject1.Models;
using Microsoft.AspNetCore.Identity;
using chatProject1.Data;
using Microsoft.EntityFrameworkCore;
using chatProject1.Hubs;

namespace chatProject1.Controllers
{
    public class HomeController : Controller
    {
        //ChatHub chat = new ChatHub();
        public ApplicationDbContext _dbContext;
        public UserManager<IdentityUser> _userManager;//InvalidOperationException: Unable to resolve service for type 'Microsoft.AspNetCore.Identity.UserManager`1[chatProject1.Models.AppUser]' while attempting to activate 'chatProject1.Controllers.HomeController'.
        public HomeController(ApplicationDbContext DbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = DbContext;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                ViewBag.friends = _dbContext.Friends.Where(f => f.userid == currentUser.Id).Include(f => f.user).ToList();

            }
            //ViewBag.currentUserName = currentUser.UserName;
            //var message = await _dbContext.Messages.ToListAsync();
            return View();
        }
        //public async Task<IActionResult>Create(Message message)
        //{
        //    if (ModelState.IsValid) {
        //        message.userName = User.Identity.Name;
        //        var sender = await _userManager.GetUserAsync(User);
        //        message.userID = sender.Id;
        //        await _dbContext.Messages.AddAsync(message);
        //        await _dbContext.SaveChangesAsync();
        //        return Ok();
        //    }
        //    return Error();
        //}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
        public ActionResult chat()
        {
            return View();

        }
        [HttpPost]
        public async Task setState(string name, bool state)
        {
            var user = await _dbContext.AppUsers.Where(u => u.UserName == name).FirstOrDefaultAsync();
            var friend = await _dbContext.Friends.Where(f => f.friendID == user.Id).FirstOrDefaultAsync();
            friend.active = state;
            _dbContext.SaveChanges();

        }
        public async Task<JsonResult> GetCurrentUser()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            return Json(_dbContext.AppUsers.Where(u => u.Id == currentUser.Id).SingleOrDefault());

        }


        public void SaveMessage(string message, string recieverName, DateTime when, string recieverID, string senderID)
        {
            _dbContext.Messages.Add(new Message { userName = recieverName, text = message, userID = recieverID, when = when, senderID = senderID });
            _dbContext.SaveChanges();
        }


        public List<Message> RetrieveAllMessage(string currentUser, string friendName, string currentID, string frienID)
        {
            var currentMessage = _dbContext.Messages.Where(m => m.senderID == currentID && m.userID == frienID || m.senderID == frienID && m.userID == currentID).ToList();
            var allMessages = _dbContext.Messages.Where(m => m.senderID == frienID && m.userID == currentID).Concat(currentMessage).OrderBy(m=>m.when).ToList();
            return currentMessage;
        }
    }


}
