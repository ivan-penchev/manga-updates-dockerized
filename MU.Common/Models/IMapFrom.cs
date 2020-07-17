using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MU.Common.Models
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), this.GetType());
    }
}
