namespace SeleniumMailBot.Interfaces.Page
{
    public interface IMailPageObject : IPageObject
    {
        IMailPageObject SendMessage(string targetMailAdress, string text);
        ILoginPageObject Logout();
        string GetMessageText(int index);
    }
}