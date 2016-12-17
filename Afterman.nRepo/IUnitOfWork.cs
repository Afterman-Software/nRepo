using System;

namespace Afterman.nRepo
{
    public interface IUnitOfWork :
        IDisposable
    {
        void Begin();

        void End(Exception e = null);

    }
}
