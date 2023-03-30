using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Interfaces.Services
{
    public interface IGenericService<Model, ViewModel, SaveViewModel>
        where ViewModel : class
        where SaveViewModel : class
        where Model : class
    {
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Delete(int id);
        Task Update(SaveViewModel vm, int id);
        Task<SaveViewModel> GetByIdSave(int id);
        Task<List<ViewModel>> GetAll();
    }
}
