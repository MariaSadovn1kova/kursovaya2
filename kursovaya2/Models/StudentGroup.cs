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

    public partial class StudentGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentGroup()
        {
            this.Curriculum = new HashSet<Curriculum>();
        }
    
        [Required]
        [Display (Name = "Номер группы")]
        [Range (0, 999999)]
        public int NumberOfGroup { get; set; }

        [Required]
        [Display(Name = "Специальность")]
        public string Speciality { get; set; }

        [Required]
        [Display(Name = "Год поступления")]
        [Range(1950, 2021)]
        public int YearOf { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Curriculum> Curriculum { get; set; }

        public virtual ICollection<Student> Student { get; set; }
    }
}
