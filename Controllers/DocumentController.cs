using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageMagick;
using DMS_DUT.Models;
using DMS_V2P.Models;


namespace DMS_DUT.Controllers
{
    public class DocumentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        // GET: Document/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Document/Create
        public ActionResult Create()
        {

            // Fetch all forms. Consider ordering or filtering as necessary.
            ViewBag.FormList = new SelectList(db.Forms.OrderBy(x => x.Name), "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, int formId)
        {
            ViewBag.FormList = new SelectList(db.Forms.OrderBy(x => x.Name), "Id", "Name");  // This is common to all returns, set it once

            if (file == null || file.ContentLength == 0)
            {
                ViewBag.Message = "You have not specified a file.";
                return View();
            }

            try
            {
                string base64Image = ConvertPdfToBase64Image(file);
                if (!string.IsNullOrEmpty(base64Image))
                {
                    var document = new Document
                    {
                        Path = base64Image,
                        FormId = formId,
                        IsActive = true,
                        IsDeleted = false,
                        IsLocked = false,
                    };

                    db.Documents.Add(document);
                    db.SaveChanges();

                    return RedirectToAction("Create");
                }
                else
                {
                    ViewBag.Message = "Failed to convert PDF to image. Please ensure the file is a valid PDF.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Consider using a logging framework to log the exception details
                ViewBag.Message = "ERROR: " + ex.Message;
                return View();
            }
        }

        private string ConvertPdfToBase64Image(HttpPostedFileBase file)
        {
            var settings = new MagickReadSettings
            {
                Density = new Density(300, 300) // Set both dimensions for clarity
            };

            using (var images = new MagickImageCollection())
            {
                images.Read(file.InputStream, settings);
                var image = images.FirstOrDefault();

                if (image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        image.Write(ms, MagickFormat.Jpg);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }

            return null;
        }


        //[HttpPost]
        //public ActionResult Create(HttpPostedFileBase file, int formId) // Assuming you pass formId as part of the form submission
        //{
        //    if (file != null && file.ContentLength > 0)
        //    {

        //        if (file == null)
        //        {
        //            System.Diagnostics.Debug.WriteLine("File is null");
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine($"Received file length: {file.ContentLength}");
        //        }
        //        try
        //        {
        //            string base64Image = ConvertPdfToBase64Image(file);
        //            if (!string.IsNullOrEmpty(base64Image))
        //            {
        //                // Create and configure the Document object
        //                var document = new Document
        //                {

        //                    Path = base64Image,
        //                    FormId = formId,
        //                    IsActive = true,
        //                    IsDeleted = false,
        //                    IsLocked = false,

        //                    // Set other properties as needed
        //                };

        //                // Add the new Document to the database
        //                db.Documents.Add(document);
        //                db.SaveChanges(); // Save changes to the database

        //                ViewBag.FormList = new SelectList(db.Forms.OrderBy(x => x.Name), "Id", "Name");
        //                return RedirectToAction("Create"); // Redirect as appropriate
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = "ERROR:" + ex.Message.ToString();
        //            ViewBag.FormList = new SelectList(db.Forms.OrderBy(x => x.Name), "Id", "Name");
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "You have not specified a file.";
        //        ViewBag.FormList = new SelectList(db.Forms.OrderBy(x => x.Name), "Id", "Name");
        //        return View();
        //    }
        //    ViewBag.FormList = new SelectList(db.Forms.OrderBy(x => x.Name), "Id", "Name");
        //    return View(); // Return the view in case of failure
        //}

        //private string ConvertPdfToBase64Image(HttpPostedFileBase file)
        //{
        //    // Convert PDF stream to image
        //    var settings = new MagickReadSettings();
        //    // Settings for the conversion, high density to improve quality
        //    settings.Density = new Density(300);

        //    using (var images = new MagickImageCollection())
        //    {
        //        // Read the PDF from the input stream
        //        images.Read(file.InputStream, settings);

        //        // Select the first page (or another page if desired)
        //        var image = images.FirstOrDefault();

        //        if (image != null)
        //        {
        //            using (var ms = new MemoryStream())
        //            {
        //                // Save the image to a memory stream
        //                image.Write(ms, MagickFormat.Jpg);
        //                ms.Position = 0;

        //                // Convert the memory stream to a byte array
        //                byte[] imageBytes = ms.ToArray();

        //                // Convert the byte array to a base64 string
        //                string base64Image = Convert.ToBase64String(imageBytes);

        //                // Return the base64 string
        //                return base64Image;
        //            }
        //        }
        //    }

        //    return null;
        //}

        // GET: Document/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Document/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Document/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Document/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
