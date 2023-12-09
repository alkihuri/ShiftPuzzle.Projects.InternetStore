using System.ComponentModel.DataAnnotations;

namespace MyStore.Models
{
    [System.Serializable]
    public class Product
    {
        [Key] public int Id { get; set; } // Первичный ключ

        [Required] public string Name { get; set; } // Дополнительное свойство

        public string Description{ get; set; }
 
     
        public int Price{ get; set; }
 
    }
}
