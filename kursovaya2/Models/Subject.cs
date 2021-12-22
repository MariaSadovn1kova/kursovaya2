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

    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            this.Curriculum = new HashSet<Curriculum>();
        }

        [Required]
        [Display(Name = "Название предмета")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Имя лектора")]
        public string FullNameOfLecturer { get; set; }
        [Required]
        [Display(Name = "Название кафедры")]
        public string TitleOfDepartmen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Curriculum> Curriculum { get; set; }
    }
}
