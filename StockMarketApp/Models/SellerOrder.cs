using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StockMarketApp.Models
{
    public class SellerOrder
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Цена за шт.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime DateTimeSeller { get; set; }
    }
}
