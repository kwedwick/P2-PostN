
using PostN.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;


namespace Tests
{
    public class UnitTest1
    {
        
        [Fact]
        public void Validate_UserAccountInput()
        {
            {
                var regUser = new CreatedUser
                {
                    FirstName = "UnitTest",
                    LastName = "test",
                    Username = "unittest",
                    Email = "unittest@gmail.com",
                    Password = "password@123",
                    DoB = new DateTime(1994, 2, 12)

                };

                var validationResults = new List<ValidationResult>();
                var actual = Validator.TryValidateObject(regUser, new ValidationContext(regUser), validationResults, true);
                Assert.True(actual, "Expected to return true");

            }
        }
        
    }
}
