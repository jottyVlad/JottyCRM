using System;

namespace JottyCRM.Stores
{
    public class ContractorsStore
    {
        public event Action<string, string, string> ContractorCreated;

        public void CreateContractor(string fullName, string email, string phoneNumber)
        {
            ContractorCreated?.Invoke(fullName, email, phoneNumber);
        }
    }
}