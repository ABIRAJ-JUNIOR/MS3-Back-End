namespace MS3_Back_End.DTOs.ResponseDTOs.Payment
{
    public class PaymentOverview
    {
        public decimal TotalPayment { get; set; }
        public decimal FullPayment {  get; set; }
        public decimal Installment {  get; set; }
        public decimal OverDue {  get; set; }
    }
}
