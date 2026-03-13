using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS.DocumenFormater.Repository.Domain
{
    [Table("EventLog", Schema = "Log")]
    public class EventLog
    {
        [Key]
        [Required]
        [CustomDisplayName("Id", IsRequired = true)]
        [Column("Id")]
        public int Id { get; set; }

        [CustomDisplayName("Event Id", IsRequired = true)]
        [Column("EventId")]
        public int EventId { get; set; }

        [StringLength(1000, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("LogLevel", IsRequired = true)]
        [Column("LogLevel")]
        public String LogLevel { get; set; }

        [StringLength(1000, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("Tag", IsRequired = true)]
        [Column("Tag")]
        public String Tag { get; set; }

        [CustomDisplayName("Message", IsRequired = true)]
        [Column("Message")]
        public String Message { get; set; }

        [CustomDisplayName("Nombre Entidad", IsRequired = true)]
        [Column("EntityName")]
        public String EntityName { get; set; }

        [CustomDisplayName("Id Entidad", IsRequired = true)]
        [Column("EntityId")]
        public String EntityId { get; set; }

        [CustomDisplayName("Campo Entidad", IsRequired = true)]
        [Column("EntityField")]
        public String EntityField { get; set; }

        [CustomDisplayName("Valor Entidad", IsRequired = true)]
        [Column("EntityValue")]
        public String EntityValue { get; set; }

        [Required]
        [CustomDisplayName("Fecha de Creación", IsRequired = true)]
        [Column("CreationDate")]
        public DateTime CreationDate { get; set; }
    }
}
