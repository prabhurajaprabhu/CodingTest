using System;

namespace App.repositroy
{
    public interface ICompanyRepository
    {
        Company GetbyId(int id);

        bool AddCustomer(string firstName, string surName, string email, DateTime dateOfBirth, int companyId);
    }
}
