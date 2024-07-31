using ExcelMerger.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExcelMerger.Controllers
{

    public class AccountController : Controller
    {
        private AccountDbContext db = new AccountDbContext(); // Replace with your actual DbContext

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Fetch the user from the database
                var user = db.Users.FirstOrDefault(u => u.Logon == model.Username);

                if (user != null)
                {
                    // Log the retrieved and entered passwords (ensure you remove these logs in production)
                    System.Diagnostics.Debug.WriteLine($"Entered Password: {model.Password}");
                    System.Diagnostics.Debug.WriteLine($"Stored Password: {user.Password}");

                    // Assuming you have a method to decrypt or verify the hashed password
                    if (VerifyPassword(model.Password, user.Password))
                    {
                        // Authentication successful, redirect to the home page
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Authentication failed, add error to model state
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                }
                else
                {
                    // User not found
                    System.Diagnostics.Debug.WriteLine("User not found");
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Implement your password verification logic here
            // For example, if using hashing:
            // return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);

            // Or if using decryption:
            // string decryptedPassword = Decrypt(storedPassword);
            // return enteredPassword == decryptedPassword;

            // Example decryption (replace with your actual logic)
            string decryptedPassword = Decrypt(storedPassword);
            return enteredPassword == decryptedPassword;
        }

        private string Decrypt(string encryptedText)
        {
            // Implement your decryption logic here
            // For example, if you are using a specific encryption method, make sure to use the corresponding decryption method.
            // This is a placeholder, replace it with your actual decryption logic
            return encryptedText;
        }
    }
}
