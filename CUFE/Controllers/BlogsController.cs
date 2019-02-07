using CUFE.Models;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUFE.Controllers
{
    public class BlogsController : BaseXpoController
    {
        
        // GET: Blogs
        public ActionResult GridViewPartial()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Blog>();
                return PartialView("_GridViewPartial", model.ToList());
            }
        }
        public ActionResult Add([ModelBinder(typeof(XpoModelBinder))]Blog item)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Blog>();
                if (ModelState.IsValid)
                {

                    //item.Session.CommitTransaction();
                    var country = new Blog(uow)
                    {
                        ShortDescription = item.ShortDescription,
                        Article = item.Article,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = DateTime.Now
                    };

                    uow.CommitChanges();
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";
                return PartialView("_GridViewPartial", model.ToList());
            }
        }

        public ActionResult Update([ModelBinder(typeof(XpoModelBinder))]Blog item)
        {
            
            using (UnitOfWork uow = new UnitOfWork())
            {
                var model = uow.Query<Blog>();
                if (item.IsChanged)
                    item.Save();
                
                return PartialView("_GridViewPartial", model.ToList());
            }

        }
        public ActionResult Delete(System.Int32 Oid)
        {
            using (var uow = XpoHelper.GetNewUnitOfWork())
            {
                var model = uow.Query<Blog>();
                var item = model.First(m => m.Oid == Oid);
                uow.Delete(item);
                uow.CommitChanges();
                return PartialView("_GridViewPartial", model.ToList());
            }

        }

    }
}