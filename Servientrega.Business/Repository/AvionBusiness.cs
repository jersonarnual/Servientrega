using AutoMapper;
using Servientrega.Business.Interface;
using Servientrega.Data.Interface;
using Servientrega.Data.Models;
using Servientrega.Infraestructure.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servientrega.Business.Repository
{
    public class AvionBusiness : IAvionBusiness
    {
        #region Member
        private readonly IAvionRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public AvionBusiness(IAvionRepository repository,
                              IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public Result Delete(Guid id)
        {
            Result result = new();
            try
            {
                Avion Avion = _repository.GetById(id);
                if (object.Equals(Avion, null))
                {
                    result.MessageException = $"ERROR: El modelo se encuentra vacio";
                    result.State = false;
                    return result;
                }

                if (_repository.Delete(Avion))
                {
                    result.State = true;
                    result.Message = "Operacion Exitosa";
                    return result;
                }
                result.State = false;
                result.Message = "No se logro completar la operacion";
                return result;

            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }

        public Result GetAll()
        {
            Result result = new();
            List<Avion> Avion = new();
            try
            {
                var model = _repository.GetAll();
                if (!model.Any() || object.Equals(model, null))
                {
                    result.MessageException = $"ERROR: El objeto se encuentra vacio";
                    result.State = false;
                    return result;
                }
                foreach (var item in model)
                    Avion.Add(item);

                result.ListModel = Avion;
                result.Message = "Operacion Exitosa";
                result.State = true;
                return result;
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }

        public Result GetById(Guid id)
        {
            Result result = new();
            try
            {
                var model = _repository.GetById(id);
                if (object.Equals(model, null))
                {
                    result.MessageException = $"ERROR: No se encontraron registros";
                    result.State = false;
                }
                result.Model = model;
                result.Message = "Operacion Exitosa";
                result.State = true;
                return result;
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }

        public Result Insert(Avion entity)
        {
            Result result = new();
            try
            {
                if (object.Equals(entity, null))
                {
                    result.MessageException = $"ERROR: El modelo se encuentra vacio";
                    result.State = false;
                    return result;
                }

                var model = entity;
                if (_repository.Insert(model))
                {
                    result.State = true;
                    result.Message = "Operacion Exitosa";
                    return result;
                }
                result.State = false;
                result.Message = "No se logro completar la operacion";
                return result;
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }

        public Result Update(Avion entity)
        {
            Result result = new();
            try
            {
                if (object.Equals(entity, null))
                {
                    result.MessageException = $"ERROR: El modelo se encuentra vacio";
                    result.State = false;
                    return result;
                }

                var model = entity;
                if (_repository.Update(model))
                {
                    result.State = true;
                    result.Message = "Operacion Exitosa";
                    return result;
                }
                result.State = false;
                result.Message = "No se logro completar la operacion";
                return result;
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }
        #endregion
    }
}
