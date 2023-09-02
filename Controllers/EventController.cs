using Microsoft.AspNetCore.Mvc;

namespace LamConference.Controllers{
    public class EventController : Controller{

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult OurSpeakers()
        {
            return View();
        }
        public IActionResult WhatToExpect()
        {
            return View();
        }
        public IActionResult HowTo()
        {
            return View();
        }
    }
}