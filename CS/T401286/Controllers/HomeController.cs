using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DevExpress.Web.Mvc;

namespace T401286.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView

            return View();    
        }
        
        public ActionResult GridViewPartialView() 
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", Company.OfficeRooms);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BatchUpdateRoomsPartial(MVCxGridViewBatchUpdateValues<Room, int> batchValues)
        {
            foreach (var item in batchValues.Insert)
            {
                if (batchValues.IsValid(item))
                    Company.InsertRoom(item);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var item in batchValues.Update)
            {
                if (batchValues.IsValid(item))
                    Company.UpdateRoom(item);
                else
                    batchValues.SetErrorText(item, "Correct validation errors");
            }
            foreach (var itemKey in batchValues.DeleteKeys)
            {
                Company.RemoveRoomByID(itemKey);
            }
            return PartialView("GridViewPartialView", Company.OfficeRooms);
        }
    
    }
}