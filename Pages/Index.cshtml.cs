using LoginApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LoginApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<ContactInfo> listContact = new List<ContactInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=windows-uc6hst5;Initial Catalog=todo;Integrated Security=True;TrustServerCertificate=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from users";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContactInfo contact = new ContactInfo();
                                contact.Id = "" + reader.GetInt32(0);
                                contact.Name = reader.GetString(1);
                                contact.Email = reader.GetString(2);
                                contact.Address = reader.GetString(3);
                                contact.Phone = reader.GetString(4);

                                listContact.Add(contact);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Exception: " + err.ToString());
            }
        }
    }

    public class ContactInfo
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}