using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabaseFirst_EF_Core.Models;

public partial class Student
{
    [Key]
    public int Id { get; set; }

    [Column("Stud_Name", TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column("Stud_Surname", TypeName = "varchar(50)")]
    public string? Surname { get; set; }

    public Gender Gender { get; set; }

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public string? City { get; set; }


}

public enum Gender
{
    Male, Female
}

