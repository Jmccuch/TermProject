namespace TermProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.Security.Principal;
    using System.Data.Common;
    using Utilities;
    using System.ComponentModel.DataAnnotations;

    namespace TermProject.Models
    {
        public class SignUpModel
        {


            [Required(ErrorMessage = "The Username field is required!")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "The Password field is required!")]
            public string Password { get; set; }

            [Required(ErrorMessage = "The First Name field is required!")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "The Last Name field is required!")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "The Email field is required!")]
            public string Email { get; set; }

            [Required(ErrorMessage = "The Security Answer 1 field is required!")]
            public string Answer1 { get; set; }

            [Required(ErrorMessage = "The Security Answer 2 field is required!")]
            public string Answer2 { get; set; }

            [Required(ErrorMessage = "The Security Answer 3 field is required!")]
            public string Answer3 { get; set; }


            public SignUpModel()
            {



            }



        }
    }
}

