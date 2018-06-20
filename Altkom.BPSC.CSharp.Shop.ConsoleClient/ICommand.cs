namespace Altkom.BPSC.CSharp.Shop.ConsoleClient
{
    public interface ICommand
    {
        void Execute();

        bool CanExecute();
    }
}
