 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services
{
    public class AccountService : IAccountServices
    {
        // store the accounts in a dictionary indexed by the account name
        private Dictionary<string, AccountBase> accountsDictionary;

        public AccountService()
        {
            //instantiate the dictionary for accounts
            accountsDictionary = new Dictionary<string, AccountBase>();
        }

        public List<string> GetAllAccounts()
        // get a list of all account names
        {
            return new List<string>(accountsDictionary.Keys);
        }

        public void CreateAccount(string accountName, AccountType accountType)
        // create a new account
        {
            AccountBase newAccount = AccountBase.CreateAccount(accountType);
            accountsDictionary.Add(accountName, newAccount);
        }

        public decimal GetAccountBalance(string accountName)
        // find the balance of the given account
        {
            AccountBase acc = FindAccount(accountName);
            return acc.Balance;
        }
        public int GetRewardPoints(string accountName)
        // find the reward points of the given account
        {
            AccountBase acc = FindAccount(accountName);
            return acc.RewardPoints;
        }

        public void Deposit(string accountName, decimal amount)
        // deposit the given account into the account named
        {
            AccountBase acc = FindAccount(accountName);
            acc.AddTransaction(amount);
        }

        public void Withdrawal(string accountName, decimal amount)
        // withdrawal the given account into the account named
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            }

            AccountBase acc = FindAccount(accountName);
            if (acc == null)
            {
                throw new KeyNotFoundException($"Account '{accountName}' not found.");
            }

            if (acc.Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
            }

            acc.AddTransaction(-amount); // This will decrease the balance without adding reward points // Subtract the amount from the account balance
                                         
        }

        private AccountBase FindAccount(string accountName)
        {
            if (accountsDictionary.ContainsKey(accountName))
            {
                return accountsDictionary[accountName];
            }
            return null;
        }

    }
}
