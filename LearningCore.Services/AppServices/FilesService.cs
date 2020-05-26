using LearningCore.Data.MVCModels;
using LearningCore.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningCore.Services
{
    public class FilesService : IFilesService
    {
        private readonly LearningCoreContext _context;

        public FilesService(LearningCoreContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppFile>> GetList()
        {
            return await _context.Files.ToListAsync();
        }
    }
}
