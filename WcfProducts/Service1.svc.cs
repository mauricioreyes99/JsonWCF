using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfProducts.Models;

namespace WcfProducts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public bool Crear(CProductos producto)
        {
            bool creado = true;

            Models.Producto data = new Models.Producto();
            using (MarketPEntities db = new MarketPEntities())
            {
                data.ProductID = producto.ProductID;
                data.Nombre = producto.Nombre;
                data.Cantidad = producto.Cantidad;
                data.Precio = producto.Precio;
                data.FechaCompra = producto.FechaCompra;
                try
                {
                    db.Productos.Add(data);
                    db.SaveChanges();

                }
                catch (Exception)
                {

                    creado = false;
                }
               
            }

            return creado;
        }

        public bool Editar(CProductos producto)
        {
           
            try
            {
                using (MarketPEntities db = new MarketPEntities())
                {
                    Producto data = new Producto();

                    data.ProductID = producto.ProductID;
                    data.Nombre = producto.Nombre;
                    data.Cantidad = producto.Cantidad;
                    data.Precio = producto.Precio;
                    data.FechaCompra = producto.FechaCompra;

                    db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Eliminar(CProductos producto)
        {
            try
            {
                using (MarketPEntities db = new MarketPEntities())
                {
                    int id = producto.ProductID;
                    var busqueda = db.Productos.Find(id);
                    db.Productos.Remove(busqueda);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CProductos Filtrar(string filtro)
        {
            using (MarketPEntities db = new MarketPEntities())
            {
               
                var buscarxN = (from s in db.Productos
                               where s.Nombre.Contains(filtro)
                               select new CProductos
                               {
                                  ProductID = s.ProductID,
                                  Nombre= s.Nombre,
                                  Cantidad= s.Cantidad,
                                  Precio= s.Precio,
                                  FechaCompra= s.FechaCompra
                               }).FirstOrDefault();
                               
                return buscarxN;
            }
        }

        public List<CProductos> Mostrar()
        {
            using (MarketPEntities db = new MarketPEntities())
            {
                var lista = from p in db.Productos
                            select new CProductos
                            {
                                ProductID = p.ProductID,
                                Nombre = p.Nombre,
                                Cantidad = p.Cantidad,
                                FechaCompra = p.FechaCompra,
                                Precio = p.Precio
                            };
                return lista.ToList();
            }
        }

        public CProductos Ver(string id)
        {
            int ID = Convert.ToInt32(id);
            using (MarketPEntities db = new MarketPEntities())
            {
                var buscarxN = (from s in db.Productos
                                where s.ProductID == ID
                                select new CProductos
                                {
                                    ProductID = s.ProductID,
                                    Nombre = s.Nombre,
                                    Cantidad = s.Cantidad,
                                    Precio = s.Precio,
                                    FechaCompra = s.FechaCompra
                                }).FirstOrDefault();

                return buscarxN;
            }
        }
    }
}
