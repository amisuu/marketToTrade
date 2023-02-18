using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum YesOrNoEnum
    { 
        [Display(Name = "Yes")]
        Yes,
        [Display(Name = "No")]
        No,
    }
}
