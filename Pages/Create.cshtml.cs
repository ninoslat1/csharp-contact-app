using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LoginApp.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public ContactInfo contactInfo = new ContactInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            contactInfo.Name = Request.Form["name"];
            contactInfo.Name = Request.Form["email"];
            contactInfo.Name = Request.Form["phone"];
            contactInfo.Name = Request.Form["address"];

            if (contactInfo.Name?.Length == 0 || contactInfo.Email?.Length == 0
                || contactInfo.Phone?.Length == 0 || contactInfo.Address?.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            //save user data into a database
            try
            {
                String connectString = "Data Source=windows-uc6hst5;Initial Catalog=todo;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users " +
                        "(name, email, phone, address) VALUES " +
                        "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", contactInfo.Name);
                        command.Parameters.AddWithValue("@email", contactInfo.Email);
                        command.Parameters.AddWithValue("@phone", contactInfo.Phone);
                        command.Parameters.AddWithValue("@address", contactInfo.Address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            contactInfo.Name = "";
            contactInfo.Email = "";
            contactInfo.Phone = "";
            contactInfo.Address = "";
            successMessage = "New user has been added";

            Response.Redirect("/Index");
        }
    }
}
