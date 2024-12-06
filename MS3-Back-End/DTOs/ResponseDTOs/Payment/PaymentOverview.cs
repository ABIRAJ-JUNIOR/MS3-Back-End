namespace MS3_Back_End.DTOs.ResponseDTOs.Payment
{
    public class PaymentOverview
    {
        public double TotalAmount { get; set; }
        public double FullPayment {  get; set; }
        public double Installment {  get; set; }
        public double OverDue {  get; set; }
    }
}
