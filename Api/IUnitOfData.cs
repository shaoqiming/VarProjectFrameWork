using System;

namespace VarProject.FrameWork.Core.Api
{
    public interface IUnitOfData:IDisposable
    {
        int Submit();

        int EFSubmit();
    }
}
