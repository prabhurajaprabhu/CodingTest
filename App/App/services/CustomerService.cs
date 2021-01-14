using App.models;
using App.repositroy;
using App.services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace App
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;
        public Company GetbyId(int id)
        {
            Company company = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetCompanyById"
                };

                var parameter = new SqlParameter("@CompanyId", SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    company = new Company
                    {
                        Id = int.Parse(reader["CompanyId"].ToString()),
                        Name = reader["Name"].ToString(),
                        Classification = (Classification)int.Parse(reader["ClassificationId"].ToString())
                    };
                }
            }

            return company;
        }

        public bool AddCustomer(string firstName, string surName, string email, DateTime dateOfBirth, int companyId)
        {
            var customerValidationService = new CustomerValidationService();

            var isValid = customerValidationService.IsValid(firstName, surName, email);
            
            var isValidAge = customerValidationService.IsValidAge(dateOfBirth);

            if (!isValid || !isValidAge)
            {
                return false;
            }

            var company = GetbyId(companyId);

            var customer = new Customer
            {
                Company = company,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstName,
                Surname = surName,
                HasCreditLimit = company.Name == Constants.VeryImportantClient ? false : true
            };

            if (customer.HasCreditLimit)
            {
                using (var customerCreditService = new CustomerCreditServiceClient())
                {
                    var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    customer.CreditLimit = company.Name == Constants.ImportantClient ? creditLimit * 2 : creditLimit;
                }
            }

            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }

            CustomerDataAccess.AddCustomer(customer, connectionString);

            return true;
        }
    }
}
