namespace Challenge.API.Hubs.Contract
{
    public interface IMessageHub
    {
        public void TextBroadcast(string message);
        public void TextMessage(string receiver, string message);
    }
}
