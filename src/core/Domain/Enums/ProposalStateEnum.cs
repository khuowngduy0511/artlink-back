namespace Domain.Enums;

public enum ProposalStateEnum
{
    Waiting = 0, // 1
    Accepted = 1, // 2.1
    Declined = 2, // 2.2
    Cancelled = 5, // 2.3
    InitPayment = 3, // 3
    Completed = 6, // 4  
    CompletePayment = 4, //5
    ConfirmPayment = 7 //6
}
