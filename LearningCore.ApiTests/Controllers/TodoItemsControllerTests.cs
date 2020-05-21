using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearningCore.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using LearningCore.Service;
using System.Linq;

namespace LearningCore.Api.Controllers.Tests
{
    [TestClass()]
    public class TodoItemsControllerTests
    {
        [TestMethod()]
        public void GetTodoItemsTest()
        {
            using (var _context = new LearningCoreContext())
            {
                var controll = new TodoItemsController(_context);

                var result = controll.GetTodoItems();

                Assert.IsTrue(result.Result.Value.Count() > 0);
            }
        }
    }
}