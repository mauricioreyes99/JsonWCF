using ClienteMVCProducto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ClienteMVCProducto.Controllers
{
    public class ProductsController : Controller
    {
        private string UrlBase = "http://localhost:50135/Service1.svc/";
        // GET: Products
        public ActionResult Index()
        {
            var producto = new WebClient();
            var cadena = producto.DownloadString(UrlBase + "mostrar");
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<List<Product>>(cadena);
            return View(datos.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product Pro)
        {
            try
            {
                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(Product));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, Pro);
                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente = new WebClient();
                cliente.Headers["Content-type"] = "application/json";
                cliente.Encoding = Encoding.UTF8;
                cliente.UploadString(UrlBase + "Crear", "POST", data);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(Pro);
            }
            
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var producto = new WebClient();
            var cadena = producto.DownloadString(UrlBase + "mostrar");
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<List<Product>>(cadena);
            var busqueda = (from x in datos
                            where x.ProductID == id
                            select x).FirstOrDefault();
            return View(busqueda);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(Product Pro)
        {
            try
            {
                // TODO: Add update logic here
                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(Product));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, Pro);
                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente = new WebClient();
                cliente.Headers["Content-type"] = "application/json";
                cliente.Encoding = Encoding.UTF8;
                cliente.UploadString(UrlBase + "Editar", "PUT", data);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(Pro);
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var producto = new WebClient();
            var cadena = producto.DownloadString(UrlBase + "mostrar");
            var JS = new JavaScriptSerializer();
            var datos = JS.Deserialize<List<Product>>(cadena);
            var busqueda = (from x in datos
                            where x.ProductID == id
                            select x).First();
            return View(busqueda);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(Product Pro, int id)
        {

            try
            {
                var producto = new WebClient();
                var cadena = producto.DownloadString(UrlBase + "mostrar");
                var JS = new JavaScriptSerializer();
                var datos = JS.Deserialize<List<Product>>(cadena);
                var busqueda = (from x in datos
                                where x.ProductID == id
                                select x).First();

                // TODO: Add delete logic here

                DataContractJsonSerializer DCJS = new DataContractJsonSerializer(typeof(Product));
                MemoryStream m = new MemoryStream();
                DCJS.WriteObject(m, busqueda);
                string data = Encoding.UTF8.GetString(m.ToArray(), 0, (int)m.Length);
                WebClient cliente = new WebClient();
                cliente.Headers["Content-type"] = "application/json";
                cliente.Encoding = Encoding.UTF8;
                cliente.UploadString(UrlBase + "Eliminar", "DELETE", data);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(Pro);
            }
        }
    }
}
