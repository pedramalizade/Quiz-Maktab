﻿using Quiz_Maktab.APPDbContext;
using Quiz_Maktab.Entities;
using Quiz_Maktab.Interface.Repository;
using Quiz_Maktab.Interface.Service;
using Quiz_Maktab.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly CardService _cardService;
        private readonly TransactionRepository _transactionRepository;
        public TransactionService()
        {
            _cardService = new CardService();
            _transactionRepository = new TransactionRepository();
        }

        public bool Transfer(string sourceCardNumber, string destinationCardNumber,  float amount)
        {
            var transactionamount = _transactionRepository.TransactionAmountInDay(sourceCardNumber);
            if (transactionamount >= 250)
            {
                Console.WriteLine(" Transfer limit has been exceeded.");
                return false;
            }
            if (transactionamount + amount > 250)
            {
                Console.WriteLine($"The Transfer limit will be  exceeded.Entered amonut must be less than {transactionamount - 250}");
                Console.ReadKey();
                return false;

            }

            if (amount < 0)
            {
                Console.WriteLine("The transfer amount must be greater than zero.");
                Console.ReadKey();
                return false;

            }

            _cardService.ReduceAmount(amount, sourceCardNumber, destinationCardNumber);


            var sourceCard = _cardService.GetCardByNumber(sourceCardNumber);
            var destinationCard = _cardService.GetCardByNumber(destinationCardNumber);

            if(sourceCard == null || destinationCardNumber == null )
            {
                Console.WriteLine("Surce or destination card not found.");
                return false;
            }

            if(!_cardService.CheckCardBalance(sourceCard, amount))
            {
                Console.WriteLine("Insifficient balance on the source card.");
                return false;
            }

            _cardService.DeductBalance(sourceCard, amount);
            _cardService.AddBalance(destinationCard, amount);

            var transaction = new Transaction 
            {
                SourceCardNumber = sourceCardNumber,
                DestinationCardNumber = destinationCardNumber,
                Amount = amount,
                TranceactionTime = DateTime.Now,
                IsSuccessful = true
            };

            _transactionRepository.AddTransaction(transaction);
            return true;

            
        }

        public List<Transaction> GetTransactions(string cardNumber)
        {
            return _transactionRepository.GetAllTransaction(cardNumber);

        }
    }
}
