namespace Afterman.nRepo.UnitsOfWork
{
    using System;

    public interface IUnitOfWork :
        IDisposable
    {
        void Begin();

        void End(Exception e = null);

    }
}
