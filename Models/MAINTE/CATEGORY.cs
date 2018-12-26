using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNetCore.Models.MAINTE
{
    public class CATEGORY
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
       
    }
}