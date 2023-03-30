using AutoMapper;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Application.ViewModels.Users;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.Services
{
    public class GenericService<Model,ViewModel, SaveViewModel> : IGenericService<Model,ViewModel, SaveViewModel>
        where ViewModel : class
        where SaveViewModel : class
        where Model : class

    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            Model model = _mapper.Map<Model>(vm);
            model = await _repository.AddAsync(model);
            SaveViewModel EntityVM = _mapper.Map<SaveViewModel>(model);
            return EntityVM;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }


        public virtual async Task Update(SaveViewModel vm, int id)
        {
            Model model = _mapper.Map<Model>(vm);

            await _repository.UpdateAsync(model, id);
        }

        public virtual async Task<SaveViewModel> GetByIdSave(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            SaveViewModel Entityvm = _mapper.Map<SaveViewModel>(entity);
            return Entityvm;
        }

        public virtual async Task<List<ViewModel>> GetAll()
        {
            var EntityList = await _repository.GetAllAsyncWithOutInclude();
            return _mapper.Map<List<ViewModel>>(EntityList);
        }
    }
}

