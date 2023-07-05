using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using ShoeProject.Domain.Contracts.Commands;
using ShoeProject.Domain.Contracts.Queries;
using ShoeProject.Domain.DTO;
using ShoeTracker.Core;
using ShoeTracker.Models;


namespace ShoeTracker.Controllers
{
    /// <summary>
    /// the home contorller
    /// </summary>
    [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]    
    public class HomeController : Controller
    {
        /// <summary>
        /// the query dispacther
        /// </summary>
        [Inject]
        private IQueryDispatcher dispatcher;
        //new change
        /// <summary>
        /// The command dispatcher
        /// </summary>
        private ICommandDispatcher commandDispatcher;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="commandDispatcher"></param>
        public HomeController(IQueryDispatcher dispatcher, ICommandDispatcher commandDispatcher)
        {
            this.dispatcher = dispatcher;
            this.commandDispatcher = commandDispatcher;
        }

        /// <summary>
        /// Index method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public ActionResult Index(bool? wasRedirected)
        {
            HomePageModel model = new HomePageModel();
            GetNamesQuery query = new GetNamesQuery();
            model.Names = dispatcher.Dispatch<GetNamesQuery, List<Names>>(query);
            model.NameList = model.Names.Select(item => new SelectListItem
            {
                Text = item.Name
            });

            GetAllShoeTypesQuery shoeTypesQuery = new GetAllShoeTypesQuery();
            model.ShoeTypes = dispatcher.Dispatch<GetAllShoeTypesQuery, List<ShoeTypes>>(shoeTypesQuery);
            model.ShoeTypeList = model.ShoeTypes.Select(item => new SelectListItem
            {
                Text = item.Description,
                Value = item.ShoeTypeId.ToString()
            });

            if (wasRedirected.HasValue)
            {
                model.isDelete = wasRedirected;
            }

            model.isEdit = false;

            return View(model);
        }

        /// <summary>
        /// Adds the shoe
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "*")]
        public JsonResult AddShoe(ShoeTrackerDto model)
        {   
            if (model.Date == DateTime.MinValue)
            {
                model.Date = DateTime.Now;
            }
            
            AddShoeCommand command = new AddShoeCommand(model);
            int id = commandDispatcher.Dispatch(command);
            
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFile(int id)
        {
            string path = "";
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //path = AppDomain.CurrentDomain.BaseDirectory + "Uploads";
                        //Directory.CreateDirectory(path);
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname = file.FileName;
                        

                        // Get the complete folder path and store the file inside it.  
                        path = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(path);
                    }
                   int num =  commandDispatcher.Dispatch(new AddReceiptPathCommand(id, path));
                    // Returns message that successfully uploaded  
                    return Json("Your information has been saved. Click on the Shoe Purchase History tab to see results");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    
        /// <summary>
        /// Gets all the shoes
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 1, VaryByParam = "*")]
        public PartialViewResult ShoeTrackerHistory(HomePageModel model)
        {
            GetAllShoesQuery query = new GetAllShoesQuery();
            model.ShoeTrackerDtoList = dispatcher.Dispatch<GetAllShoesQuery, List<ShoeTrackerDto>>(query);
            model.Names = dispatcher.Dispatch<GetNamesQuery, List<Names>>(new GetNamesQuery());
            model.Names.Insert(0, new Names{Name = "All Shoes"});
            model.NameList = model.Names.Select(item => new SelectListItem
            {
                Text = item.Name
            });

            return PartialView("AllShoes", model);
        }

        /// <summary>
        /// Deletes a shoe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            commandDispatcher.Dispatch(new DeleteShoeCommand(id));
            bool wasRedirect = true;
            return Json(wasRedirect, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edits a shoe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEdit"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            HomePageModel model = new HomePageModel();
            model.ShoeTrackerDto =
                dispatcher.Dispatch<GetShoeByShoeIdQuery, ShoeTrackerDto>(new GetShoeByShoeIdQuery(id));
            GetNamesQuery query = new GetNamesQuery();
            List<Names>  nameList = dispatcher.Dispatch<GetNamesQuery, List<Names>>(query);
            model.Names = nameList.OrderByDescending(x => x.Name == model.ShoeTrackerDto.ChildName).ToList();
            model.NameList = model.Names.Select(item => new SelectListItem
            {
                Text = item.Name
            });

            GetAllShoeTypesQuery shoeTypesQuery = new GetAllShoeTypesQuery();
            List<ShoeTypes> shoeTypeList = dispatcher.Dispatch<GetAllShoeTypesQuery, List<ShoeTypes>>(shoeTypesQuery);
            model.ShoeTypes = shoeTypeList.OrderByDescending(x => x.ShoeTypeId == model.ShoeTrackerDto.ShoeTypeId).ToList();
            model.ShoeTypeList = model.ShoeTypes.Select(item => new SelectListItem
            {
                Text = item.Description,
                Value = item.ShoeTypeId.ToString()
            });

            model.isEdit = true;
            model.Id = id;
            return View("Index", model);
        }

        /// <summary>
        /// Update shoe
        /// </summary>
        /// <param name="shoeTrackerDto"></param>
        /// <returns></returns>
        public JsonResult UpdateShoe(ShoeTrackerDto shoeTrackerDto)
        {
            int id = commandDispatcher.Dispatch(new EditShoeCommand(shoeTrackerDto));
            string message = "Your infomration has been updated";
            return Json(id, message, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetReceipt
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 1, VaryByParam = "*")]
        public ActionResult GetReceipt(int id)
        {
            HomePageModel model = new HomePageModel();
            model.Path = Path.GetFileName(dispatcher.Dispatch<GetReceiptPathQuery, List<string>>(new GetReceiptPathQuery(id)).FirstOrDefault());
            return PartialView(model);
        }

        [OutputCache(Duration = 1, VaryByParam = "*")]
        public PartialViewResult GetMonthlyBudget()
        {
            HomePageModel model =  new HomePageModel();
            GetMonthlyBudgetQuery query = new GetMonthlyBudgetQuery();
            model.MonthlyBudget = dispatcher.Dispatch<GetMonthlyBudgetQuery, MonthlyBudget>(query);
            model.MonthlyBudget.FoodPercentage = Convert.ToInt32(Math.Round(model.MonthlyBudget.FoodDollarAmount/550 * 100, 2));
            model.MonthlyBudget.ShoppingPercentage = Convert.ToInt32(Math.Round(model.MonthlyBudget.ShoppingDollarAmount /200 * 100, 2));
            model.MonthlyBudget.GasPercentage = Convert.ToInt32(Math.Round(model.MonthlyBudget.GasDollarAmount/160 * 100, 2));
            return PartialView(model);
        }

        public ActionResult InsertBudgetValue(string category, int amount)
        {
            int success = commandDispatcher.Dispatch(new AddMonthlyBudgetCommand(category, amount));
            return RedirectToAction("Index");
        }
    }
}
