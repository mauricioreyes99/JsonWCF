using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClienteMVCProducto.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [StringLength(30, ErrorMessage = "El campo {0} debe constar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo {0}, no puede estar vacio")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaCompra { get; set; }
    }
}