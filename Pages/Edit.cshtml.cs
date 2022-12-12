using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LoginApp.Pages.Shared
{
    [Authorize]
    public class EditModel : PageModel
    {
        public ContactInfo contactInfo = new ContactInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            String Id = Request.Query["Id"];

            try
            {
                String connectString = "Data Source=windows-uc6hst5;Initial Catalog=todo;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users WHERE id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                contactInfo.Id = "" + reader.GetInt32(0);
                                contactInfo.Name = reader.GetString(1);
                                contactInfo.Email = reader.GetString(2);
                                contactInfo.Phone = reader.GetString(3);
                                contactInfo.Address = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            contactInfo.Id = Request.Form["id"];
            contactInfo.Name = Request.Form["name"];
            contactInfo.Email = Request.Form["email"];
            contactInfo.Phone = Request.Form["phone"];
            contactInfo.Address = Request.Form["address"];

            if (contactInfo.Name?.Length == 0 || contactInfo.Email?.Length == 0
                || contactInfo.Phone?.Length == 0 || contactInfo.Address?.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                String connectString = "Data Source=windows-uc6hst5;Initial Catalog=todo;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectString))
                {
                    connection.Open();
                    String sql = "UPDATE users " +
                        "SET name=@name, email=@email, phone=@phone, address=@address" +
                        " WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", contactInfo.Id);
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

            Response.Redirect("/Index");

        }
    }
}
