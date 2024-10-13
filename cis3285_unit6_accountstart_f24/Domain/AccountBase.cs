using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class AccountBase
    {
        public static AccountBase CreateAccount(AccountType type)
        {
            AccountBase account = null;
            switch(type)
            {
                case AccountType.Silver:
                    account = new SilverAccount();
                    break;
                case AccountType.Gold:
                    account = new GoldAccount();
                    break;
                case AccountType.Platinum:
                    account = new PlatinumAccount();
                    break;
            }
            return account;
        }

        public decimal Balance
        {
            get;
            private set;
        }

        public int RewardPoints
        {
            get;
            private set;
        }

        public void AddTransaction(decimal amount)
        {
            if (amount > 0)
            {
                // Only add reward points for deposits (positive amounts)
                RewardPoints += CalculateRewardPoints(amount);
            }

            // Adjust the balance regardless of the sign of the amount
            Balance += amount;
        }

        public abstract int CalculateRewardPoints(decimal amount);
    }
}
