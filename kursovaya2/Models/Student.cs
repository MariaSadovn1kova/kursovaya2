//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kursovaya2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            this.Registration = new HashSet<Registration>();
        }

        [Required]
        [Display(Name = "Номер зачетной книжки")]
        [Range(0, 999999999)]
        public int StudentNumber { get; set; }

        [Required]
        [Display(Name = "Номер группы")]
        [Range(0, 999999999)]
        public int Group_NumberOfGroup { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Fulll_Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registration> Registration { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
    }
}
