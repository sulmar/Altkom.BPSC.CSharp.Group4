using System;

namespace Altkom.BPSC.CSharp.Shop.ConsoleClient
{
    class SendMessageCommand : ICommand
    {
        private readonly Message message;

        public SendMessageCommand(Message message)
        {
            this.message = message;
        }

        public void Execute()
        {
            if (CanExecute())
            {
                Console.WriteLine($"{message.From} -> {message.To} : {message.Content}");
            }
        }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(message.From)
                && !string.IsNullOrEmpty(message.To);
        }
       
    }
}
