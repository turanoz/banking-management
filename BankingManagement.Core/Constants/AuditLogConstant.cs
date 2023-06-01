namespace BankingManagement.Core.Constants;

public class AuditLogConstant
{
    public const string Register = "Register";
    public const string Login = "Login";
    public const string LoginFailed = "Login Failed";
    public const string Logout = "Logout";
    public const string Transaction = "Transaction";
    public const string CreateTransaction = "Create Transaction";
    public const string ViewTransactionList = "View Transaction List";
    public const string ViewAccountList = "View Account List";
    public const string CreateAccount = "Create Account";
    public const string ProfileView = "Profile Viewed";
    public const string ProfileUpdate = "Profile Updated";
    public const string PasswordChange = "Password Changed";
    public const string ViewAuditLog = "View Audit Log";

    public string ViewAccount(Guid accountId)
    {
        return $"View Account : {accountId}";
    }
}