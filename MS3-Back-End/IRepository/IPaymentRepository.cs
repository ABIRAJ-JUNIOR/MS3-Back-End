﻿using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePayment(Payment payment);
        Task<ICollection<Payment>> GetAllPayments();
    }
}