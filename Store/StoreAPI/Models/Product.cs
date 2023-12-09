using System.ComponentModel.DataAnnotations;

namespace MyStore.Models
{
    [System.Serializable]
    public class Product
    {
        public Product()
        {
            Id =1 ;
            Name  = "Empty";
        }
        public int Id { get; set; } // Первичный ключ

         public string Name { get; set; } // Дополнительное свойство

        public string Description{ get; set; }
 
     
        public int Price{ get; set; }
 
    }
}
