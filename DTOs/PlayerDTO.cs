
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
namespace DTOs
{
    [DataContract(IsReference = true)]
    public class PlayerDTO
    {
        [DataMember]
        [ScaffoldColumn(true)]
        public int Id { get; set; }
        [DataMember]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Длина строки должна быть от 8 до 32 символов")]
        [RegularExpression(@"^[А-Яа-я\s]+$", ErrorMessage = "При вводе использовались недопустимые символы")]
        [Required(ErrorMessage = "Поле необходимо заполнить!")]
        public string Name { get; set; }
        [DataMember]
        [Range(20, 60, ErrorMessage = "Недопустимый возраст")]
        [Required(ErrorMessage = "Dude, please fill something in!")]
        public int Age { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public int? TeamId { get; set; }
        [DataMember]
        public TeamDTO Team { get; set; }
    }
}
