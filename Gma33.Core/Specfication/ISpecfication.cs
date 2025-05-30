﻿using Gma33.Core.Entites.StoreEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Specfication
{
    public interface ISpecfication<T> where T : BaseEntity
    {

        public Expression<Func<T, bool>> Critria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; }

        public Expression<Func<T,object>> OrderBy { get; set; }

        public Expression<Func<T,object>> OrderByDesc { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public bool EnablePagination { get; set; }
    }
}
