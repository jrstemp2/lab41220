using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SlackOverload.Models;

namespace SlackOverload.Controllers
{
    public class QandAController : Controller
    {
        private DAL dal;

        public QandAController(IConfiguration config)
        {
            dal = new DAL(config.GetConnectionString("default"));
        }

        public IActionResult Index()
        {
            //get the most recent questions
            IEnumerable<Question> results = dal.GetQuestionsMostRecent();

            ViewData["Questions"] = results;

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Question());
        }

        [HttpPost]
        public IActionResult Add(Question q)
        {
            int result = dal.CreateQuestion(q);


            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id) {
            //first get the question detail
            Question question = dal.GetQuestionById(id);

            //then get the relevant answers
            IEnumerable<Answer> answers = dal.GetAnswersByQuestionId(id);

            ViewData["Answers"] = answers;

            return View(question);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Question q = dal.GetQuestionById(id);
            return View(q);
        }

        [HttpPost]
        public IActionResult Edit(Question q)
        {
            int result = dal.UpdateQuestionById(q);
            return RedirectToAction("Index");
        }

        

    }
}