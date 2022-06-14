using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Servientrega.Business.Interface;
using Servientrega.Data.Models;
using Servientrega.Infraestructure.Util;
using System;
using System.Collections.Generic;

namespace Servientrega.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvionController : ControllerBase
    {
        #region Members
        private readonly IAvionBusiness _AvionBusiness;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public AvionController(IAvionBusiness AvionBusiness)
        {
            _AvionBusiness = AvionBusiness;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IEnumerable<object> Get()
        {
            Result model = _AvionBusiness.GetAll();
            if (object.Equals(model.ListModel, null))
                return null;
            return model.ListModel;
        }

        [HttpGet("{id}")]
        public object GetById(Guid id)
        {
            Result model = _AvionBusiness.GetById(id);
            if (object.Equals(model, null))
                return null;
            return model;
        }

        [HttpPost]
        public Result Post(Avion model)
        {
            Result result = new();
            try
            {
                result = _AvionBusiness.Insert(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        [HttpPut("{id}")]
        public Result Put(Avion model)
        {
            Result result = new();
            try
            {
                result = _AvionBusiness.Update(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        // DELETE api/<AvionController>/5
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            Result result = new();
            try
            {
                result = _AvionBusiness.Delete(id);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }
        #endregion
    }
}
