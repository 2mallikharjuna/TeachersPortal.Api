using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachPortal.Application.DTOs.Response
{
    public record StudentDto(int Id, string FirstName, string LastName, string Email);
}
