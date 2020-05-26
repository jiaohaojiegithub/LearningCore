using LearningCore.Data.MVCModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningCore.Services
{
    public interface IFilesService
    {
        Task<IEnumerable<AppFile>> GetList();
    }
}
