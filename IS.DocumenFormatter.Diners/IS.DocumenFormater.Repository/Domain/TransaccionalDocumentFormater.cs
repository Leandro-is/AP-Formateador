using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS.DocumenFormater.Repository.Domain
{
    [Table("TransaccionalDocumentFormater", Schema = "IS")]
    public class TransaccionalDocumentFormater
    {
        [Key]
        [Required]
        [CustomDisplayName("Id", IsRequired = true)]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(500, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("Nombres", IsRequired = true)]
        [Column("Names")]
        public String Names { get; set; }

        [StringLength(500, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("Apellido Parterno", IsRequired = true)]
        [Column("Lastname")]
        public String Lastname { get; set; }

        [StringLength(500, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("Apellido Materno", IsRequired = true)]
        [Column("Mothername")]
        public String Mothername { get; set; }

        [StringLength(100, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("DocumentType", IsRequired = true)]
        [Column("DocumentType")]
        public String DocumentType { get; set; }

        [StringLength(20, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("Nro de Documento", IsRequired = true)]
        [Column("NumberIdentifier")]
        public String NumberIdentifier { get; set; }

        [StringLength(100, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("Email", IsRequired = true)]
        [Column("Email")]
        public String Email { get; set; }


        [CustomDisplayName("Tipo de cliente", IsRequired = true)]
        [Column("CustomerTypeId")]
        public int CustomerTypeId { get; set; }

        [CustomDisplayName("Tipo de documento", IsRequired = true)]
        [Column("DocumentTypeId")]
        public int DocumentTypeId { get; set; }

        [StringLength(100, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("Número de documento", IsRequired = true)]
        [Column("DocumentNumber")]
        public String DocumentNumber { get; set; }

        [StringLength(100, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [CustomDisplayName("Número de solicitud", IsRequired = true)]
        [Column("RequestNumber")]
        public String RequestNumber { get; set; }

        [CustomDisplayName("Datos Registrados", IsRequired = true)]
        [Column("DataRegistered")]
        public DateTime? DataRegistered { get; set; }

        [CustomDisplayName("Formatos creados", IsRequired = true)]
        [Column("CreateFormats")]
        public DateTime? CreateFormats { get; set; }

        [CustomDisplayName("Data Recibida", IsRequired = true)]
        [Column("DataReceived")]
        public String DataReceived { get; set; }
    }
}
